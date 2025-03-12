using Avalonia.Controls;
using Avalonia.Input;
using System.Numerics;

using Avalonia.Controls.Shapes;

using GraphicsApp.ViewModels;
using Avalonia.Interactivity;

using Geometry;

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
                Vector2 newSize = new Vector2() { X = (float) e.NewSize.Width, Y = (float) e.NewSize.Height };
                viewModel.ChangeSizeCanvas.Execute(newSize);
            }
        }

        private void CreateFigure(object sender, PointerPressedEventArgs e)
        {
            if (DataContext is CanvasViewModel viewModel)
            {
                var position = e.GetPosition((Canvas) sender);
                if (viewModel.Main.SelectedButtonFigure != null)
                    viewModel.Main.SelectedButtonFigure.CreateCommand.Execute(position);
            }
        }

        private void FigureEdit(object? sender, PointerPressedEventArgs e)
        {
            if (sender is Path path)
            {
                // Получаем объект фигуры из DataContext
                if (path.DataContext is ShapeViewModel figure)
                {
                    // Устанавливаем выбранную фигуру в ViewModel
                    if (DataContext is CanvasViewModel viewmodel)
                    {
                        viewmodel.Main.SelectedFigure = figure;
                    }
                }
            }
        }
    }
}
