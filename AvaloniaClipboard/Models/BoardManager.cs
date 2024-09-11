using System.Linq;
using AvaloniaClipboard.Models.Defaults;
using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.Models;

public class BoardManager<TBoard> : IBoardManager 
    where TBoard: ISimpleBoard, new()
{
    public IBoards<ISimpleBoard> Boards { get; set; } = new DefaultBoards<ISimpleBoard>();

    public int IndexOf(string name)
    {
        for(int i =0; i < Boards.Boards.Count; i++)
        {
            var item = Boards.Boards[i];
            if (item.Name == name)
            {
                return i;
            }
        }

        return -1;
    }

    public void Add(ISimpleBoard board) => Boards.Boards.Add(board);

    public void Remove(ISimpleBoard board) => Boards.Boards.Add(board);

    public ISimpleBoard Create(string name)
    {
        var board = new TBoard()
        {
            Name = name,
            Data = ""
        };
        Boards.Boards.Add(board);
        return board;
    }

    public ISimpleBoard Get(int index) => Boards.Boards[index];

    public void SetData(string name, string data)
    {
        foreach (var board in Boards.Boards)
        {
            if (board.Name != name) continue;
            board.Data = data;
            return;
        }
    }

    public string GetData(string name)
    {
        foreach (var board in Boards.Boards)
        {
            if (board.Name != name) continue;
            return board.Data;
        }
        return "";
    }
    
}