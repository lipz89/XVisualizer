using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ExpressionVisualizer
{
    [Serializable]
    class ExpressionNode : TreeNode
    {
        public int ImgIndex
        {
            set
            {
                base.ImageIndex = value;
                base.SelectedImageIndex = value;
            }
        }

        public ExpressionNode(object obj2, string name = null)
        {
            base.Text = string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                base.Text = name + " : ";
            }
            base.Text += obj2.GetType().ObtainOriginalName();
            if (obj2 is Expression)
            {
                ImgIndex = 2;
                AddProperties(obj2);
            }
            else if (!(obj2 is IEnumerable) || obj2 is string)
            {
                if (obj2 is MethodInfo)
                {
                    ImgIndex = 3;
                    MethodInfo method = obj2 as MethodInfo;
                    base.Text += " : " + method.ObtainOriginalMethodName();
                }
                else if (obj2 is Type)
                {
                    ImgIndex = 1;
                    Type type = obj2 as Type;
                    base.Text += " : " + type.ObtainOriginalName();
                }
                else if (obj2 is PropertyInfo)
                {
                    ImgIndex = 3;
                    PropertyInfo member = obj2 as PropertyInfo;
                    base.Text += " : " + member.PropertyType.ObtainOriginalName() + " " + member.Name;
                }
                else if (obj2 is FieldInfo)
                {
                    ImgIndex = 3;
                    FieldInfo member = obj2 as FieldInfo;
                    base.Text += " : " + member.FieldType.ObtainOriginalName() + " " + member.Name;
                }
                else
                {
                    ImgIndex = 4;
                    if (obj2.GetType().IsValueType() || obj2.GetType().IsEnum)
                    {
                        base.Text += " : " + obj2;
                    }
                    else if (obj2 is string)
                    {
                        base.Text += " : \"" + obj2 + "\"";
                    }
                    else if (obj2 is DateTime)
                    {
                        base.Text += string.Format(" : {0:yyyy-MM-dd HH:mm:ss}", obj2);
                    }
                }
            }
            else
            {
                ImgIndex = 11;

                var count = 0;
                foreach (object obj3 in (IEnumerable)obj2)
                {
                    if (obj3 is Expression)
                    {
                        base.Nodes.Add(new ExpressionNode(obj3, count.ToString()));
                    }
                    else if (obj3 is MemberAssignment)
                    {
                        var node = new ExpressionNode(obj3, count.ToString());
                        base.Nodes.Add(node);
                        node.AddProperties(obj3);
                    }
                    else if (obj3 is ElementInit)
                    {
                        var node = new ExpressionNode(obj3, count.ToString());
                        base.Nodes.Add(node);
                        node.AddProperties(obj3);
                    }
                    count++;
                }
                if (count == 0)
                {
                    ImgIndex = 4;
                    base.Text += " : Empty";
                }
                else
                {
                    base.Text += " : [" + count + "]";
                }
            }
        }

        protected override void Deserialize(SerializationInfo serializationInfo, StreamingContext context)
        {
            base.Deserialize(serializationInfo, context);

            this.ImageIndex = serializationInfo.GetInt32(nameof(ImageIndex));
            this.SelectedImageIndex = this.ImageIndex;
        }

        protected void AddProperties(object obj)
        {
            var type = obj.GetType();
            List<PropertyInfo> properties = null;
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Expression<>)))
            {
                if (type.BaseType != null)
                {
                    properties = type.BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                }
            }
            else
            {
                properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            }

            if (properties != null)
            {
                properties = properties.OrderBy(x => x.Name).ToList();
                if (obj is BinaryExpression)
                {
                    var left = properties.FirstOrDefault(x => x.Name == "Left");
                    var right = properties.FirstOrDefault(x => x.Name == "Right");
                    properties.Remove(left);
                    properties.Remove(right);
                    properties.Insert(0, right);
                    properties.Insert(0, left);
                }
                foreach (PropertyInfo info in properties)
                {
                    object obj2 = info.GetValue(obj, null);
                    if (obj2 != null)
                    {
                        if (obj2 is Expression)
                        {
                            base.Nodes.Add(new ExpressionNode(obj2, info.Name));
                        }
                        else
                        {
                            base.Nodes.Add(new ExpressionNode(obj2, info.Name));
                        }
                    }
                    else
                    {
                        base.Nodes.Add(new TreeNode(info.Name + " : " + info.PropertyType.ObtainOriginalName() + " : Null"));
                    }
                }
            }
        }

        protected ExpressionNode(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
