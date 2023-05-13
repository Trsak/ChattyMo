using System;
using System.Windows.Input;
using ChattyMoWPFGUI.Command;
using ChattyMoWPFGUI.Model.Repository;
using MaterialDesignThemes.Wpf;

namespace ChattyMoWPFGUI.ViewModel;

public class ChangePasswordViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private string _currentPassword;
    private string _newPassword;
    private string _newPasswordCheck;

    public ChangePasswordViewModel()
    {
        _userRepository = new UserRepository();

        MessageQueue = new SnackbarMessageQueue();
        ChangePasswordCommand = new ViewModelCommand(ExecuteChangePasswordCommand, CanExecuteChangePasswordCommand);
        SetButtonEnabled(true);
    }

    public ICommand ChangePasswordCommand { get; }
    public bool ButtonEnabled { get; set; }
    public ISnackbarMessageQueue MessageQueue { get; }

    public string CurrentPassword
    {
        get => _currentPassword;
        set
        {
            ValidatePassword(value);
            _currentPassword = value;
            OnPropertyChanged();
        }
    }

    public string NewPassword
    {
        get => _newPassword;
        set
        {
            ValidatePassword(value);
            _newPassword = value;
            OnPropertyChanged();
        }
    }

    public string NewPasswordCheck
    {
        get => _newPasswordCheck;
        set
        {
            ValidatePassword(value, true);
            _newPasswordCheck = value;
            OnPropertyChanged();
        }
    }

    private bool CanExecuteChangePasswordCommand(object obj)
    {
        return !string.IsNullOrWhiteSpace(CurrentPassword) && !string.IsNullOrWhiteSpace(NewPassword) &&
               !string.IsNullOrWhiteSpace(NewPasswordCheck);
    }

    private async void ExecuteChangePasswordCommand(object obj)
    {
        SetButtonEnabled(false);

        try
        {
            await _userRepository.ChangePassword(CurrentPassword, NewPassword);

            CurrentPassword = "";
            NewPassword = "";
            NewPasswordCheck = "";
            MessageQueue.Enqueue("Password was changed successfully");
        }
        catch (Exception e)
        {
            MessageQueue.Enqueue(e.Message);
        }

        SetButtonEnabled(true);
    }

    private void SetButtonEnabled(bool enabled)
    {
        if (ButtonEnabled == enabled) return;

        ButtonEnabled = enabled;
        OnPropertyChanged(nameof(ButtonEnabled));
    }

    private void ValidatePassword(string value, bool checkIfSame = false)
    {
        if (value == "") return;

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty.");

        if (value.Length < 3)
            throw new ArgumentException("Password must have at least 3 characters.");

        if (value.Length > 60)
            throw new ArgumentException("Password can not have more than 60 characters.");

        if (!checkIfSame) return;

        if (!value.Equals(NewPassword))
            throw new ArgumentException("New passwords must be the same.");
    }
}