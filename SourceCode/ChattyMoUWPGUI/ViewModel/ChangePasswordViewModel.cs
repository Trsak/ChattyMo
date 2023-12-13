using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using ChattyMoUWPGUI.Model.Respository;
using ChattyMoUWPGUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChattyMoUWPGUI.ViewModel;

public partial class ChangePasswordViewModel : ObservableValidator
{
    private readonly IUserRepository _userRepository;
    private readonly IDialogService DialogService;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(60, ErrorMessage = "New Password can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "New Password must be at least 3 characters long.")]
    private string? _newPassword;

    private string? _newPasswordCheck;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(60, ErrorMessage = "Current Password can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "Current Password must be at least 3 characters long.")]
    private string? _oldPassword;

    [ObservableProperty]
    private Visibility errorsDialogButtonVisibility;

    public ChangePasswordViewModel(IDialogService dialogService, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        DialogService = dialogService;
    }

    [Required]
    [MaxLength(60, ErrorMessage = "New Password Check can not be longer than 60 characters.")]
    [MinLength(3, ErrorMessage = "New Password Check must be at least 3 characters long.")]
    [CustomValidation(typeof(ChangePasswordViewModel), "ValidatePasswordCheck")]
    public string? NewPasswordCheck
    {
        get => _newPasswordCheck;
        set => SetProperty(ref _newPasswordCheck, value, true);
    }

    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;

    [RelayCommand]
    private async Task ChangePassword()
    {
        if (IsFormValid())
            try
            {
                await _userRepository.ChangePassword(OldPassword, NewPassword);
                FormSubmissionCompleted?.Invoke(this, EventArgs.Empty);
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

    public static ValidationResult ValidatePasswordCheck(string? value, ValidationContext context)
    {
        var instance = (ChangePasswordViewModel) context.ObjectInstance;

        if (instance.NewPassword != null && instance.NewPassword.Equals(value)) return ValidationResult.Success;

        return new ValidationResult("Both New Password and New Password check must be the same.");
    }
}