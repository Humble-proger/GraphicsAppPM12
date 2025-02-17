using Avalonia.Media;

public class Line : Avalonia.Controls.Shapes.Line
{
    public Line(double x1, double y1, double x2, double y2, double thickness = 1, string color = "#000000")
    {
        StartPoint = new Avalonia.Point(x1, y1);
        EndPoint = new Avalonia.Point(x2, y2);
        Stroke = new SolidColorBrush(Color.Parse(color));
        StrokeThickness = thickness;
    }
}