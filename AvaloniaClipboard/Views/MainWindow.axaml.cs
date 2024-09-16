using Avalonia.Controls;
//using Avalonia.Interactivity;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;
using AvaloniaClipboard.Views.ServiceImplements;

namespace AvaloniaClipboard.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(new ClipboardService(this));
        Closing += Window_OnClosing;
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        ((Window)sender).Hide();
        e.Cancel = true;
    }
    

    /*private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
    }*/
}