using System.Drawing;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace FontVisualizer
{
    class FontVisualizer : DialogDebuggerVisualizer
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