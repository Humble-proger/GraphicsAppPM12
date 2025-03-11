using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.IO;
using System.Runtime.CompilerServices;

using Avalonia.Media;

namespace GraphicsApp.ViewModels
{
    public class ToolBarsViewModel : ViewModelBase
    {
        private Color _selectedColor = Colors.Red;
        private double _lineThickness = 1.0;
        
        public ObservableCollection<ShapeItem> Shapes { get; set; }

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                   Console.WriteLine(_selectedColor);
                    _selectedColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public double LineThickness
        {
            get => _lineThickness;
            set
            {
                if (_lineThickness != value)
                {
                    Console.WriteLine(value);
                    _lineThickness = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
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