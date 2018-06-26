using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using ExpressionVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;

using Newtonsoft.Json;


namespace Visualizer.Test
{
    class TestViewer
    {
        public static void TestFont()
        {
            var font = new Font("黑体", 36f);

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(font, typeof(FontVisualizer.FontVisualizer));
            visualizerHost.ShowVisualizer();
        }

        public static void TestColor()
        {
            var color = Color.FromArgb(171, 215, 170);
            var color2 = ConsoleColor.Red;
            var color3 = SystemColors.Control;
            var brush = Brushes.Red;

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(color, typeof(ColorVisualizer.ColorVisualizer));
            visualizerHost.ShowVisualizer();
        }

        public static void TestExpressionViewer()
        {
            Expression<Func<int, string, int>> f = (x, y) => x * x + y.Length;

            //var s = new {X = 1, Y = 2};
            //var e = GetAssigner(s.GetType());


            VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(f,
                                                                           typeof(ExpressionTreeVisualizer),
                                                                           typeof(ExpressionTreeVisualizerObjectSource));
            host.ShowVisualizer();
        }

        public static void TestImageViewer()
        {
            var image = new Bitmap(300, 100);
            using (var gp = Graphics.FromImage(image))
            {
                gp.FillRectangle(Brushes.Red, 10, 10, 10, 10);
                gp.Flush();
            }

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(image, typeof(ImageVisualizer.ImageVisualizer));
            visualizerHost.ShowVisualizer();
        }

        public static void TestString()
        {
            var tc = new StackTrace().ToString();
            var lst = new List<object>();
            //for (int i = 0; i < 10; i++)
            //{
            //    lst.Add(new { text = i, value = new { message = tc } });
            //}

            var js = JsonConvert.SerializeObject(lst);
            js = "{a:{b:1},c:[1,0.5,\"12\"]}";
            Console.WriteLine(js.Length);
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(js, typeof(StringVisualizer.StringVisualizer));
            visualizerHost.ShowVisualizer();

            //using (StringForm form = new StringForm())
            //{
            //    form.SetString(js);
            //    form.ShowInTaskbar = false;

            //    form.ShowDialog();
            //}
        }
    }
}