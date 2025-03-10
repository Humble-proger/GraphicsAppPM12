using System.Numerics;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Composition;
using System.Composition.Hosting;

using CommunityToolkit.Mvvm.Input;
using Geometry;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;


namespace GraphicsApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _mouseCoords = "X: 0.0 Y: 0.0";

    [ObservableProperty]
    private string _canvasSize = "0 × 0 мм";
    
    public ObservableCollection<ShapeViewModel> Figures { get; } = [];

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];

    public ICommand SaveJsonCommand { get; }
    public RelayCommand LoadJsonCommand { get; }

    [ImportMany]
    private IEnumerable<ExportFactory<IShape>> ModelFactories { get; set; } = [];
    
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
        ChangeMouseCoord = new RelayCommand<Avalonia.Point>(OnMouseMove);
        ChangeSizeCanvas = new RelayCommand<Vector2>(OnResizeCanvas);
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
    private void OnMouseMove(Avalonia.Point point) {
        MouseCoords = $"X: {point.X} Y: {point.Y}";
    }
    private void OnResizeCanvas(Vector2 size) 
    {
        CanvasSize = $"{size.X} × {size.Y} мм";
    }

    public CanvasViewModel CanvasViewModel { get; } = new CanvasViewModel();
}
