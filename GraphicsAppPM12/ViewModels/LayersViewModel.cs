using System.Collections.ObjectModel;
using System.ComponentModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphicsApp.ViewModels;

public partial class LayersViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowViewModel? _main;

    public ObservableCollection<Layer> LayersList { get; set; }

    public LayersViewModel(MainWindowViewModel? main)
    {
        Main = main;
        LayersList = new ObservableCollection<Layer>() {
            new() { LayerName = "���� 1", IsVisibleIcon=true },
            new() { LayerName = "���� 2", IsVisibleIcon=true },
            new() { LayerName = "���� 3", IsVisibleIcon=true }
        };
    }

}

public partial class Layer : ObservableObject
{
    [ObservableProperty]
    private string _layerName;

    [ObservableProperty]
    private bool _isVisibleIcon;

}