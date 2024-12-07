using System.Collections.Generic;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class LjsonBoardManagerConverter: LjsonDefaultConvert<BoardManager>
{
    public LjsonBoardConverter BoardConverter { get; set; } = new(); 
    public LjsonBoardManagerConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = 'Ȣ'
        };
    }
    public override IList<object> ExtractValues(BoardManager obj)
    {
        var array = new object[obj.Boards.Count];
        for (var index = 0; index < obj.Boards.Count; index++)
        {
            var board = obj.Boards[index];
            array[index] = BoardConverter.ToLjson(board);
        }

        return array;
    }

    public override void AssignValues(BoardManager obj, IList<string> values)
    {
        obj.Clear();
        foreach (var value in values)
        {
            var board = new OBoard();
            BoardConverter.FromLjson(board, value);
            obj.AddHotkeyBoard(board);
        }
    }
}