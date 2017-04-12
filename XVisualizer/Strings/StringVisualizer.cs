using System;

using Microsoft.VisualStudio.DebuggerVisualizers;

namespace XVisualizer.Strings
{
    public class StringVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            string str = (string)objectProvider.GetObject();

            if (str != null)
            {
                using (StringForm form = new StringForm())
                {
                    form.SetString(str);
                    form.ShowInTaskbar = false;

                    windowService.ShowDialog(form);
                }
            }
        }
    }
}