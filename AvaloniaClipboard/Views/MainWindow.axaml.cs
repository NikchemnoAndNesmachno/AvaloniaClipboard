using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaClipboard.Services;
using AvaloniaClipboard.ViewModels;

namespace AvaloniaClipboard.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ServiceManager.Get<MainWindowViewModel>();
        Closing += Window_OnClosing;
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        Keys.Text += e.Key.ToString();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        ((Window)sender).Hide();
        e.Cancel = true;
    }
}