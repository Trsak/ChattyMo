using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ChattyMoUWPGUI.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ChattyMoUWPGUI.View;

public sealed partial class AuthPage : Page
{
    public AuthPage()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<AuthFormViewModel>();
    }

    public AuthFormViewModel ViewModel => (AuthFormViewModel) DataContext;

    private void AuthPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ApplicationView.GetForCurrentView().TryResizeView(new Size(650, 350));
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null) ((dynamic) DataContext).Password = ((PasswordBox) sender).Password;
    }
}