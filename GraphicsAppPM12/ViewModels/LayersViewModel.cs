using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

using Avalonia.Controls;


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GraphicsApp.ViewModels;

public partial class LayersViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowViewModel? _main;

    public ICommand SelectFigure { get; }

    public LayersViewModel(MainWindowViewModel? main)
    {
        Main = main;
        SelectFigure = new RelayCommand<ShapeViewModel>((figure) => {
            if (Main is not null && figure is not null) {
                if (Main.SelectedFigure is not null)
                    Main.SelectedFigure.Active = false;
                Main.SelectedFigure = figure;
                figure.Active = true;
            }

        });
    }

}
