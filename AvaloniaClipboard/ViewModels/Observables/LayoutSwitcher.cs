using System.Collections.ObjectModel;
using AvaloniaClipboard.Services;
using ReactiveUI;

namespace AvaloniaClipboard.ViewModels.Observables;

public class LayoutSwitcher: ObservableBase
{
    public ObservableBoardLayout CurrentLayout => Layouts[CurrentLayoutIndex];
    
    private int _currentLayoutIndex = 0;
    public int CurrentLayoutIndex
    {
        get => _currentLayoutIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentLayoutIndex, value);
            this.RaisePropertyChanged(nameof(CurrentLayout));
        }
    }

    public LayoutSwitcher()
    {
        CurrentLayoutIndex = 0;
    }

    private ObservableCollection<ObservableBoardLayout> _layouts =
    [
        ObservableBoardLayout.Default
    ];

    public ObservableCollection<ObservableBoardLayout> Layouts
    {
        get => _layouts;
        set => this.RaiseAndSetIfChanged(ref _layouts, value);
    } 
    

    public void Add()
    {
        Layouts.Add(ObservableBoardLayout.Default);
        CurrentLayoutIndex = Layouts.Count-1;
    }

    public void Remove()
    {
        if(Layouts.Count <= 1) return;
        Layouts.Remove(CurrentLayout);
    }

    public async void BrowseSave()
    {
        var fileService = ServiceManager.Get<IFileService>();
        CurrentLayout.FilePath = await fileService.BrowseSaveFile();
    }
    public async void BrowseOpen()
    {
        var fileService = ServiceManager.Get<IFileService>();
        CurrentLayout.FilePath = await fileService.BrowseOpenFile();
    }

    public void Read(LayoutSwitcher switcher)
    {
        Layouts = switcher.Layouts;
        CurrentLayoutIndex = switcher.CurrentLayoutIndex;
    }
}