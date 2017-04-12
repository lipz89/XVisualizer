using System.Drawing;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace XVisualizer
{
    public class FontVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Font font = (Font)objectProvider.GetObject();


            using (FontForm form = new FontForm())
            {
                form.ShowInTaskbar = false;
                form.Set(font);

                windowService.ShowDialog(form);
            }
        }
    }
}