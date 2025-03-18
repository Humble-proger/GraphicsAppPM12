using System;
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
    public ICommand MoveUpFigure { get; }
    public ICommand MoveDownFigure { get; }

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
        MoveUpFigure = new RelayCommand<ShapeViewModel>(OnMoveUpButtonClick);
        MoveDownFigure = new RelayCommand<ShapeViewModel>(OnMoveDownButtonClick);
    }

    private void OnMoveUpButtonClick(ShapeViewModel figure)
    { 
        var indexEl = Main.Figures.IndexOf(figure);
        if (indexEl > 0)
        {
            // Свап
            (Main.Figures[indexEl], Main.Figures[indexEl - 1]) = (Main.Figures[indexEl - 1], Main.Figures[indexEl]);

            // После свапа меняем выделенную фигуру
            Main.Figures[indexEl].Active = false;
            Main.SelectedFigure = Main.Figures[indexEl - 1];
            Main.Figures[indexEl - 1].Active = true;
        }
    }

    private void OnMoveDownButtonClick(ShapeViewModel figure)
    {
        var indexEl = Main.Figures.IndexOf(figure);
        if (indexEl < Main.Figures.Count - 1)
        {
            // Свап
            (Main.Figures[indexEl], Main.Figures[indexEl + 1]) = (Main.Figures[indexEl + 1], Main.Figures[indexEl]);

            // После свапа меняем выделенную фигуру
            Main.Figures[indexEl].Active = false;
            Main.SelectedFigure = Main.Figures[indexEl + 1];
            Main.Figures[indexEl + 1].Active = true;
        }
    }

}
