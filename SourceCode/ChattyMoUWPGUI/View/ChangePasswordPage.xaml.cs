using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ChattyMoUWPGUI.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;

// Dokumentaci k šabloně Prázdná aplikace najdete na adrese https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChattyMoUWPGUI.View;

/// <summary>
///     Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
/// </summary>
public sealed partial class ChangePasswordPage : Page
{
    public ChangePasswordPage()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<ChangePasswordViewModel>();
    }

    public ChangePasswordViewModel ViewModel => (ChangePasswordViewModel) DataContext;

    private void OldPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null) ((dynamic) DataContext).OldPassword = ((PasswordBox) sender).Password;
    }

    private void NewPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null) ((dynamic) DataContext).NewPassword = ((PasswordBox) sender).Password;
    }

    private void NewPasswordCheck_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null) ((dynamic) DataContext).NewPasswordCheck = ((PasswordBox) sender).Password;
    }
}