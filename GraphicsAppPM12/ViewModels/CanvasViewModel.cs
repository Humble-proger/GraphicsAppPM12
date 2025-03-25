using System.Globalization;
using System;
using System.Numerics;
using System.Windows.Input;

using Avalonia.Data.Converters;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Avalonia.Controls;

namespace GraphicsApp.ViewModels
{

    public partial class CanvasViewModel : ViewModelBase
    {
        private double _originalWidth = 1000;
    
        private double _originalHeight = 500;

        [ObservableProperty]
        private Canvas _mainCanvas;

        public double OriginalWidth {
            get => _originalWidth;
            set {
                _originalWidth = value;
                OnPropertyChanged(nameof(OriginalWidth));
                OnPropertyChanged(nameof(ScaledWidth));
            }
        }

        public double OriginalHeight
        {
            get => _originalHeight;
            set
            {
                _originalHeight = value;
                OnPropertyChanged(nameof(OriginalHeight));
                OnPropertyChanged(nameof(ScaledHeight));
            }
        }

        [ObservableProperty]
        private MainWindowViewModel? _main;

        public ICommand ChangeMouseCoord { get; }
        public ICommand MouseLeaveCanvas { get; }
        public ICommand ChangeSizeCanvas { get; }

        public double ScaledWidth => OriginalWidth * ZoomFactor;

        public double ScaledHeight => OriginalHeight * ZoomFactor;


        public CanvasViewModel(MainWindowViewModel? main)
        {
            Main = main;
            ChangeMouseCoord = new RelayCommand<Avalonia.Point>(OnMouseMove);
            MouseLeaveCanvas = new RelayCommand(OnMouseLeave);
            ChangeSizeCanvas = new RelayCommand<Vector2>(OnResizeCanvas);
        }
    
        private void OnMouseMove(Avalonia.Point Point)
        {
            if (Main is not null)
                Main.Footerview.MouseCoords = $"X: {Point.X,5:F1}, Y {Point.Y, 5:F1}";
        }

        private void OnMouseLeave()
        {
            if (Main is not null)
                Main.Footerview.MouseCoords = "";
        }

        private void OnResizeCanvas(Vector2 size)
        {
            if (Main is not null)
                Main.Footerview.CanvasSize = $"{size.X} x {size.Y}";
            OriginalHeight = size.Y;
            OriginalWidth = size.X;

        }
        private double _zoomFactor = 1.0;

        public double ZoomFactor
        {
            get => _zoomFactor;
            set { 
                SetProperty(ref _zoomFactor, value);
                if (Main is not null)
                    Main.Footerview.ZoomString = $"{Math.Round(value * 100)}%";
                OnPropertyChanged(nameof(ScaledHeight));
                OnPropertyChanged(nameof(ScaledWidth));
            }
        }

        public void Zoom(double delta)
        {
            var temp = ZoomFactor + delta;
            if (temp < 5 && temp > 0.1)
            {
                ZoomFactor = temp;
            }
        }
    }
    public class OffsetConverter : IValueConverter
    {
        public static OffsetConverter Instance { get; } = new OffsetConverter();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double coord && parameter is string offsetStr && double.TryParse(offsetStr, out double offset))
            {
                return coord - offset;
            }
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
