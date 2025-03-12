using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphicsApp.ViewModels;

public partial class LayersViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowViewModel? _main;

    public LayersViewModel(MainWindowViewModel? main)
    {
        Main = main;
    }
    
}