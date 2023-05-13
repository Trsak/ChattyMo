namespace ChattyMoWinFormsGUI.View;

partial class AuthForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
        loginButton = new MaterialSkin.Controls.MaterialButton();
        usernameInput = new MaterialSkin.Controls.MaterialTextBox();
        passwordInput = new MaterialSkin.Controls.MaterialTextBox();
        registerButton = new MaterialSkin.Controls.MaterialButton();
        SuspendLayout();
        // 
        // loginButton
        // 
        resources.ApplyResources(loginButton, "loginButton");
        loginButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
        loginButton.Depth = 0;
        loginButton.HighEmphasis = true;
        loginButton.Icon = null;
        loginButton.MouseState = MaterialSkin.MouseState.HOVER;
        loginButton.Name = "loginButton";
        loginButton.NoAccentTextColor = Color.Empty;
        loginButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
        loginButton.UseAccentColor = false;
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += loginButton_Click;
        // 
        // usernameInput
        // 
        usernameInput.AnimateReadOnly = false;
        usernameInput.BorderStyle = BorderStyle.None;
        usernameInput.Depth = 0;
        resources.ApplyResources(usernameInput, "usernameInput");
        usernameInput.LeadingIcon = null;
        usernameInput.MouseState = MaterialSkin.MouseState.OUT;
        usernameInput.Name = "usernameInput";
        usernameInput.TrailingIcon = null;
        usernameInput.KeyUp += usernameInput_KeyUp;
        // 
        // passwordInput
        // 
        passwordInput.AnimateReadOnly = false;
        passwordInput.BorderStyle = BorderStyle.None;
        passwordInput.Depth = 0;
        resources.ApplyResources(passwordInput, "passwordInput");
        passwordInput.LeadingIcon = null;
        passwordInput.MouseState = MaterialSkin.MouseState.OUT;
        passwordInput.Name = "passwordInput";
        passwordInput.Password = true;
        passwordInput.TrailingIcon = null;
        passwordInput.KeyUp += passwordInput_KeyUp;
        // 
        // registerButton
        // 
        resources.ApplyResources(registerButton, "registerButton");
        registerButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
        registerButton.Depth = 0;
        registerButton.HighEmphasis = true;
        registerButton.Icon = null;
        registerButton.MouseState = MaterialSkin.MouseState.HOVER;
        registerButton.Name = "registerButton";
        registerButton.NoAccentTextColor = Color.Empty;
        registerButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
        registerButton.UseAccentColor = false;
        registerButton.UseVisualStyleBackColor = true;
        registerButton.Click += registerButton_Click;
        // 
        // AuthForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(registerButton);
        Controls.Add(passwordInput);
        Controls.Add(usernameInput);
        Controls.Add(loginButton);
        MaximizeBox = false;
        Name = "AuthForm";
        Sizable = false;
        ResumeLayout(false);
    }

    #endregion

    private MaterialSkin.Controls.MaterialButton loginButton;
    private MaterialSkin.Controls.MaterialTextBox usernameInput;
    private MaterialSkin.Controls.MaterialTextBox passwordInput;
    private MaterialSkin.Controls.MaterialButton registerButton;
}