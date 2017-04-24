using System;

using XCommonTest;

namespace Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            TestViewer.TestString();
            //TestViewer.TestExpressionViewer();
            //TestViewer.TestImageViewer();
            //TestViewer.TestColor();
            //TestViewer.TestFont();

            Console.Read();
        }
    }
}
