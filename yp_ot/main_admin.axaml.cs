using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace yp_ot;

public partial class main_admin : Window
{
    public main_admin()
    {
        InitializeComponent();
    }
    private void OpenNewWindow(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new Services();
        newWindow.Show();
        this.Close();
    }
    private void Back(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainWindow back = new MainWindow();
        back.Show();
        this.Close();
    }

    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        this.Close();
    }
}