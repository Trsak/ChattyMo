#nullable enable

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ChattyMoUWPGUI.Model.Respository;
using ChattyMoUWPGUI.Services;
using ChattyMoUWPGUI.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChattyMoUWPGUI.ViewModel;

public partial class AuthFormViewModel : ObservableValidator
{
    private readonly IUserRepository _userRepository;
    private readonly IDialogService DialogService;

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    private Visibility errorsDialogButtonVisibility;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(60)]
    [MinLength(3)]
    private string? password;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    private string? username;

    public AuthFormViewModel(IDialogService dialogService, IUserRepository userRepository)
    {
        ErrorsDialogButtonVisibility = Visibility.Visible;
        _userRepository = userRepository;
        DialogService = dialogService;
    }

    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;

    [RelayCommand]
    private async Task Register(object obj)
    {
        if (IsFormValid())
            try
            {
                await _userRepository.Register(Username, Password);

                Username = "";

                var passwordBox = obj as PasswordBox;
                passwordBox.Password = "";

                FormSubmissionCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                DisplayErrorMessageBox(ex.Message);
            }
    }

    [RelayCommand]
    private async Task Login()
    {
        if (IsFormValid())
            try
            {
                await _userRepository.Authenticate(Username, Password);

                Window.Current.Content = new ChattyMoPage();
            }
            catch (Exception ex)
            {
                DisplayErrorMessageBox(ex.Message);
            }
    }

    private bool IsFormValid()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            DisplayErrorMessageBox("The form was filled in with some errors.", true);
            return false;
        }


        return true;
    }

    private void DisplayErrorMessageBox(string message, bool displayDialogButton = false)
    {
        if (displayDialogButton)
            ErrorsDialogButtonVisibility = Visibility.Visible;
        else
            ErrorsDialogButtonVisibility = Visibility.Collapsed;

        ErrorMessage = message;
        FormSubmissionFailed?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private void ShowErrors()
    {
        var message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));

        _ = DialogService.ShowMessageDialogAsync("Validation errors", message);
    }
}