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
        private MainWindowViewModel? _main;
        
        [ObservableProperty]
        private Color _selectedColor = Colors.Red;
        
        [ObservableProperty]
        private double _lineThickness = 0.0;
        
        public ObservableCollection<ShapeItem> Shapes { get; set; }
    
        
        public ToolBarsViewModel(MainWindowViewModel? main)
        {
            Main = main;
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