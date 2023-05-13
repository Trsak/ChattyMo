using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChattyMoWPFGUI.Command;
using ChattyMoWPFGUI.Model.Repository;
using ChattyMoWPFGUI.Model.Response;

namespace ChattyMoWPFGUI.ViewModel;

public class UserListViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private string _usernameSearch;

    public UserListViewModel()
    {
        _userRepository = new UserRepository();
        Users = new ObservableCollection<User>();
        FindUsersCommand = new ViewModelCommand(ExecuteFindUsersCommand, CanExecuteFindUsersCommand);

        LoadUsers();
    }

    public ICommand FindUsersCommand { get; }

    public bool SearchButtonEnabled { get; set; }
    public ObservableCollection<User> Users { get; }

    public string UsernameSearch
    {
        get => _usernameSearch;

        set
        {
            _usernameSearch = value;
            OnPropertyChanged();
        }
    }

    private void SetButtonEnabled(bool enabled)
    {
        if (SearchButtonEnabled == enabled) return;

        SearchButtonEnabled = enabled;
        OnPropertyChanged(nameof(SearchButtonEnabled));
    }

    private async Task LoadUsers()
    {
        SetButtonEnabled(false);

        try
        {
            var users = await _userRepository.FindByUsername(UsernameSearch);

            Application.Current.Dispatcher.Invoke(() =>
            {
                Users.Clear();
                foreach (var user in users) Users.Add(user);
            });
        }
        catch (Exception)
        {
            // ignored
        }

        SetButtonEnabled(true);
    }


    private bool CanExecuteFindUsersCommand(object obj)
    {
        return true;
    }

    private async void ExecuteFindUsersCommand(object obj)
    {
        await LoadUsers();
    }
}