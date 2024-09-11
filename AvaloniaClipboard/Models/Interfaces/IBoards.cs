using System.Collections.Generic;

namespace AvaloniaClipboard.Models.Interfaces;

public interface IBoards<TBoard>
    where TBoard : ISimpleBoard
{
    IList<TBoard> Boards { get; set; }
}