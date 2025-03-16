using System.Collections.ObjectModel;
using System.ComponentModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphicsApp.ViewModels;

public partial class LayersViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowViewModel? _main;

    [ObservableProperty]
    private string _layerName;

    [ObservableProperty]
    private bool _isVisibleIcon;

    public LayersViewModel(MainWindowViewModel? main)
    {
        Main = main;
    }

}
