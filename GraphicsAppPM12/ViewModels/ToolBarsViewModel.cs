using System;
using System.Collections.ObjectModel;

using Avalonia.Media.Imaging;
using Avalonia.Platform;

using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphicsApp.ViewModels
{
public partial class ToolBarsViewModel : ViewModelBase
{
    [ObservableProperty]
    private Color _selectedColor = Colors.Red;

    [ObservableProperty]
    private Color _outlineColor = Colors.Black; // Новое свойство для цвета контура

    [ObservableProperty]
    private double _lineThickness = 0.0;

    public ObservableCollection<ShapeItem> Shapes { get; set; }

    public ToolBarsViewModel()
    {
        Shapes = new ObservableCollection<ShapeItem>
        {
            new ShapeItem("Линия", "line2.png"),
            new ShapeItem("Ломаная", "polyline2.png"),
            new ShapeItem("Квадрат", "square.png"),
            new ShapeItem("Треугольник", "triangle.png"),
            new ShapeItem("Круг", "circle.png"),
            new ShapeItem("Круг2", "circle.png"),
            new ShapeItem("Круг3", "circle.png"),
            new ShapeItem("Круг4", "circle.png")
        };
    }
}
    public class ShapeItem
    {
        public string Name { get; }
        public Bitmap Icon { get; }

        public ShapeItem(string name, string iconFileName)
        {
            Name = name;
            Icon = LoadBitmap($"avares://GraphicsApp/Views/pictures/ToolBars/{iconFileName}");
        }

        private Bitmap LoadBitmap(string assetPath)
        {
            try
            {
                var uri = new Uri(assetPath);
                var asset = AssetLoader.Open(uri);
                return new Bitmap(asset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки {assetPath}: {ex.Message}");
                return null;
            }
        }
    }
}