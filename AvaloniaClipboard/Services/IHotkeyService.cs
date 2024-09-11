using SharpHotHook;

namespace AvaloniaClipboard.Services;

public interface IHotkeyService
{
    public HotkeyManager GetGlobalHotkeyManager();
    public HotkeyManager GetLocalHotkeyManager();
    public void Switch();
}