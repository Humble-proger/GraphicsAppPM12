using Avalonia;

using IO.exporters;

namespace IO;

public class IO1
{
    static void Main(string[] args)
    {
        BuildAvaloniaApp().SetupWithoutStarting();
        Console.WriteLine("Main");
        var canvas = CanvasFactory.CreateTestCanvas();
        var exporter = new RasterExporter(); 
        exporter.Export(canvas, "test_output.png");

        Console.WriteLine("End");
    }
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}