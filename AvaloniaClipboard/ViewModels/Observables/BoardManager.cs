using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaClipboard.Services;
using SharpHook.Native;

namespace AvaloniaClipboard.ViewModels.Observables;

public class BoardManager
{
    private IClipboard? _clipboard;
    public IClipboard Clipboard => _clipboard ??= ServiceManager.Get<IClipboard>();
    public IList<OBoard> Boards { get; set; } = new ObservableCollection<OBoard>();
    
    public OBoard this[int key]
    {
        get => Boards[key];
        set => Boards[key] = value;
    }

    public void Remove(OBoard board) => Boards.Remove(board);
    private int IndexOf(string name)
    {
        for (var i = 0; i < Boards.Count; i++)
            if(Boards[i].Name == name) return i;
        return -1;
    }
    
    public async Task SetTextToClipboard(OBoard board) => 
        await Clipboard.SetTextAsync(board.Data);

    public async Task SetTextFromClipBoard(OBoard board) => 
        board.Data = await Clipboard.GetTextAsync();


    public void AddHotkeyBoard(OBoard board)
    {
        var index = IndexOf(board.Name);
        if (index == -1)
        {
            board.ToClipboardHotkey.OnHotkey = () => _ = SetTextToClipboard(board);
            board.FromClipboardHotkey.OnHotkey = () => _ = SetTextFromClipBoard(board);
            Boards.Add(board);
        }
        else
        {
            this[index].ToClipboardHotkey.KeyCodes =
                new ObservableCollection<KeyCode>(board.ToClipboardHotkey.KeyCodes);
            this[index].FromClipboardHotkey.KeyCodes = 
                new ObservableCollection<KeyCode>(board.FromClipboardHotkey.KeyCodes);
        }
    }
    public void AddHotkeyBoardCopy(OBoard board)
    {
        var index = IndexOf(board.Name);
        if (index == -1)
        {
            var newBoard = new OBoard
            {
                Name = board.Name,
                ToClipboardHotkey =
                {
                    KeyCodes = new ObservableCollection<KeyCode>(board.ToClipboardHotkey.KeyCodes),
                },
                FromClipboardHotkey =
                {
                    KeyCodes = new ObservableCollection<KeyCode>(board.FromClipboardHotkey.KeyCodes)
                }
            };
            newBoard.ToClipboardHotkey.OnHotkey = () => _ = SetTextToClipboard(newBoard);
            newBoard.FromClipboardHotkey.OnHotkey = () => _ = SetTextFromClipBoard(newBoard);
            Boards.Add(newBoard);
        }
        else
        {
            this[index].ToClipboardHotkey.KeyCodes = new ObservableCollection<KeyCode>(board.ToClipboardHotkey.KeyCodes);
            this[index].FromClipboardHotkey.KeyCodes = new ObservableCollection<KeyCode>(board.FromClipboardHotkey.KeyCodes);
        }
    }

    public void Clear()
    { 
        Boards.Clear();
    }
}