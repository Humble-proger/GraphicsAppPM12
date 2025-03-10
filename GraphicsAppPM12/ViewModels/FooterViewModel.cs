using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GraphicsApp.ViewModels
{
    public partial class FooterViewModel : ViewModelBase
    {
        [ObservableProperty]
        private MainWindowViewModel? _main;

        [ObservableProperty]
        private string _mouseCoords;

        [ObservableProperty]
        private string _canvasSize;

        public FooterViewModel(MainWindowViewModel? main)
        {
            Main = main;
            MouseCoords = "";
            CanvasSize = "";
        }
    }
}
