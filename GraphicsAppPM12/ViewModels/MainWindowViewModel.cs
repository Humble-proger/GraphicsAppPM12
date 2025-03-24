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

public enum Tools
{
    Cursor,
    Pen,
    Fill,
    Rotate,
    SelectFigure,
    None
}


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

    [ObservableProperty]
    private Tools _selectTool = Tools.None;

    public ObservableCollection<ShapeViewModel> Figures { get; } = [];
    

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];
    
    [ObservableProperty]
    private ModelFactoryViewModel? _selectedButtonFigure;
    
    private ShapeViewModel? _selectedFigure;

    public ShapeViewModel? SelectedFigure {
        get => _selectedFigure;
        set {
            if (_selectedFigure is not null)
                _selectedFigure.Active = false;
            if (value is not null)
                value.Active = true;
            _selectedFigure = value;
            OnPropertyChanged(nameof(SelectedFigure));
        }
    }
    
    public ICommand SaveJsonCommand { get; }
    public ICommand LoadJsonCommand { get; }
    public ICommand SaveSvgCommand { get; }
    public ICommand SavePngCommand { get; }
    public ICommand ToDefaultSettingsAndClearCanvas { get; }

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

        SaveSvgCommand = new RelayCommand<string>(SaveToSVG);
        SavePngCommand = new RelayCommand<string>(SaveToPng);
        ToDefaultSettingsAndClearCanvas = new RelayCommand(ClearCanvas);

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

    private void LoadFigures(IEnumerable<ShapeViewModel>? figures)
    {
        if (figures is null)
            return;

        ToDefaultSettingsAndClearCanvas.Execute(null);
        foreach (var fig in figures)
        {
            fig.Main = this;
            fig.Active = false;

            Figures.Add(fig);
        }
    }

    private void SaveToSVG(string? filepath) {
        if (filepath is null) return;
        if (Figures.Count < 1) return;
        List<IShape> shapes = new List<IShape>();
        foreach (var fig in Figures) {
            shapes.Add(fig.Model);
        }
        SvgExporter.Export(filepath, new Vector2(x: (float) Canvasview.OriginalWidth, y: (float) Canvasview.OriginalHeight), shapes);
        if (!File.Exists(filepath)) Debug.WriteLine($"Warning: ���� {filepath} �� ��� ������!") ;
    }

    private void SaveToPng(string? filepath) {
        if (filepath is null) return;
        RasterExporter.Export(Canvasview.MainCanvas, filepath);
        if (!File.Exists(filepath)) Debug.WriteLine($"Warning: ���� {filepath} �� ��� ������!");
    }

    private void ClearCanvas() {
        Figures.Clear();
        SelectedFigure = null;
        SelectedButtonFigure = null;
        Canvasview.MainCanvas.Background = new SolidColorBrush(Colors.White);
        Canvasview.OriginalHeight = 500;
        Canvasview.OriginalWidth = 1000;
        Canvasview.ZoomFactor = 1;
        Toolbarsview.LineThickness = 0;
        Toolbarsview.OutlineColor = Colors.Black;
        Toolbarsview.SelectedColor = Colors.Red;
    }
}
