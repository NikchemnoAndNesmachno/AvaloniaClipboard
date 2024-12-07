using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AvaloniaClipboard.Services;
using ReactiveUI;

namespace AvaloniaClipboard.ViewModels.Observables;

public class LayoutSwitcher: ReactiveObject
{
    public OBoardLayout CurrentLayout => Layouts[CurrentLayoutIndex];
    
    private int _currentLayoutIndex;
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

    private ObservableCollection<OBoardLayout> _layouts =
    [
        OBoardLayout.Default
    ];

    public ObservableCollection<OBoardLayout> Layouts
    {
        get => _layouts;
        set => this.RaiseAndSetIfChanged(ref _layouts, value);
    } 
    

    public void Add()
    {
        Layouts.Add(OBoardLayout.Default);
        CurrentLayoutIndex = Layouts.Count-1;
    }

    public void Remove()
    {
        if(Layouts.Count <= 1) return;
        Layouts.Remove(CurrentLayout);
    }

    public async Task BrowseSave()
    {
        var fileService = ServiceManager.Get<IFileService>();
        CurrentLayout.FilePath = await fileService.BrowseSaveFile();
    }
    public async Task BrowseOpen()
    {
        var fileService = ServiceManager.Get<IFileService>();
        CurrentLayout.FilePath = await fileService.BrowseOpenFile();
    }
}