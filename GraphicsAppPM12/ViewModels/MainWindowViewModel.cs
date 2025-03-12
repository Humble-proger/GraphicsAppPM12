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
    private FooterViewModel _footerview;

    [ObservableProperty]
    private CanvasViewModel _canvasview;
    
    [ObservableProperty]
    private ToolBarsViewModel _toolbarsview;

    public ObservableCollection<ShapeViewModel> Figures { get; } = [];
    

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];
    
    [ObservableProperty]
    private ModelFactoryViewModel? _selectedButtonFigure;
    
    public ICommand SaveJsonCommand { get; }
    public RelayCommand LoadJsonCommand { get; }

    [ImportMany]
    private IEnumerable<ExportFactory<IShape, ModelMetadata>> ModelFactories { get; set; } = [];
    

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
        Footerview = new(this);
        Canvasview = new(this);
        Toolbarsview = new (this);
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
}
