using static GraphicsApp.Views.Layers;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VecEditor.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using GraphicsApp.Views;
using Avalonia;
using System.IO;

namespace GraphicsApp.ViewModels;

public partial class SettingsWindowViewModel : ViewModelBase, IDisposable
{
    [ObservableProperty]
    private MainWindowViewModel _main;

    [ObservableProperty]
    private string _saveLocation;

    private CancellationTokenSource _autoSaveCts;
    private bool _isAutoSaveEnabled;

    [ObservableProperty]
    private int _autoSaveInterval = 1;


    public bool IsAutoSaveEnabled
    {
        get => _isAutoSaveEnabled;
        set
        {
            
            SetProperty(ref _isAutoSaveEnabled, value);
            UpdateAutoSave();
            
        }
    }

    private void UpdateAutoSave()
    {
        _autoSaveCts?.Cancel();

        if (IsAutoSaveEnabled)
        {
            _autoSaveCts = new CancellationTokenSource();
            StartAutoSave(_autoSaveCts.Token);
        }
    }

    private async void StartAutoSave(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMinutes(AutoSaveInterval), ct);
            await SaveAllDataAsync();
        }
    }

    private async Task SaveAllDataAsync()
    {
        var result = SaveLocation;
        var TypeFile = Path.GetExtension(result);
        var NameFile = Path.GetFileName(result);
        if (TypeFile != null && NameFile != null)
        {
            var Type = TypeFile.ToLower();
            switch (Type)
            {
                case ".json":
                    Main.SaveJsonCommand.Execute(result);
                    break;
                case ".svg":
                    Main.SaveSvgCommand.Execute(result);
                    break;
                case ".png":
                    Main.SavePngCommand.Execute(result);
                    break;
                default:
                    break;
            }
        }
        Debug.WriteLine($"Автосохранение выполнено в {DateTime.Now}");
    }

    public SettingsWindowViewModel(MainWindowViewModel main) {
        Main = main;
    }

    public void Dispose() => _autoSaveCts?.Cancel();
}