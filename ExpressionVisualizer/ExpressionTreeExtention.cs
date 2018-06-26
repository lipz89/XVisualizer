using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ExpressionVisualizer
{
    static class ExpressionTreeExtention
    {
        // Methods
        private static string ExtractGenericArguments(IEnumerable<Type> names)
        {
            StringBuilder builder = new StringBuilder("<");
            foreach (Type type in names)
            {
                if (builder.Length != 1)
                {
                    builder.Append(", ");
                }
                builder.Append(type.ObtainOriginalName());
            }
            builder.Append(">");
            return builder.ToString();
        }

        private static string ExtractName(string name)
        {
            int length = name.IndexOf("`", StringComparison.Ordinal);
            if (length > 0)
            {
                name = name.Substring(0, length);
            }
            return name;
        }

        public static string ObtainOriginalMethodName(this MethodInfo method)
        {
            if (method.IsGenericMethod)
            {
                return (ExtractName(method.Name) + ExtractGenericArguments(method.GetGenericArguments()));
            }
            return method.Name;
        }
        private static string ObtainOriginalNameCore(this Type type)
        {
            if (type.IsArray)
            {
                var n = type.Name;
                var etype = type.GetElementType();
                return n.Replace(etype.Name, etype.ObtainOriginalName());
            }
            if (type.IsGenericType)
            {
                var gt = ExtractName(type.FullName);
                var gtp = ExtractGenericArguments(type.GetGenericArguments());
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return gtp.Substring(1, gtp.Length - 2) + "?";
                }
                return gt + gtp;
            }
            return type.FullName;
        }

        public static string ObtainOriginalName(this Type type)
        {
            if (simpleName.ContainsKey(type))
            {
                return simpleName[type];
            }
            return type.ObtainOriginalNameCore();
        }

        private static Dictionary<Type, string> simpleName = new Dictionary<Type, string>
        {
            { typeof(object), "object"},
            { typeof(string), "string"},
            { typeof(bool), "bool"},
            { typeof(char) ,"char"},
            { typeof(int), "int"},
            { typeof(uint), "uint"},
            { typeof(byte), "byte"},
            { typeof(sbyte), "sbyte"},
            { typeof(short), "short"},
            { typeof(ushort), "ushort"},
            { typeof(long), "long"},
            { typeof(ulong), "ulong"},
            { typeof(float), "float"},
            { typeof(double), "double"},
            { typeof(decimal), "decimal"},
            { typeof(void), "void"}
        };
        public static bool IsValueType(this Type t)
        {
            var tc = Type.GetTypeCode(t);
            return tc >= TypeCode.Boolean && tc <= TypeCode.Decimal;
        }
    }
}