using System.ComponentModel;
using ChattyMoWinFormsGUI.Presenter;
using MaterialSkin;
using MaterialSkin.Controls;
using Timer = System.Windows.Forms.Timer;

namespace ChattyMoWinFormsGUI.View;

public partial class ChattyMo : MaterialForm, IChattyMoView
{
    private AuthForm _authForm;
    private long _latestMessageId;
    private Timer _messageAutoLoadTimer;

    public ChattyMo()
    {
        InitializeComponent();

        var materialSkinManager = MaterialSkinManager.Instance;
        materialSkinManager.AddFormToManage(this);
        materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo900, Primary.Amber50, Primary.BlueGrey500,
            Accent.Blue100, TextShade.WHITE);

        Closing += CloseWholeApp;
    }

    public UserPresenter UserPresenter { private get; set; }

    public ChatMessagePresenter ChatMessagePresenter { private get; set; }

    public string UserSearch
    {
        get => usernameSearch.Text;
        set => usernameSearch.Text = value;
    }

    public string OldPassword
    {
        get => oldPassword.Text;
        set => oldPassword.Text = value;
    }

    public string NewPassword
    {
        get => newPassword.Text;
        set => newPassword.Text = value;
    }

    public string ChatMessage
    {
        get => sendText.Text;
        set => sendText.Text = value;
    }

    public async Task Display(AuthForm authForm)
    {
        _authForm = authForm;
        materialTabControl1.SelectedIndex = 0;

        Text = $"ChattyMo [{UserPresenter.GetCurrentUser()?.Username}]";

        Update();
        Show();
        await LoadNewMessages();
        await LoadUsersList();

        var snackBarMessage =
            new MaterialSnackBar($"You have been logged in as {UserPresenter.GetCurrentUser()?.Username}");
        snackBarMessage.Show(this);

        _messageAutoLoadTimer = new Timer();
        _messageAutoLoadTimer.Tick += LoadMessages_Tick;
        _messageAutoLoadTimer.Interval = 5000;
        _messageAutoLoadTimer.Start();
    }

    private async void LoadMessages_Tick(object sender, EventArgs e)
    {
        await LoadNewMessages();
    }

    private async Task LoadNewMessages()
    {
        var messages = await ChatMessagePresenter.GetLatestMessages();

        if (messages.Count == 0 || _latestMessageId == messages.First().Id) return;

        _latestMessageId = messages.First().Id;

        messagesListView.Items.Clear();

        foreach (var message in messages.Reverse())
        {
            var item = new ListViewItem(message.Created.ToString());
            item.SubItems.Add(message.Username);
            item.SubItems.Add(message.Message);
            messagesListView.Items.Add(item);
        }

        messagesListView.Items[^1].EnsureVisible();
    }

    private async Task LoadUsersList()
    {
        var users = await UserPresenter.FindByUsername();

        userListView.Items.Clear();

        foreach (var user in users)
        {
            var item = new ListViewItem(user.Id.ToString());
            item.SubItems.Add(user.Username);
            item.SubItems.Add(user.Created.ToString());
            item.SubItems.Add(user.LastAction.ToString());
            userListView.Items.Add(item);
        }
    }

    private static void CloseWholeApp(object sender, CancelEventArgs e)
    {
        Environment.Exit(Environment.ExitCode);
    }

    private void Tab_selected(object sender, TabControlEventArgs e)
    {
        if (e.TabPage?.Text == "Logout")
        {
            _messageAutoLoadTimer.Stop();
            Hide();

            _authForm.ClearInputs();
            _authForm.SetButtonsEnabled();
            _authForm.Display();
        }
    }

    private async void searchUsersButton_Click(object sender, EventArgs e)
    {
        searchUsersButton.Enabled = false;

        try
        {
            await LoadUsersList();
        }
        catch (Exception ex)
        {
            var materialDialog = new MaterialDialog(this, "Searching for users failed", ex.Message, "OK");
            materialDialog.ShowDialog(this);
        }

        searchUsersButton.Enabled = true;
    }

    private async void chatSendButton_Click(object sender, EventArgs e)
    {
        chatSendButton.Enabled = false;

        try
        {
            if (IsChatInputValid())
            {
                var chanePasswordAttempt = await ChatMessagePresenter.SendMessage();

                sendText.Text = "";

                var snackBarMessage = new MaterialSnackBar("Message was sent");
                snackBarMessage.Show(this);

                await LoadNewMessages();
            }
        }
        catch (Exception ex)
        {
            var materialDialog = new MaterialDialog(this, "Changing password failed", ex.Message, "OK");
            materialDialog.ShowDialog(this);
        }

        chatSendButton.Enabled = true;
    }

    private async void changePasswordButton_Click(object sender, EventArgs e)
    {
        changePassword.Enabled = false;

        try
        {
            if (ArePasswordInputsValid())
            {
                var chanePasswordAttempt = await UserPresenter.ChangePassword();

                oldPassword.Text = "";
                newPassword.Text = "";
                newPasswordCheck.Text = "";

                var snackBarMessage = new MaterialSnackBar("Password was changed");
                snackBarMessage.Show(this);
            }
        }
        catch (Exception ex)
        {
            var materialDialog = new MaterialDialog(this, "Changing password failed", ex.Message, "OK");
            materialDialog.ShowDialog(this);
        }

        changePassword.Enabled = true;
    }

    private bool ArePasswordInputsValid()
    {
        var errorText = "";

        if (!ValidatePassword(oldPassword))
            errorText += "Old password must be filled and be in size of 3 to 60 chars! ";

        if (!ValidatePassword(newPassword))
            errorText += "New password must be filled and be in size of 3 to 60 chars! ";

        if (!ValidatePassword(newPasswordCheck))
            errorText += "New password check must be filled and be in size of 3 to 60 chars! ";
        else if (!ValidatePassword(newPasswordCheck, true)) errorText += "New password must be the same!";

        var isValid = errorText.Equals("");
        if (!isValid)
        {
            var materialDialog = new MaterialDialog(this, "Invalid input", errorText, "OK");
            materialDialog.ShowDialog(this);
        }

        return isValid;
    }

    private bool IsChatInputValid()
    {
        var errorText = "";

        if (!ValidateChatInput()) errorText += "Chat message can not be empty! ";


        var isValid = errorText.Equals("");
        if (!isValid)
        {
            var materialDialog = new MaterialDialog(this, "Invalid chat input", errorText, "OK");
            materialDialog.ShowDialog(this);
        }

        return isValid;
    }

    private void oldPasswordInput_KeyUp(object sender, EventArgs e)
    {
        ValidatePassword(sender as MaterialTextBox);
    }

    private void newPasswordInput_KeyUp(object sender, EventArgs e)
    {
        ValidatePassword(sender as MaterialTextBox);
    }

    private void newPasswordCheckInput_KeyUp(object sender, EventArgs e)
    {
        ValidatePassword(sender as MaterialTextBox, true);
    }

    private bool ValidatePassword(MaterialTextBox sender, bool checkIfSame = false)
    {
        sender.SetErrorState(false);

        if (sender.Text.Length is < 3 or > 60)
        {
            sender.SetErrorState(true);
            return false;
        }

        if (!checkIfSame) return true;

        if (!newPassword.Text.Equals(newPasswordCheck.Text))
        {
            sender.SetErrorState(true);
            return false;
        }

        return true;
    }

    private bool ValidateChatInput()
    {
        if (sendText.Text.Length < 1) return false;

        return true;
    }
}