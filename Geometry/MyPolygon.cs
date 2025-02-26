using System.Composition;
using Avalonia.Controls;
using Avalonia.Media;

namespace Geometry;

[Export(typeof(IShape))]
[ExportMetadata("Name", nameof(MyPolygon))]
public class MyPolygon : Avalonia.Controls.Shapes.Polygon, IShapeOfPoints
{
    public MyPolygon() { }
    List<Avalonia.Point> IShapeOfPoints.Points
    {
        get { return (List<Avalonia.Point>) base.Points; }
        set { base.Points = value; }
    }
    public double CenterX
    {
        set { CenterX = value; }
        get { return getCenterX(); }
    }

    public double CenterY
    {
        set { CenterY = value; }
        get { return getCenterY(); }
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

    public void addPoint(Avalonia.Point point)
    {
        Points.Add(point);
    }

    public void deletePoint(Avalonia.Point point)
    {
        Points.Remove(point);
    }

    private double getCenterX()
    {
        double sum = 0.0;
        for (int i = 0; i < Points.Count; i++)
        {
            sum += Points[0].X;
        }

        return sum / Points.Count;
    }

    private double getCenterY()
    {
        double sum = 0.0;
        for (int i = 0; i < Points.Count; i++)
        {
            sum += Points[0].Y;
        }

        return sum / Points.Count;
    }

}
