﻿using System.Text.Json.Serialization;
using System.Windows.Input;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Geometry;

namespace GraphicsApp.ViewModels
{
    public partial class ShapeViewModel : ViewModelBase
    {
        [ObservableProperty]
        [property: JsonIgnore]
        private MainWindowViewModel? _main;

        [ObservableProperty]
        private string _name = "Name";
        
        [ObservableProperty]
        private Avalonia.Media.Color _selectedColor;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Thickness))]
        [property: JsonIgnore]
        private bool _isSelected;
        

        [JsonIgnore]
        public float Thickness => IsSelected ? 2 : 1;

        public required IShape Model { get; init; }
        
        

        [JsonIgnore]
        public ICommand Remove { get; }

        public ShapeViewModel()
        {
            Remove = new RelayCommand(() => Main?.Figures.Remove(this));
        }
    }
}