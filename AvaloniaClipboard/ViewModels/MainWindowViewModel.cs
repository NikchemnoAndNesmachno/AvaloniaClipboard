using ReactiveUI;

namespace AvaloniaClipboard.ViewModels;

public class MainWindowViewModel(HotkeyViewModel hotkeyViewModel, SettingsViewModel settingsViewModel)
    : ReactiveObject
{
    public HotkeyViewModel HotkeyViewModel { get; set; } = hotkeyViewModel;
    public SettingsViewModel SettingsViewModel { get; set; } = settingsViewModel;
}