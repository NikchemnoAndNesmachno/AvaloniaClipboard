using System.Collections.Generic;
using AvaloniaClipboard.ViewModels.Observables;
using Ljson.ConvertStringsStrategy;
using Ljson.DefaultBases;

namespace AvaloniaClipboard.ViewModels.LjsonConverters;

public class LjsonLayoutConverter: LjsonDefaultConvert<OBoardLayout>
{
    public LjsonLayoutConverter()
    {
        ConvertStrategy = new SimpleStrategy()
        {
            Char = 'ȴ'
        };
    }
    public override IList<object> ExtractValues(OBoardLayout obj) =>
    [
        obj.Name,
        obj.FilePath
    ];

    public override void AssignValues(OBoardLayout obj, IList<string> values)
    {
        obj.Name = values[0];
        obj.FilePath = values[1];
    }
}