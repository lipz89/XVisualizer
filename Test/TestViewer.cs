using System;
using System.Drawing;
using System.Linq.Expressions;

using Microsoft.VisualStudio.DebuggerVisualizers;

using XVisualizer;
using XVisualizer.Expressions;
using XVisualizer.Images;
using XVisualizer.Strings;

namespace XCommonTest
{
    class TestViewer
    {
        public static void TestFont()
        {
            var font = new Font("黑体", 36f);

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(font, typeof(FontVisualizer));
            visualizerHost.ShowVisualizer();
        }

        public static void TestColor()
        {
            var color = Color.FromArgb(171, 215, 170);
            var color2 = ConsoleColor.Red;
            var color3 = SystemColors.Control;
            var brush = Brushes.Red;

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(color, typeof(ColorVisualizer));
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

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(image, typeof(ImageVisualizer));
            visualizerHost.ShowVisualizer();
        }

        public static void TestString()
        {
            //'\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '​', '\u2028', '\u2029', '　', '﻿'
            var js = "第一行\n\n第二行\r\r\n第三行\r第四行\n\n\r第五行";
            js = "{a:[1,'\\uf556',\"\\uf535\\u6ff53\",{b:[],c:{},d:false,f:undefined,g:null,h:5.333}]}";

            //js = "<?xml version=\"1.0\"?>  <EFConnectionConfigInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">    <DataSource>172.16.0.219</DataSource>      <Port>1521</Port>    <Database>DMSP</Database>    <IntgSecurity>false</IntgSecurity>    <PerSecurity>true</PerSecurity>    <UserId>629C81F71AA055C8</UserId>    <Password>629C81F71AA055C8</Password>    <expireTime>30</expireTime>    <DataProvider>oracle</DataProvider>  </EFConnectionConfigInfo>";

            //js = "sysdate";

            //js = js.Replace("\r", Environment.NewLine);
            //js = js.Replace("\r\n\n", Environment.NewLine);
            //js = js.Replace("\n", Environment.NewLine);
            //js = js.Replace("\r\r\n", Environment.NewLine);

            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(js, typeof(StringVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}