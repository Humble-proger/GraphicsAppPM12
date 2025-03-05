using System.Numerics;
using System.Windows.Input;

namespace GraphicsApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _mouseCoord = "X: 0.0 Y: 0.0";
    private string _canvasSize = "0 × 0";
    
    public string MouseCoords {
        get => _mouseCoord;
        set {
            _mouseCoord = value;
            OnPropertyChanged(nameof(MouseCoords));
        }
    }
    public string CanvasSize {
        get => _canvasSize;
        set {
            _canvasSize = value;
            OnPropertyChanged(nameof(CanvasSize));
        }
    }
    public ICommand ChangeMouseCoord { get; }
    public ICommand ChangeSizeCanvas { get; }

    public MainWindowViewModel()
    {
        ChangeMouseCoord = new RelayCommand(OnMouseMove);
        ChangeSizeCanvas = new RelayCommand(OnResizeCanvas);
    }
    
    private void OnMouseMove(object point) {
        if (point is Avalonia.Point _point) {
            MouseCoords = $"X: {_point.X} Y: {_point.Y}";
        }
    }
    private void OnResizeCanvas(object size) 
    {
        if (size is Vector2 _size) {
            CanvasSize = $"{_size.X} × {_size.Y} мм";
        }
    }
}