using System.Numerics;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Composition;
using System.Composition.Hosting;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

using IO;
using Geometry;
using System.Collections.Generic;


namespace GraphicsApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _mouseCoord = "X: 0.0 Y: 0.0";
    private string _canvasSize = "0 × 0";
    
    public ObservableCollection<ShapeViewModel> Figures { get; } = [];

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];

    public ICommand SaveJsonCommand { get; }
    public RelayCommand LoadJsonCommand { get; }

    [ImportMany]
    private IEnumerable<ExportFactory<IShape>> ModelFactories { get; set; } = [];
    
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
        var configuration = new ContainerConfiguration()
            .WithAssembly(typeof(IShape).Assembly);
        using var container = configuration.CreateContainer();
        container.SatisfyImports(this);

        foreach (var factory in ModelFactories)
        {
            Factories.Add(new() { Factory = factory, Main = this });
        }
        ChangeMouseCoord = new RelayCommand(OnMouseMove);
        ChangeSizeCanvas = new RelayCommand(OnResizeCanvas);
    }

    private void LoadFigures(IEnumerable<ShapeViewModel>? figures)
    {
        if (figures is null)
            return;

        Figures.Clear();
        foreach (var fig in figures)
        {
            fig.Main = this;
            Figures.Add(fig);
        }
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
