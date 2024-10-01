using Avalonia.Controls;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views.ServiceImplements;

namespace AvaloniaClipboard.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var clipboard = new ClipboardService(this);
        DataContext = new MainWindowViewModel(clipboard);
        Closing += Window_OnClosing;
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        ((Window)sender).Hide();
        e.Cancel = true;
    }
    
}