using Avalonia.Controls;
using Avalonia.Controls.Shapes;

using IO.interfaces;

namespace IO.exporters;

public abstract class BaseExporter: IExporter
{
    protected List<ShapeData> ConvertCanvasToListShapeData(Canvas canvas)
    {
        var shapes = new List<ShapeData>();
        foreach (var child in canvas.Children)
        {
            switch (child)
            {
                case Rectangle rect:
                    shapes.Add(new ShapeData
                    {
                        Type = "Rectangle",
                        Width = rect.Width,
                        Height = rect.Height,
                        CenterX = Canvas.GetLeft(rect),
                        CenterY = Canvas.GetTop(rect),
                        Fill = rect.Fill?.ToString() ?? "#FFFFFF",
                        Stroke = rect.Stroke?.ToString() ?? "#000000",
                        StrokeThickness = rect.StrokeThickness
                    });
                    break;
                case Ellipse ellipse:
                    shapes.Add(new ShapeData
                    {
                        Type = "Ellipse",
                        Width = ellipse.Width,
                        Height = ellipse.Height,
                        CenterX = Canvas.GetLeft(ellipse),
                        CenterY = Canvas.GetTop(ellipse),
                        Fill = ellipse.Fill?.ToString() ?? "#FFFFFF",
                        Stroke = ellipse.Stroke?.ToString() ?? "#000000",
                        StrokeThickness = ellipse.StrokeThickness
                    });
                    break;

                default:
                    Console.WriteLine($"Неизвестный тип фигуры: {child.GetType().Name}");
                    break;
            }
        }
        return shapes;
    }
    public abstract void Export(Canvas canvas, string path);
}