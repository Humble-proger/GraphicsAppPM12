using Avalonia.Controls;
using Avalonia.Input;
using System.Numerics;

using Avalonia.Controls.Shapes;

using GraphicsApp.ViewModels;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia;

using Geometry;
using System.Drawing.Printing;
using Avalonia.Input.TextInput;
using System;
using Tmds.DBus.Protocol;
using Avalonia.Rendering.Composition;

namespace GraphicsApp.Views
{
    public partial class MyCanvas : UserControl
    {
        private enum Operation {
            Translate,
            Rotate,
            ScaleX,
            ScaleY,
            None
        }

        private bool _isReflectionX = false;
        private bool _isReflectionY = false;
        private double _startAngle = 0.0;
        private Avalonia.Point _startposition;
        private int _inversionScale = 1;
        private double _oldTheta = 0.0;

        private Operation _isFigureResize = Operation.None;
        public MyCanvas()
        {
            InitializeComponent();
        }

        private double CalcAngle(double deltaX, double deltaY) {
            double theta;
            if (double.Abs(deltaX) < double.Epsilon)
                if (deltaY >= 0)
                    theta = 90;
                else
                    theta = -90;
            else
            {
                theta = Math.Atan(deltaY / deltaX) * (180 / Math.PI);
                if (deltaX < 0)
                    theta += 180;
            }
            return theta;
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
            if (e.Handled)
                return;
            if (DataContext is CanvasViewModel viewModel && sender is Canvas _canvas && e.GetCurrentPoint(_canvas).Properties.IsLeftButtonPressed)
            {

                if (viewModel.Main is not null)
                {
                    if (viewModel.Main.SelectedFigure is not null) {
                        viewModel.Main.SelectedFigure.Active = false;
                        viewModel.Main.SelectedFigure = null;
                    }

                    if (viewModel.Main.SelectedButtonFigure is not null) {
                        var position = e.GetCurrentPoint(_canvas).Position;
                        viewModel.Main.SelectedButtonFigure.CreateCommand.Execute(position);
                    }
                }
                
            }
        }

        private void MarkingPressed(object sender, PointerPressedEventArgs e)
        {
            if (sender is Canvas canvas && e.GetCurrentPoint(canvas).Properties.IsLeftButtonPressed)
            {
                var path = canvas.Children[0] as Path;
                var rect = canvas.Children[1] as Rectangle;
                // Получаем объект фигуры из DataContext
                if (path == null || rect == null)
                    return;
                if (path.DataContext is ShapeViewModel figure)
                {
                    // Устанавливаем выбранную фигуру в ViewModel
                    if (DataContext is CanvasViewModel viewmodel)
                    {
                        if (viewmodel.Main is not null)
                        {
                            if (viewmodel.Main.SelectedFigure is null)
                            {
                                viewmodel.Main.SelectedFigure = figure;
                                figure.Active = true;
                            }
                            else if (viewmodel.Main.SelectedFigure != figure) {
                                viewmodel.Main.SelectedFigure.Active = false;
                                viewmodel.Main.SelectedFigure = figure;
                                figure.Active = true;
                            }
                            else {
                                var position = e.GetPosition(rect);
                                var thickness = rect.StrokeThickness;
                                var width = rect.Width;
                                var height = rect.Height;
                                

                                if (position.X <= thickness && position.Y <= thickness) // Левый верхний угол
                                {
                                    _isFigureResize = Operation.Rotate;
                                }
                                else if (position.X >= width - thickness && position.Y <= thickness) // Правый верхний угол
                                {
                                    _isFigureResize = Operation.Rotate;
                                }
                                else if (position.X <= thickness && position.Y >= height - thickness) // Левый нижний угол
                                {
                                    _isFigureResize = Operation.Rotate;
                                }
                                else if (position.X >= width - thickness && position.Y >= height - thickness) // Правый нижний угол
                                {
                                    _isFigureResize = Operation.Rotate;
                                }
                                // Проверка границ
                                else if (position.X <= thickness) // Левая граница
                                {
                                    _isFigureResize = Operation.ScaleX;
                                    _inversionScale = -1;
                                }
                                else if (position.X >= width - thickness) // Правая граница
                                {
                                    _isFigureResize = Operation.ScaleX;
                                    _inversionScale = 1;
                                }
                                else if (position.Y <= thickness) // Верхняя граница
                                {
                                    _isFigureResize = Operation.ScaleY;
                                    _inversionScale = -1;
                                }
                                else if (position.Y >= height - thickness) // Нижняя граница
                                {
                                    _isFigureResize = Operation.ScaleY;
                                    _inversionScale = 1;
                                }
                                else // Внутренняя область
                                {
                                    _isFigureResize = Operation.Translate;
                                    _startposition = e.GetPosition(path);
                                }
                                if (_isFigureResize == Operation.Rotate)
                                {
                                    position = e.GetPosition(path);
                                    _startAngle = CalcAngle(position.X - figure.Model.CenterX, figure.Model.CenterY - position.Y);
                                }
                            }
                            e.Handled = true;
                        }
                        
                    }
                    
                }
            }
        }

        private void MarkingMoved(object sender, PointerEventArgs e)
        {
            if (_isFigureResize != Operation.None && sender is Canvas canvas)
            {
                // Устанавливаем выбранную фигуру в ViewModel
                if (DataContext is CanvasViewModel viewmodel)
                {
                    if (viewmodel.Main is not null)
                    {
                        if (viewmodel.Main.SelectedFigure is null)
                            return;
                        var newPosition = e.GetPosition(canvas);
                        var oldPositionX = viewmodel.Main.SelectedFigure.Model.CenterX;
                        var oldPositionY = viewmodel.Main.SelectedFigure.Model.CenterY;
                        double deltaX, deltaY, _angle, theta;
                        switch (_isFigureResize) {
                            case Operation.Translate:
                                deltaX = newPosition.X - _startposition.X;
                                deltaY = newPosition.Y - _startposition.Y;
                                _startposition = new Point(_startposition.X + deltaX, _startposition.Y + deltaY);
                                viewmodel.Main.SelectedFigure.Model.Move((float) deltaX, (float) deltaY);
                                break;
                            case Operation.Rotate:
                                deltaX = newPosition.X - oldPositionX;
                                deltaY = newPosition.Y - oldPositionY;
                                theta = CalcAngle(deltaX, deltaY);
                                theta += _startAngle;
                                theta -= _oldTheta;
                                _oldTheta += theta;
                                viewmodel.Main.SelectedFigure.Model.Rotate((float) theta);
                                break;
                            case Operation.ScaleX:
                                _angle = viewmodel.Main.SelectedFigure.Model.Angle;
                                if (_angle < 0)
                                    _angle += 360;
                                deltaX = 2 * _inversionScale * (newPosition.X - oldPositionX) / (viewmodel.Main.SelectedFigure.Model.BoxWidth - viewmodel.Main.SelectedFigure.Model.StrokeThickness - 6);

                                if (deltaX < 0)
                                {
                                    if (_isReflectionX)
                                        viewmodel.Main.SelectedFigure.Model.Scale(float.Abs((float) deltaX), (float) 1.0);
                                    else
                                    {
                                        _isReflectionX = true;
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) deltaX, (float) 1.0);
                                    }
                                }
                                else if (deltaX > 0) {
                                    if (_isReflectionX)
                                    {
                                        _isReflectionX = false;
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) -deltaX, (float) 1.0);
                                    }
                                    else {
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) deltaX, (float) 1.0);
                                    }
                                }
                                break;
                            case Operation.ScaleY:
                                _angle = viewmodel.Main.SelectedFigure.Model.Angle;
                                if (_angle < 0)
                                    _angle += 360;
                                deltaY = 2 * _inversionScale * (newPosition.Y - oldPositionY) / (viewmodel.Main.SelectedFigure.Model.BoxHeight - viewmodel.Main.SelectedFigure.Model.StrokeThickness - 6);
                                
                                if (deltaY < 0)
                                {
                                    if (_isReflectionY)
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) 1.0, float.Abs((float) deltaY));
                                    else
                                    {
                                        _isReflectionY = true;
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) 1.0, (float) deltaY);
                                    }
                                }
                                else if (deltaY > 0)
                                {
                                    if (_isReflectionY)
                                    {
                                        _isReflectionY = false;
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) 1.0, (float) -deltaY);
                                    }
                                    else
                                    {
                                        viewmodel.Main.SelectedFigure.Model.Scale((float) 1.0, (float) deltaY);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    e.Handled = true;
                }

            }
        }

        private void MarkingReleased(object sender, RoutedEventArgs e) {
            _isFigureResize = Operation.None;
            _isReflectionX = false;
            _isReflectionY = false;
            _oldTheta = 0;
        }
        private void Canvas_Loaded(object sender, RoutedEventArgs e) {
            if (sender is Canvas canvas && DataContext is CanvasViewModel viewModel && viewModel.OriginalHeight > 0 && viewModel.OriginalWidth > 0)
            {
                // Устанавливаем Clip по размерам Canvas
                canvas.Clip = new RectangleGeometry(new Rect(0, 0, viewModel.OriginalWidth, viewModel.OriginalHeight));

            }
        }

        private void MarkingCursor(object sender, PointerEventArgs e)
        {
            
            if (_isFigureResize == Operation.None && sender is Rectangle marking && marking.DataContext is ShapeViewModel figure && figure.Active)
            {
                var position = e.GetPosition(marking);
                var thickness = marking.StrokeThickness;
                var width = marking.Width;
                var height = marking.Height;

                if (position.X <= thickness && position.Y <= thickness) // Левый верхний угол
                {
                    marking.Cursor = new Cursor(StandardCursorType.Hand); // Курсор поворота
                }
                else if (position.X >= width - thickness && position.Y <= thickness) // Правый верхний угол
                {
                    marking.Cursor = new Cursor(StandardCursorType.Hand); // Курсор поворота
                }
                else if (position.X <= thickness && position.Y >= height - thickness) // Левый нижний угол
                {
                    marking.Cursor = new Cursor(StandardCursorType.Hand); // Курсор поворота
                }
                else if (position.X >= width - thickness && position.Y >= height - thickness) // Правый нижний угол
                {
                    marking.Cursor = new Cursor(StandardCursorType.Hand); // Курсор поворота
                }
                // Проверка границ
                else if (position.X <= thickness) // Левая граница
                {
                    marking.Cursor = new Cursor(StandardCursorType.SizeWestEast);
                }
                else if (position.X >= width - thickness) // Правая граница
                {
                    marking.Cursor = new Cursor(StandardCursorType.SizeWestEast);
                }
                else if (position.Y <= thickness) // Верхняя граница
                {
                    marking.Cursor = new Cursor(StandardCursorType.SizeNorthSouth);
                }
                else if (position.Y >= height - thickness) // Нижняя граница
                {
                    marking.Cursor = new Cursor(StandardCursorType.SizeNorthSouth);
                }
                else // Внутренняя область
                {
                    marking.Cursor = new Cursor(StandardCursorType.SizeAll);
                }
            }
        }
    }
}
