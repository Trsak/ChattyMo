using System;
using System.Windows;
using System.Windows.Input;
using ChattyMoWPFGUI.Command;
using ChattyMoWPFGUI.Context;
using ChattyMoWPFGUI.Model.Repository;
using ChattyMoWPFGUI.View;
using MaterialDesignThemes.Wpf;

namespace ChattyMoWPFGUI.ViewModel;

public class AuthenticationViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private string _password;
    private string _username;

    public AuthenticationViewModel()
    {
        _userRepository = new UserRepository();

        ButtonsEnabled = true;
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        MessageQueue = new SnackbarMessageQueue();
    }

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }
    public ISnackbarMessageQueue MessageQueue { get; }
    public bool ButtonsEnabled { get; set; }

    public string Username
    {
        get => _username;

        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;

        set
        {
            if (value != "")
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password cannot be empty.");

                if (value.Length < 3)
                    throw new ArgumentException("Password must have at least 3 characters.");

                if (value.Length > 60)
                    throw new ArgumentException("Password can not have more than 60 characters.");
            }

            _password = value;
            OnPropertyChanged();
        }
    }

    private bool CanExecuteLoginCommand(object obj)
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private async void ExecuteLoginCommand(object obj)
    {
        SetButtonsState(false);

        try
        {
            var user = await _userRepository.Authenticate(Username, Password);
            UserContext.SetUser(user);

            var chattyMoWindow = new ChattyMoWindow();
            Application.Current.MainWindow.Close();
            chattyMoWindow.Show();
        }
        catch (Exception e)
        {
            MessageQueue.Enqueue(e.Message);
        }

        SetButtonsState(true);
    }

    private bool CanExecuteRegisterCommand(object obj)
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private async void ExecuteRegisterCommand(object obj)
    {
        SetButtonsState(false);

        try
        {
            await _userRepository.Register(Username, Password);

            Username = "";
            Password = "";
            MessageQueue.Enqueue("Your account was created!");
        }
        catch (Exception e)
        {
            MessageQueue.Enqueue(e.Message);
        }

        SetButtonsState(true);
    }

    private void SetButtonsState(bool enabled)
    {
        if (ButtonsEnabled == enabled) return;

        ButtonsEnabled = enabled;
        OnPropertyChanged(nameof(ButtonsEnabled));
    }
}