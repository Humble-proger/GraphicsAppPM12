using static GraphicsApp.Views.Layers;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphicsApp.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowViewModel _main;

    public SettingsWindowViewModel(MainWindowViewModel main) {
        Main = main;
    }

}
