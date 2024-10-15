using System;
using System.Collections.ObjectModel;
using System.IO;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels.LjsonConverters;
using AvaloniaClipboard.ViewModels.Observables;
using ReactiveUI;

namespace AvaloniaClipboard.ViewModels;

public class SettingsViewModel: ViewModelBase
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
    private int _selectedLanguageIndex = 0;
    private int _selectedColorIndex = 0;

    public void Read()
    {
        try
        {
            var content = File.ReadAllText(ReadFile);
            var convertStrategy = new SettingsConverter();
            var l = convertStrategy.LjsonToList(content);
            convertStrategy.AssignValues(this, l);
            
        }
        catch (Exception e)
        {
            // ignored
        }
    }
    
    public void Save()
    {
        try
        {
            var convertStrategy = new SettingsConverter();
            var l = convertStrategy.ExtractValues(this);
            var ljson = convertStrategy.ListToLjson(l);
            File.WriteAllText(ReadFile, ljson);
            
        }
        catch (Exception e)
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