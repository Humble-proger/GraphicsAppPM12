using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        [ObservableProperty]
        private string _zoomString = "100%";

        public ICommand ZoomIn { get; }
        public ICommand ZoomOut { get; }

        public FooterViewModel(MainWindowViewModel? main)
        {
            Main = main;
            MouseCoords = "";
            CanvasSize = "";
            ZoomIn = new RelayCommand(() => Main?.Canvasview.Zoom(0.1));
            ZoomOut = new RelayCommand(() => Main?.Canvasview.Zoom(-0.1));
        }
    }
}
