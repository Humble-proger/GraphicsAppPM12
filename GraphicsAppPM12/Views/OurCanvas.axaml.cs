using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;

using GraphicsApp.ViewModels;

namespace GraphicsApp.Views
{
    public partial class OurCanvas : UserControl
    {
        private Avalonia.Point _point;
        public OurCanvas()
        {
            InitializeComponent();
        }

        private void Canvas_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            _point = e.GetCurrentPoint(CanvasName).Position;

            //var figure = FigureFabric.CreateFigure("MyEllipse");

            // Создаем точку (эллипс) с радиусом 5
            var point = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red,

            };

            Canvas.SetLeft(point, _point.X - 5); // Центрируем точку относительно клика
            Canvas.SetTop(point, _point.Y - 5);
            // Добавляем точку на Canvas
            CanvasName.Children.Add(point);
            //var figureShape = figure as Shape;
            //if (figureShape != null)
            //    CanvasName.Children.Add(figureShape);
        }
    }
}
