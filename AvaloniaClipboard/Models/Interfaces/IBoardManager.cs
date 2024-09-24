using System.Collections.Generic;

namespace AvaloniaClipboard.Models.Interfaces;

public interface IBoardManager
{
    public IList<IBoard> Boards { get; set; }
    int IndexOf(string name);
    void Add(IBoard board);
    void Remove(IBoard board);

    IBoard Create(string name);
    IBoard Get(int index);
}