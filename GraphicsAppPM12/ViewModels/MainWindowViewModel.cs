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
    //public string Greeting { get; } = "Здравствуйте, тимлидер Дмитрий!";
    public ObservableCollection<ShapeViewModel> Figures { get; } = [];

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];

    public ICommand SaveJsonCommand { get; }
    public RelayCommand LoadJsonCommand { get; }

    [ImportMany]
    private IEnumerable<ExportFactory<IShape>> ModelFactories { get; set; } = [];

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
