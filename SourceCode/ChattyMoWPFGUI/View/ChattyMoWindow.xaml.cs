using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ChattyMoWPFGUI.View;

public partial class ChattyMoWindow : Window
{
    private bool _isMenuOpen;

    public ChattyMoWindow()
    {
        InitializeComponent();

        EventManager.RegisterClassHandler(typeof(Window), PreviewMouseDownEvent,
            new MouseButtonEventHandler(OnPreviewMouseDown));
    }

    private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
    {
        ButtonCloseMenu.Visibility = Visibility.Visible;
        ButtonOpenMenu.Visibility = Visibility.Collapsed;

        _isMenuOpen = true;
    }

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!_isMenuOpen) return;
        CloseMenu();
        _isMenuOpen = false;
    }

    private void CloseMenu()
    {
        ButtonCloseMenu.Visibility = Visibility.Collapsed;
        ButtonOpenMenu.Visibility = Visibility.Visible;

        ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
    }
}