using System.Collections;
using System.Collections.Generic;
using AvaloniaClipboard.Models.Defaults;
using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.Models;

public class BoardManager<TBoard> : IBoardManager
    where TBoard : IBoard, new()
{
    public IList<IBoard> Boards { get; set; } = [];

    public int IndexOf(string name)
    {
        for (var i = 0; i < Boards.Count; i++)
        {
            var item = Boards[i];
            if (item.Name == name) return i;
        }

        return -1;
    }

    public void Add(IBoard board) => Boards.Add(board);

    public void Remove(IBoard board) => Boards.Remove(board);

    public IBoard Create(string name)
    {
        var board = new TBoard
        {
            Name = name,
            Data = ""
        };
        Boards.Add(board);
        return board;
    }

    public IBoard Get(int index) => Boards[index];

    public void SetData(string name, string data)
    {
        foreach (var board in Boards)
        {
            if (board.Name != name) continue;
            board.Data = data;
            return;
        }
    }

    public string GetData(string name)
    {
        foreach (var board in Boards)
        {
            if (board.Name != name) continue;
            return board.Data;
        }

        return "";
    }
}