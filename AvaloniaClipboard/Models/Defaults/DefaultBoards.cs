using System.Collections.Generic;
using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.Models.Defaults;

public class DefaultBoards<TBoard> : IBoards<TBoard> where TBoard : ISimpleBoard
{
    public IList<TBoard> Boards { get; set; } = [];
}