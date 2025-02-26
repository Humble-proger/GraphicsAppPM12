using System.Composition;
using Avalonia.Controls;
using Avalonia.Media;

namespace Geometry;

[Export(typeof(IShape))]
[ExportMetadata("Name", nameof(MyRectangle))]
public class MyRectangle : Avalonia.Controls.Shapes.Rectangle, IShapeNamed
{
    public MyRectangle() { }
    public MyRectangle(double x = 0, double y = 0)
    {
        Canvas.SetLeft(this, x);
        Canvas.SetTop(this, y);
    }
    public double CenterX
    {
        set { CenterX = value; }
        get { return Canvas.GetLeft(this) + Width / 2; }
    }

    public double CenterY
    {
        set { CenterY = value; }
        get { return Canvas.GetTop(this) + Height / 2; }
    }

    double IShape.StrokeThickness
    {
        get { return base.StrokeThickness; }
        set { base.StrokeThickness = value; }
    }

    IBrush IShape.Stroke
    {
        get { return base.Stroke; }
        set { base.Stroke = value; }
    }

    IBrush IShape.Fill
    {
        get { return base.Fill; }
        set { base.Fill = value; }
    }

    double IShapeNamed.Width
    {
        get { return base.Width; }
        set { base.Width = value; }
    }
    double IShapeNamed.Height
    {
        get { return base.Height; }
        set { base.Height = value; }
    }

    public void SetColor(Color color)
    {
        Fill = new SolidColorBrush(color);
    }

    public void SetStroke(Color color)
    {
        Stroke = new SolidColorBrush(color);
    }

    public void SetThickness(double thickness)
    {
        StrokeThickness = thickness;
    }
}