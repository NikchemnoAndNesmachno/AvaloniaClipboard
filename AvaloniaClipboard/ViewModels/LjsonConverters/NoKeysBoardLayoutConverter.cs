using System.Collections.Generic;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class NoKeysBoardLayoutConverter: LjsonDefaultConvert<ObservableBoardLayout>
{
    public NoKeysBoardLayoutConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = '袈'
        };
    } 
    public override IList<object> ExtractValues(ObservableBoardLayout obj) =>
    [
        obj.Name,
        obj.FilePath
    ];

    public override void AssignValues(ObservableBoardLayout obj, IList<string> values)
    {
        obj.Name = values[0];
        obj.FilePath = values[1];
    }
}