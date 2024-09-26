using System.Collections.Generic;
using System.Linq;
using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.ViewModels;

public class HandManagerViewModel(IBoardManager boardManager): ViewModelBase
{
    public IBoardManager BoardManager { get; set; } = boardManager;

    public IList<IBoard> Boards => BoardManager.Boards;

    public void AddBoard(string name)
    {
        if (Boards.FirstOrDefault(x => x.Name == name) is not null) return;
        BoardManager.Create(name);
    }

    public void RemoveBoard(string name)
    {
        var board = Boards.FirstOrDefault(x => x.Name == name);
        if(board is null) return;
        BoardManager.Remove(board);
    }
}