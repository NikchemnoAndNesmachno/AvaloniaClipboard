namespace AvaloniaClipboard.Models.Interfaces;

public interface IBoardManager
{
    int IndexOf(string name);
    void Add(ISimpleBoard board);
    void Remove(ISimpleBoard board);

    ISimpleBoard Create(string name);
    ISimpleBoard Get(int index);
}