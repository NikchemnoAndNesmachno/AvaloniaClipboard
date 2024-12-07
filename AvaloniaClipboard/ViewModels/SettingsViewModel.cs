using System;
using System.IO;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels.LjsonConverters;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;

namespace AvaloniaClipboard.ViewModels;

public class SettingsViewModel: ReactiveObject
{

    public SettingsViewModel(LayoutSwitcher layoutSwitcher)
    {
        LayoutSwitcher = layoutSwitcher;
        this.WhenAnyValue(x => x.SelectedLanguageIndex).Subscribe(OnLanguageChanged);
        this.WhenAnyValue(x => x.SelectedColorIndex).Subscribe(OnColorChanged);
        Read();
    }

    private void OnLanguageChanged(int obj)
    {
        var service = ServiceManager.Get<ILanguageChanger>();
        service.ChangeLanguage(SelectedLanguageIndex);
    }
    
    private void OnColorChanged(int obj)
    {
        var service = ServiceManager.Get<IColorChanger>();
        service.ChangeColorScheme(SelectedColorIndex);
    }
    
    public LayoutSwitcher LayoutSwitcher { get; set; }
    private int _selectedLanguageIndex;
    private int _selectedColorIndex;

    public void Read()
    {
        try
        {
            var content = File.ReadAllText(ReadFile);
            var converter = new LjsonSettingsConverter();
            converter.FromLjson(this, content);
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    public void Save()
    {
        try
        {
            var converter = new LjsonSettingsConverter();
            var ljson = converter.ToLjson(this);
            File.WriteAllText(ReadFile, ljson);
            
        }
        catch (Exception)
        {
            // ignored
        }
    }
    private string ReadFile => "settings_config.txt";
    public int SelectedLanguageIndex
    {
        get => _selectedLanguageIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedLanguageIndex, value);
    }
    public int SelectedColorIndex
    {
        get => _selectedColorIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedColorIndex, value);
    }
}