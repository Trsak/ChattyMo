using Windows.UI.Xaml.Controls;
using ChattyMoUWPGUI.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ChattyMoUWPGUI.View;

public sealed partial class UserListPage : Page
{
    public UserListPage()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<UserListViewModel>();
    }

    public UserListViewModel ViewModel => (UserListViewModel) DataContext;
}