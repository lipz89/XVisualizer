using System;
using System.Drawing;

namespace ColorVisualizer
{
    static class ColorConverter
    {
        public static string ToHex(this Color color)
        {
            var str = string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            return str;
        }

        public static string GetName(this Color color)
        {
            return color.Name;
        }

        public static string ToARGB(this Color color)
        {
            var str = string.Format("{0},{1},{2},{3}", color.A, color.R, color.G, color.B);
            return str;
        }

        public static string ToCMYK(this Color color)
        {
            double cyan = 0;
            double magenta = 0;
            double yellow = 0;
            double keyPlate = 0;
            
            if (color.R == 0 && color.G == 0 && color.B == 0)
            {
                keyPlate = 100;
            }
            else
            {
                cyan = 1 - color.R / 255d;
                magenta = 1 - color.G / 255d;
                yellow = 1 - color.B / 255d;

                var black = Math.Min(cyan, Math.Min(magenta, yellow));

                cyan = (cyan - black) / (1 - black) * 100;
                magenta = (magenta - black) / (1 - black) * 100;
                yellow = (yellow - black) / (1 - black) * 100;
                keyPlate = black * 100;
            }

            var str = string.Format("{0},{1},{2},{3}", (int)cyan, (int)magenta, (int)yellow, (int)keyPlate);
            return str;
        }
    }
}
