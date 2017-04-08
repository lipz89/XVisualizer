using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.DebuggerVisualizers;
using XVisualizer;
using XVisualizer.Expressions;
using XVisualizer.Images;
using XVisualizer.Strings;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("XVisualizer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("XVisualizer")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//将 ComVisible 设置为 false 将使此程序集中的类型
//对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("9b8b69a0-8f08-4ab2-b78f-2d3817c69bfc")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: InternalsVisibleTo("XCommonTest")]
//可视化调试工具
[assembly: DebuggerVisualizer(typeof(ImageVisualizer), typeof(VisualizerObjectSource), Target = typeof(Image), Description = "XImage")]
[assembly: DebuggerVisualizer(typeof(ExpressionTreeVisualizer), typeof(ExpressionTreeVisualizerObjectSource), Target = typeof(Expression), Description = "XExpression")]
[assembly: DebuggerVisualizer(typeof(StringVisualizer), Target = typeof(string), Description = "XString")]
[assembly: DebuggerVisualizer(typeof(ColorVisualizer), Target = typeof(Color), Description = "XColor")]
[assembly: DebuggerVisualizer(typeof(FontVisualizer), Target = typeof(Font), Description = "XFont")]

