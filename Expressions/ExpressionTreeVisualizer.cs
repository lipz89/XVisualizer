using System.Windows.Forms;

using Microsoft.VisualStudio.DebuggerVisualizers;

namespace XVisualizer.Expressions
{
    public class ExpressionTreeVisualizer : DialogDebuggerVisualizer
    {
        // Methods
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            ExpressionTreeContainer container = (ExpressionTreeContainer)objectProvider.GetObject();
            TreeView view = new TreeView();
            view.Nodes.Add(container.TreeNode);
            using (TreeWindow form = new TreeWindow())
            {
                form.ShowInTaskbar = false;
                form.Set(view, container.Expression);

                windowService.ShowDialog(form);
            }
        }
    }
}