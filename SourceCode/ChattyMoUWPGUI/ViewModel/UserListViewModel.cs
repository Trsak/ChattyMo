using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using ChattyMoUWPGUI.Model.Response;
using ChattyMoUWPGUI.Model.Respository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChattyMoUWPGUI.ViewModel;

public partial class UserListViewModel : ObservableObject
{
    private readonly IUserRepository _userRepository;

    [ObservableProperty]
    private string? _usernameSearch;

    public UserListViewModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        Users = new ObservableCollection<User>();

        LoadUsers();
    }

    public ObservableCollection<User> Users { get; }

    [RelayCommand]
    private async Task FindUsers()
    {
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        try
        {
            var users = await _userRepository.FindByUsername(UsernameSearch);

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                Users.Clear();
                foreach (var user in users) Users.Add(user);
            });
        }
        catch (Exception)
        {
            // ignored
        }
    }
}