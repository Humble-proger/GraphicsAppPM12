using Avalonia.Media;

namespace IO.interfaces;

public class ShapeData
{
    public string Type { get; set; } = "";
    public double Width { get; set; }
    public double Height { get; set; }
    public double CenterX { get; set; }
    public double CenterY { get; set; }
    public string Stroke { get; set; } = "";
    public double StrokeThickness { get; set; }
    public string Fill { get; set; } = "";
}