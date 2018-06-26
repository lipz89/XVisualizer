using System.Drawing;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ImageVisualizer
{
    class ImageVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Image image = (Image)objectProvider.GetObject();

            if (image != null)
            {
                using (ImageForm form = new ImageForm())
                {
                    form.ShowInTaskbar = false;
                    form.Set(image);

                    windowService.ShowDialog(form);
                }
            }
        }
    }
}