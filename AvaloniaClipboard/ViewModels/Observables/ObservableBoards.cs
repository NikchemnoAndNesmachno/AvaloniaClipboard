using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaClipboard.Models;
using AvaloniaClipboard.Models.Interfaces;

namespace AvaloniaClipboard.ViewModels.Observables;

public class ObservableBoards<TBoard>: IBoards<TBoard> where TBoard : ISimpleBoard
{
    public IList<TBoard> Boards { get; set; } = new ObservableCollection<TBoard>();
}
