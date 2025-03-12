﻿using Avalonia.Controls;
using Avalonia.Input;
using System.Numerics;
using GraphicsApp.ViewModels;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia;

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
                if (viewModel.OriginalHeight > 0 && viewModel.OriginalWidth > 0 && sender is Canvas canvas) {
                    canvas.Clip = new RectangleGeometry(new Rect(0, 0, viewModel.OriginalWidth, viewModel.OriginalHeight));
                }
            }
        }

        private void CreateFigure(object sender, PointerPressedEventArgs e)
        {
            if (sender is Canvas _canvas && e.GetCurrentPoint(_canvas).Properties.IsLeftButtonPressed)
            if (DataContext is CanvasViewModel viewModel)
            {
                var position = e.GetCurrentPoint(_canvas).Position;
                viewModel.Main?.SelectedButtonFigure?.CreateCommand.Execute(position);
            }
        }
        private void Canvas_Loaded(object sender, RoutedEventArgs e) {
            if (sender is Canvas canvas && DataContext is CanvasViewModel viewModel && viewModel.OriginalHeight > 0 && viewModel.OriginalWidth > 0)
            {
                // Устанавливаем Clip по размерам Canvas
                canvas.Clip = new RectangleGeometry(new Rect(0, 0, viewModel.OriginalWidth, viewModel.OriginalHeight));
            }
        }
    }
}
