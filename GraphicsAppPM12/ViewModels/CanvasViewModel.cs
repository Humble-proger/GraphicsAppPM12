
using System.Numerics;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GraphicsApp.Views;

namespace GraphicsApp.ViewModels;

public partial class CanvasViewModel : ViewModelBase
{

    [ObservableProperty]
    private MainWindowViewModel? _main;

    public ICommand ChangeMouseCoord { get; }
    public ICommand MouseLeaveCanvas { get; }
    public ICommand ChangeSizeCanvas { get; }
    

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
    }
    private double _zoomFactor = 1.0;

    public double ZoomFactor
    {
        get => _zoomFactor;
        set => SetProperty(ref _zoomFactor, value);
    }

    public void Zoom(double delta)
    {
        ZoomFactor += delta;
        if (ZoomFactor < 0.1) ZoomFactor = 0.1;

    }
}