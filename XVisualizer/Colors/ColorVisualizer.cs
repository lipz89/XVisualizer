using System.Drawing;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace XVisualizer
{
    public class ColorVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Color color = (Color)objectProvider.GetObject();


            using (ColorForm form = new ColorForm())
            {
                form.ShowInTaskbar = false;
                form.Set(color);

                windowService.ShowDialog(form);
            }
        }
    }
}