using System.ComponentModel;
using ChattyMoWinFormsGUI.Presenter;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ChattyMoWinFormsGUI.View;

public partial class AuthForm : MaterialForm, IAuthView
{
    private readonly ChattyMo _chattyMo;

    public AuthForm(ChattyMo chattyMo)
    {
        _chattyMo = chattyMo;
        InitializeComponent();

        var materialSkinManager = MaterialSkinManager.Instance;
        materialSkinManager.AddFormToManage(this);
        materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo400, Primary.Indigo700, Primary.Indigo100,
            Accent.Blue200, TextShade.WHITE);

        Closing += CloseWholeApp;
    }

    public UserPresenter Presenter { private get; set; }


    public string Username
    {
        get => usernameInput.Text;
        set => usernameInput.Text = value;
    }

    public string Password
    {
        get => passwordInput.Text;
        set => passwordInput.Text = value;
    }

    private static void CloseWholeApp(object sender, CancelEventArgs e)
    {
        Environment.Exit(Environment.ExitCode);
    }

    private async void loginButton_Click(object sender, EventArgs e)
    {
        var isLoggedIn = false;

        try
        {
            if (ValidateUsernameAndPassword())
            {
                SetButtonsEnabled(false);
                var user = await Presenter.Login();
                isLoggedIn = true;
            }
        }
        catch (Exception ex)
        {
            var materialDialog = new MaterialDialog(this, "Login failed", ex.Message, "OK");
            materialDialog.ShowDialog(this);
            SetButtonsEnabled();
            return;
        }

        if (!isLoggedIn) return;
        Hide();
        await _chattyMo.Display(this);
    }

    private async void registerButton_Click(object sender, EventArgs e)
    {
        SetButtonsEnabled(false);

        try
        {
            if (ValidateUsernameAndPassword())
            {
                var registerAttempt = await Presenter.Register();

                if (registerAttempt)
                {
                    ClearInputs();
                    var materialDialog = new MaterialDialog(this, "Registration successful",
                        "Your account was created, you can now log in!", "OK");
                    materialDialog.ShowDialog(this);
                }
            }
        }
        catch (Exception ex)
        {
            var materialDialog = new MaterialDialog(this, "Registration failed", ex.Message, "OK");
            materialDialog.ShowDialog(this);
        }

        SetButtonsEnabled();
    }

    private void usernameInput_KeyUp(object sender, EventArgs e)
    {
        ValidateUsername();
    }

    private void passwordInput_KeyUp(object sender, EventArgs e)
    {
        ValidatePassword();
    }

    public void SetButtonsEnabled(bool enabled = true)
    {
        loginButton.Enabled = enabled;
        registerButton.Enabled = enabled;
    }

    public void ClearInputs()
    {
        usernameInput.Text = "";
        passwordInput.Text = "";
    }

    private bool ValidateUsernameAndPassword()
    {
        var errorText = "";

        if (!ValidateUsername()) errorText += "Username must be filled and be in size of 3 to 30 chars! ";

        if (!ValidatePassword()) errorText += "Password must be filled and be in size of 3 to 60 chars! ";

        var isValid = errorText.Equals("");
        if (!isValid)
        {
            var materialDialog = new MaterialDialog(this, "Invalid input", errorText, "OK");
            materialDialog.ShowDialog(this);
        }

        return isValid;
    }

    private bool ValidateUsername()
    {
        usernameInput.SetErrorState(false);

        if (string.IsNullOrWhiteSpace(usernameInput.Text) || usernameInput.Text.Length is < 3 or > 30)
        {
            usernameInput.SetErrorState(true);
            return false;
        }

        return true;
    }

    private bool ValidatePassword()
    {
        passwordInput.SetErrorState(false);

        if (string.IsNullOrWhiteSpace(passwordInput.Text) || passwordInput.Text.Length is < 3 or > 60)
        {
            passwordInput.SetErrorState(true);
            return false;
        }

        return true;
    }

    public void Display()
    {
        Show();

        var snackBarMessage = new MaterialSnackBar("You have been logged out");
        snackBarMessage.Show(this);
    }
}