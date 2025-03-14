using System.Numerics;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Composition;
using System.Composition.Hosting;

using CommunityToolkit.Mvvm.Input;
using Geometry;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Templates;
using VecEditor.IO;
using System.IO;


namespace GraphicsApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private FooterViewModel _footerview;

    [ObservableProperty]
    private CanvasViewModel _canvasview;
    
    [ObservableProperty]
    private ToolBarsViewModel _toolbarsview;
    
    [ObservableProperty]
    private LayersViewModel _layersview;
    

    [ObservableProperty]
    private SettingsWindowViewModel _settingswindow;

    [ObservableProperty]
    private SettingsViewModel _settings;

    public ObservableCollection<ShapeViewModel> Figures { get; } = [];
    

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];
    
    [ObservableProperty]
    private ModelFactoryViewModel? _selectedButtonFigure;
    
    [ObservableProperty]
    private ShapeViewModel? _selectedFigure;
    
    public ICommand SaveJsonCommand { get; }
    public ICommand LoadJsonCommand { get; }

    [ImportMany]
    private IEnumerable<ExportFactory<IShape, ModelMetadata>> ModelFactories { get; set; } = [];

    private readonly GeometryJsonSerializer<ShapeViewModel> _geometryJsonSerializer;
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
        Layersview = new(this);

        Toolbarsview = new(this);
        Settingswindow = new(this);
        Settings = new(this);
        _geometryJsonSerializer = new();

        SaveJsonCommand = new RelayCommand<string>(
            (filePath) => { _geometryJsonSerializer.SaveJson(filename: filePath, Figures); });
        
        LoadJsonCommand = new RelayCommand<string>( (FilePath) => LoadFigures(_geometryJsonSerializer.LoadJson(FilePath)) );

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
