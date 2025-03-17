
using System.Globalization;
using System;
using System.Numerics;
using System.Windows.Input;

using Avalonia.Data.Converters;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GraphicsApp.Views;

namespace GraphicsApp.ViewModels;

public partial class CanvasViewModel : ViewModelBase
{
    private double _originalWidth = 1000;
    
    private double _originalHeight = 500;

    public double OriginalWidth {
        get => _originalWidth;
        set {
            _originalWidth = value;
            OnPropertyChanged(nameof(OriginalWidth));
            UpdateScaledSize();
        }
    }

    public double OriginalHeight
    {
        get => _originalHeight;
        set
        {
            _originalHeight = value;
            OnPropertyChanged(nameof(OriginalHeight));
            UpdateScaledSize();
        }
    }

    [ObservableProperty]
    private MainWindowViewModel? _main;

    public ICommand ChangeMouseCoord { get; }
    public ICommand MouseLeaveCanvas { get; }
    public ICommand ChangeSizeCanvas { get; }

    [ObservableProperty]
    private double _scaledWidth;
    [ObservableProperty]
    private double _scaledHeight;


    public CanvasViewModel(MainWindowViewModel? main)
    {
        Main = main;
        ChangeMouseCoord = new RelayCommand<Avalonia.Point>(OnMouseMove);
        MouseLeaveCanvas = new RelayCommand(OnMouseLeave);
        ChangeSizeCanvas = new RelayCommand<Vector2>(OnResizeCanvas);
        UpdateScaledSize();
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

        UpdateScaledSize();

    }
    private double _zoomFactor = 1.0;

    public double ZoomFactor
    {
        get => _zoomFactor;
        set { 
            SetProperty(ref _zoomFactor, value);
            UpdateScaledSize(); 
        }
    }

    private void UpdateScaledSize()
    {
        ScaledWidth = OriginalWidth * ZoomFactor;
        ScaledHeight = OriginalHeight * ZoomFactor;
    }

    public void Zoom(double delta)
    {
        ZoomFactor += delta;
        if (ZoomFactor < 0.1) ZoomFactor = 0.1;

    }
}

public class NegativeValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double numericValue)
        {
            return -numericValue; // �������� �� -1
        }
        return value; // ���������� �������� ��������, ���� ��� �� ��������
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double numericValue)
        {
            return -numericValue; // �������� �� -1 ��� �������� ��������������
        }
        return value; // ���������� �������� ��������, ���� ��� �� ��������
    }
}