using System;

using Avalonia.Controls;
using Avalonia.Interactivity;

using Geometry;

using GraphicsApp.ViewModels;

namespace GraphicsApp.Views
{
    public partial class ToolBars : UserControl
    {
        public ToolBars()
        {
            InitializeComponent();
        }

        private bool _isClearingSelection = false;

        // Обработчик для кнопки "Курсор"
        private void CursorButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(cursor: true);
            if (DataContext is ToolBarsViewModel viewmodel && viewmodel.Main is not null)
                viewmodel.Main.SelectTool = Tools.Cursor;
            //ThicknessPopupControl.ClosePopup();
            ClearShapeSelection();
        }

        // Обработчик для кнопки "Выделить"
        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(selection: true);
            //ThicknessPopupControl.ClosePopup();
            ClearShapeSelection();
        }

        // Обработчик для кнопки "Перо"
        private void PenButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(pen: true);
            if (DataContext is ToolBarsViewModel viewmodel && viewmodel.Main is not null)
            {
                viewmodel.Main.SelectTool = Tools.Pen;
            }
            //ThicknessPopupControl.ClosePopup();
            ClearShapeSelection();
        }

        // Обработчик для кнопки "Заливка"
        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(fill: true);
            if (DataContext is ToolBarsViewModel viewmodel && viewmodel.Main is not null)
            {
                viewmodel.Main.SelectTool = Tools.Fill;
                viewmodel.Main.SelectedFigure = null;
            }
            //ThicknessPopupControl.ClosePopup();
            ClearShapeSelection();
        }

        // Обработчик для кнопки "Поворот"
        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(rotate: true);
            if (DataContext is ToolBarsViewModel viewmodel && viewmodel.Main is not null)
            {
                viewmodel.Main.SelectTool = Tools.Rotate;
                viewmodel.Main.SelectedFigure = null;
            }
            //ThicknessPopupControl.ClosePopup();
            ClearShapeSelection();
        }
        private void ThicknessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThicknessButton.IsChecked == true)
            {
                ThicknessButton.Flyout.ShowAt(ThicknessButton);
                CursorButton.IsChecked = false;
                SelectionButton.IsChecked = false;
                FillButton.IsChecked = false;
                RotateButton.IsChecked = false;
                if (DataContext is ToolBarsViewModel viewmodel && viewmodel.Main is not null)
                {
                    viewmodel.Main.SelectTool = Tools.None;
                    viewmodel.Main.SelectedFigure = null;
                }
            }
            else
            {
                ThicknessButton.Flyout.Hide();
            }
        }

        // Обработчик для областм "Фигуры"
        private void ShapeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isClearingSelection)
            {
                SetButtonStates(shapeSelected: true);
                if (DataContext is ToolBarsViewModel viewmodel && viewmodel.Main is not null) {
                    viewmodel.Main.SelectTool = Tools.SelectFigure;
                    if (viewmodel.Main.SelectedFigure is not null)
                    {
                        if (viewmodel.Main.SelectedFigure.Model is not BezierCurveModel)
                            viewmodel.Main.SelectedFigure = null;
                    }
                    else
                        viewmodel.Main.SelectedFigure = null;

                }
                //ThicknessPopupControl.ClosePopup();
            }
        }

        // Скролл для области "Фигуры"
        void ScrollListUp(object sender, RoutedEventArgs e)
        {
            if (ShapeScroll != null)
            {
                ShapeScroll.Offset = new Avalonia.Vector(0, Math.Max(ShapeScroll.Offset.Y - 41, 0));
            }
        }

        private void ScrollListDown(object sender, RoutedEventArgs e)
        {
            if (ShapeScroll != null)
            {
                double maxOffset = ShapeScroll.Extent.Height - ShapeScroll.Viewport.Height;
                ShapeScroll.Offset = new Avalonia.Vector(0, Math.Min(ShapeScroll.Offset.Y + 41, maxOffset));
            }
        }

        // Helper methods to reduce code duplication
        private void SetButtonStates(bool cursor = false, bool selection = false, bool pen = false,
            bool fill = false, bool rotate = false, bool thickness = false, bool shapeSelected = false)
        {
            CursorButton.IsChecked = cursor;
            SelectionButton.IsChecked = selection;
            PenButton.IsChecked = pen;
            FillButton.IsChecked = fill;
            RotateButton.IsChecked = rotate;
            ThicknessButton.IsChecked = thickness;
        }

        private void ClearShapeSelection()
        {
            _isClearingSelection = true;
            ShapeList.SelectedItem = null;
            _isClearingSelection = false;
        }
        private void ThicknessFlyoutControl_ThicknessChanged(object sender, int thickness)
        {
            if (DataContext is ToolBarsViewModel viewModel)
            {
                viewModel.LineThickness = thickness;
            }
            Console.WriteLine($"Толщина изменена на: {thickness}");
        }

        private void ThicknessFlyoutControl_CloseFlyoutRequested(object sender, EventArgs e)
        {
            ThicknessButton.IsChecked = false;
            ThicknessButton.Flyout.Hide();
        }


        private void OnColorChangedFill(object? sender, ColorChangedEventArgs e)
        {
            if (DataContext is ToolBarsViewModel viewModel)
            {
                viewModel.SelectedColor = e.NewColor;
            }
        }
        
        private void OnColorChangedBorder(object? sender, ColorChangedEventArgs e)
        {
            if (DataContext is ToolBarsViewModel viewModel)
            {
                viewModel.OutlineColor = e.NewColor;
            }
        }
    }
}
