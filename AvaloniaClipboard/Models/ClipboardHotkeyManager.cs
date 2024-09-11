using AvaloniaClipboard.Models.Defaults;
using AvaloniaClipboard.Models.Interfaces;
using SharpHotHook;

namespace AvaloniaClipboard.Models;

public class ClipboardHotkeyManager
{
    public IClipboard? Clipboard { get; set; }
    public HotkeyManager HotkeyManager { get; set; } = new();

    public IBoardManager BoardManager { get; set; } = new BoardManager<DefaultBoard>()
    {

    };
    
    public async void SetTextToClipboard(string boardName)
    {
        if(Clipboard is null) return;
        var index = BoardManager.IndexOf(boardName);
        if(index == -1) return;
        await Clipboard.SetTextAsync(BoardManager.Get(index).Data);
    }

    public async void SetTextFromClipBoard(string boardName)
    {
        if(Clipboard is null) return;
        var index = BoardManager.IndexOf(boardName);
        if(index == -1) return;
        var board = BoardManager.Get(index);
        board.Data = await Clipboard.GetTextAsync();
    }

    public void RemoveHotkey(IHotkey hotkey, string boardName)
    {
        HotkeyManager.HotkeyContainer.Remove(hotkey.KeyCodes);
        var boardIndex = BoardManager.IndexOf(boardName);
        BoardManager.Remove(BoardManager.Get(boardIndex));
    }
    
    public void AddHotkey_SetToBoard(IHotkey hotkey, string boardName)
    {
        var boardIndex = BoardManager.IndexOf(boardName);
        if (boardIndex == -1)
        {
            BoardManager.Create(boardName);
        }
        HotkeyManager.AddHotkey(hotkey.KeyCodes, () =>
        {
            SetTextFromClipBoard(boardName);
        });
    }
    
    
}