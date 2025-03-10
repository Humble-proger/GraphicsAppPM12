using Avalonia.Controls;
using Avalonia.Input;
using System.Numerics;
using GraphicsApp.ViewModels;
using Avalonia.Interactivity;

namespace GraphicsApp.Views
{
    public partial class MyCanvas : UserControl
    {
        public MyCanvas()
        {
            InitializeComponent();
        }
        
        private void MouseMoved(object sender, PointerEventArgs args) 
        {
            if (DataContext is CanvasViewModel viewModel) {
                var point = args.GetPosition((Canvas) sender);
                viewModel.ChangeMouseCoord.Execute(point);
            }
        }
        
        private void MouseLeave(object sender, RoutedEventArgs e)
        {
            if (DataContext is CanvasViewModel viewModel)
                viewModel.MouseLeaveCanvas.Execute(null);
        }

        private void CanvasResize(object sender, SizeChangedEventArgs e) 
        {
            if (DataContext is CanvasViewModel viewModel)
            {
                Vector2 newSize = new Vector2() { X = (float)e.NewSize.Width, Y = (float)e.NewSize.Height };

                viewModel.ChangeSizeCanvas.Execute(newSize);
            }
        }
    }
}
