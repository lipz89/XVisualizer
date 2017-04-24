using System.Text.RegularExpressions;

namespace XVisualizer.Strings.Htmls
{
    public partial class HtmlViewer : BaseViewer
    {
        private readonly Regex reg = new Regex("<script[^<]*</script>");
        public HtmlViewer()
        {
            InitializeComponent();
        }

        public override void SetContent(string html)
        {
            var htmlWithoutScript = reg.Replace(html, "");
            webBrowser1.DocumentText = htmlWithoutScript;
        }
    }
}
