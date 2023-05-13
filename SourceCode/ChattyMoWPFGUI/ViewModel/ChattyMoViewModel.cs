using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ChattyMoWPFGUI.Context;
using ChattyMoWPFGUI.Model.Repository;
using ChattyMoWPFGUI.View;

namespace ChattyMoWPFGUI.ViewModel;

public class ChattyMoViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private UserControl? _currentContentControl;
    private MenuItemViewModel? _selectedMenuItem;

    public ChattyMoViewModel()
    {
        _userRepository = new UserRepository();

        MenuItems = CreateMenu();
        SelectedMenuItem = MenuItems.First();
    }

    public string Username => UserContext.User.Username;

    public MenuItemViewModel? SelectedMenuItem
    {
        get => _selectedMenuItem;
        set
        {
            _selectedMenuItem = value;
            OnPropertyChanged();
            SelectedMenuItemAction();
        }
    }

    public UserControl? ContentUserControl
    {
        get => _currentContentControl;
        set
        {
            _currentContentControl = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<MenuItemViewModel> MenuItems { get; }

    private void SelectedMenuItemAction()
    {
        if (_selectedMenuItem == null) return;

        ContentUserControl = _selectedMenuItem.UserControl;
        if (ContentUserControl == null) Logout();
    }

    private void Logout()
    {
        _userRepository.Logout();

        var authWindow = new AuthWindow();

        foreach (Window window in Application.Current.Windows)
            if (window.GetType() != typeof(AuthWindow))
                window.Close();

        authWindow.Show();
    }

    private ObservableCollection<MenuItemViewModel> CreateMenu()
    {
        return new ObservableCollection<MenuItemViewModel>
        {
            new()
            {
                Title = "Chat",
                Icon = "Chat",
                UserControl = new ChatControl()
            },
            new()
            {
                Title = "Users",
                Icon = "Users",
                UserControl = new UserListControl()
            },
            new()
            {
                Title = "Change password",
                Icon = "Password",
                UserControl = new ChangePasswordControl()
            },
            new()
            {
                Title = "Logout",
                Icon = "Logout",
                UserControl = null
            }
        };
    }
}