using System;
using System.Collections.Generic;
using Avalonia;
using IO.exporters;
using VecEditor.IO;

namespace IO;

public class IO1
{
    static void Main(string[] args)
    {
        BuildAvaloniaApp().SetupWithoutStarting();
        Console.WriteLine("Main");

        // Работа с JSON
        string filename = "shapes.json";
        var serializer = new GeometryJsonSerializer<Shape>();

        var shapes = serializer.LoadJson(filename);
        if (shapes == null)
        {
            Console.WriteLine("Файл не найден. Создаём новый...");

            // Создаём список фигур
            shapes = new List<Shape>
            {
                new Shape { Name = "Circle", Sides = 0 },
                new Shape { Name = "Square", Sides = 4 }
            };

            // Экспортируем в JSON
            serializer.SaveJson(filename, shapes);
            Console.WriteLine($"Создан файл {filename} с начальными данными.");
        }
        else
        {
            Console.WriteLine("Загружены фигуры из JSON:");
            foreach (var shape in shapes)
            {
                Console.WriteLine($"Фигура: {shape.Name}, Количество сторон: {shape.Sides}");
            }
        }

        // Работа с Растровым форматом
        var canvas = CanvasFactory.CreateTestCanvas();
        var exporter = new RasterExporter(); 
        exporter.Export(canvas, "test_output.png");

        Console.WriteLine("End");

        // Работа с SVG
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}

// Класс фигуры для теста. проблемы с доступом из проекта io к ishape'ам из geometry
public class Shape
{
    public string Name { get; set; }
    public int Sides { get; set; }
}
