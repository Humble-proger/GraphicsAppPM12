using Avalonia.Controls;

using IO.interfaces;

namespace IO.exporters;

public class RasterExporter: BaseExporter
{
    public override void Export(Canvas canvas, string path)
    {
        Console.WriteLine("RasterExporter\n");
        var shapeData = ConvertCanvasToListShapeData(canvas);
        foreach (var item in shapeData)
        {
            Console.WriteLine(item.Type);
            Console.WriteLine(item.Width);
            Console.WriteLine(item.Height);
            Console.WriteLine(item.Fill);
            Console.WriteLine(item.Stroke);
            Console.WriteLine(item.CenterX);
            Console.WriteLine(item.CenterY);
            Console.WriteLine("\n");
        }
    }
}