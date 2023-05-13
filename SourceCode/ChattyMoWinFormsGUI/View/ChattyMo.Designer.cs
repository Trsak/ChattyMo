namespace ChattyMoWinFormsGUI.View
{
    partial class ChattyMo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChattyMo));
            materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            chatTab = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            chatSendButton = new MaterialSkin.Controls.MaterialFloatingActionButton();
            sendText = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            messagesListView = new MaterialSkin.Controls.MaterialListView();
            dateTimeColumn = new ColumnHeader();
            userColumn = new ColumnHeader();
            messageColumn = new ColumnHeader();
            usersTab = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            usernameSearch = new MaterialSkin.Controls.MaterialTextBox();
            userListView = new MaterialSkin.Controls.MaterialListView();
            idUserColumn = new ColumnHeader();
            usernameUserColumn = new ColumnHeader();
            createdAtUserColumn = new ColumnHeader();
            lastActivityUserColumn = new ColumnHeader();
            searchUsersButton = new MaterialSkin.Controls.MaterialButton();
            passwordChangeTab = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            newPasswordCheck = new MaterialSkin.Controls.MaterialTextBox();
            changePassword = new MaterialSkin.Controls.MaterialButton();
            oldPassword = new MaterialSkin.Controls.MaterialTextBox();
            newPassword = new MaterialSkin.Controls.MaterialTextBox();
            exitTab = new TabPage();
            imageList1 = new ImageList(components);
            materialTabControl1.SuspendLayout();
            chatTab.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            usersTab.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            passwordChangeTab.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(chatTab);
            materialTabControl1.Controls.Add(usersTab);
            materialTabControl1.Controls.Add(passwordChangeTab);
            materialTabControl1.Controls.Add(exitTab);
            materialTabControl1.Depth = 0;
            materialTabControl1.Dock = DockStyle.Fill;
            materialTabControl1.ImageList = imageList1;
            materialTabControl1.Location = new Point(3, 64);
            materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.ShowToolTips = true;
            materialTabControl1.Size = new Size(1194, 683);
            materialTabControl1.TabIndex = 0;
            materialTabControl1.Selected += Tab_selected;
            // 
            // chatTab
            // 
            chatTab.Controls.Add(tableLayoutPanel1);
            chatTab.ImageKey = "chat.png";
            chatTab.Location = new Point(4, 39);
            chatTab.Name = "chatTab";
            chatTab.Padding = new Padding(3);
            chatTab.Size = new Size(1186, 640);
            chatTab.TabIndex = 0;
            chatTab.Text = "Chat";
            chatTab.ToolTipText = "Chat";
            chatTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 91.10169F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.898305F));
            tableLayoutPanel1.Controls.Add(chatSendButton, 1, 1);
            tableLayoutPanel1.Controls.Add(sendText, 0, 1);
            tableLayoutPanel1.Controls.Add(messagesListView, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 85.83106F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.1689377F));
            tableLayoutPanel1.Size = new Size(1180, 634);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // chatSendButton
            // 
            chatSendButton.Anchor = AnchorStyles.None;
            chatSendButton.Depth = 0;
            chatSendButton.Icon = (Image)resources.GetObject("chatSendButton.Icon");
            chatSendButton.Location = new Point(1099, 561);
            chatSendButton.MouseState = MaterialSkin.MouseState.HOVER;
            chatSendButton.Name = "chatSendButton";
            chatSendButton.Size = new Size(56, 56);
            chatSendButton.TabIndex = 0;
            chatSendButton.Text = "materialFloatingActionButton1";
            chatSendButton.UseVisualStyleBackColor = true;
            chatSendButton.Click += chatSendButton_Click;
            // 
            // sendText
            // 
            sendText.AnimateReadOnly = false;
            sendText.BackgroundImageLayout = ImageLayout.None;
            sendText.CharacterCasing = CharacterCasing.Normal;
            sendText.Depth = 0;
            sendText.Dock = DockStyle.Fill;
            sendText.HideSelection = true;
            sendText.Hint = "Text of your message";
            sendText.Location = new Point(3, 547);
            sendText.MaxLength = 32767;
            sendText.MouseState = MaterialSkin.MouseState.OUT;
            sendText.Name = "sendText";
            sendText.PasswordChar = '\0';
            sendText.ReadOnly = false;
            sendText.ScrollBars = ScrollBars.None;
            sendText.SelectedText = "";
            sendText.SelectionLength = 0;
            sendText.SelectionStart = 0;
            sendText.ShortcutsEnabled = true;
            sendText.Size = new Size(1069, 84);
            sendText.TabIndex = 1;
            sendText.TabStop = false;
            sendText.TextAlign = HorizontalAlignment.Left;
            sendText.UseSystemPasswordChar = false;
            // 
            // messagesListView
            // 
            messagesListView.Alignment = ListViewAlignment.SnapToGrid;
            messagesListView.AutoSizeTable = false;
            messagesListView.BackColor = Color.FromArgb(255, 255, 255);
            messagesListView.BorderStyle = BorderStyle.None;
            messagesListView.Columns.AddRange(new ColumnHeader[] { dateTimeColumn, userColumn, messageColumn });
            tableLayoutPanel1.SetColumnSpan(messagesListView, 2);
            messagesListView.Depth = 0;
            messagesListView.Dock = DockStyle.Fill;
            messagesListView.FullRowSelect = true;
            messagesListView.Location = new Point(3, 3);
            messagesListView.MinimumSize = new Size(500, 500);
            messagesListView.MouseLocation = new Point(-1, -1);
            messagesListView.MouseState = MaterialSkin.MouseState.OUT;
            messagesListView.Name = "messagesListView";
            messagesListView.OwnerDraw = true;
            messagesListView.Size = new Size(1174, 538);
            messagesListView.TabIndex = 2;
            messagesListView.UseCompatibleStateImageBehavior = false;
            messagesListView.View = System.Windows.Forms.View.Details;
            // 
            // dateTimeColumn
            // 
            dateTimeColumn.Text = "Date and time";
            dateTimeColumn.Width = 150;
            // 
            // userColumn
            // 
            userColumn.Text = "User";
            userColumn.Width = 150;
            // 
            // messageColumn
            // 
            messageColumn.Text = "Message";
            messageColumn.Width = 800;
            // 
            // usersTab
            // 
            usersTab.Controls.Add(tableLayoutPanel3);
            usersTab.ImageKey = "users.png";
            usersTab.Location = new Point(4, 39);
            usersTab.Name = "usersTab";
            usersTab.Size = new Size(1186, 640);
            usersTab.TabIndex = 3;
            usersTab.Text = "Users";
            usersTab.ToolTipText = "Users";
            usersTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.8887024F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.1112976F));
            tableLayoutPanel3.Controls.Add(usernameSearch, 0, 0);
            tableLayoutPanel3.Controls.Add(userListView, 0, 1);
            tableLayoutPanel3.Controls.Add(searchUsersButton, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1186, 640);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // usernameSearch
            // 
            usernameSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            usernameSearch.AnimateReadOnly = false;
            usernameSearch.BorderStyle = BorderStyle.None;
            usernameSearch.Depth = 0;
            usernameSearch.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            usernameSearch.Hint = "Search by Username";
            usernameSearch.LeadingIcon = null;
            usernameSearch.Location = new Point(327, 15);
            usernameSearch.Margin = new Padding(15);
            usernameSearch.MaxLength = 50;
            usernameSearch.MouseState = MaterialSkin.MouseState.OUT;
            usernameSearch.Multiline = false;
            usernameSearch.Name = "usernameSearch";
            usernameSearch.ScrollBars = RichTextBoxScrollBars.None;
            usernameSearch.Size = new Size(392, 50);
            usernameSearch.TabIndex = 3;
            usernameSearch.Text = "";
            usernameSearch.TrailingIcon = null;
            // 
            // userListView
            // 
            userListView.AllowColumnReorder = true;
            userListView.AutoSizeTable = false;
            userListView.BackColor = Color.FromArgb(255, 255, 255);
            userListView.BorderStyle = BorderStyle.None;
            userListView.Columns.AddRange(new ColumnHeader[] { idUserColumn, usernameUserColumn, createdAtUserColumn, lastActivityUserColumn });
            tableLayoutPanel3.SetColumnSpan(userListView, 2);
            userListView.Depth = 0;
            userListView.Dock = DockStyle.Fill;
            userListView.FullRowSelect = true;
            userListView.Location = new Point(3, 83);
            userListView.MinimumSize = new Size(200, 100);
            userListView.MouseLocation = new Point(-1, -1);
            userListView.MouseState = MaterialSkin.MouseState.OUT;
            userListView.Name = "userListView";
            userListView.OwnerDraw = true;
            userListView.Size = new Size(1180, 554);
            userListView.TabIndex = 5;
            userListView.UseCompatibleStateImageBehavior = false;
            userListView.View = System.Windows.Forms.View.Details;
            // 
            // idUserColumn
            // 
            idUserColumn.Text = "ID";
            // 
            // usernameUserColumn
            // 
            usernameUserColumn.Text = "Username";
            usernameUserColumn.Width = 700;
            // 
            // createdAtUserColumn
            // 
            createdAtUserColumn.Text = "Created at";
            createdAtUserColumn.Width = 180;
            // 
            // lastActivityUserColumn
            // 
            lastActivityUserColumn.Text = "Last activity";
            lastActivityUserColumn.Width = 180;
            // 
            // searchUsersButton
            // 
            searchUsersButton.AutoSize = false;
            searchUsersButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            searchUsersButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            searchUsersButton.Depth = 0;
            searchUsersButton.HighEmphasis = true;
            searchUsersButton.Icon = null;
            searchUsersButton.Location = new Point(749, 22);
            searchUsersButton.Margin = new Padding(15, 22, 15, 15);
            searchUsersButton.MouseState = MaterialSkin.MouseState.HOVER;
            searchUsersButton.Name = "searchUsersButton";
            searchUsersButton.NoAccentTextColor = Color.Empty;
            searchUsersButton.Size = new Size(93, 35);
            searchUsersButton.TabIndex = 4;
            searchUsersButton.Text = "Search";
            searchUsersButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            searchUsersButton.UseAccentColor = false;
            searchUsersButton.UseVisualStyleBackColor = true;
            searchUsersButton.Click += searchUsersButton_Click;
            // 
            // passwordChangeTab
            // 
            passwordChangeTab.Controls.Add(tableLayoutPanel2);
            passwordChangeTab.ImageKey = "password.png";
            passwordChangeTab.Location = new Point(4, 39);
            passwordChangeTab.Name = "passwordChangeTab";
            passwordChangeTab.Padding = new Padding(3);
            passwordChangeTab.Size = new Size(1186, 640);
            passwordChangeTab.TabIndex = 1;
            passwordChangeTab.Text = "Change Password";
            passwordChangeTab.ToolTipText = "Change password";
            passwordChangeTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(newPasswordCheck, 0, 3);
            tableLayoutPanel2.Controls.Add(changePassword, 0, 3);
            tableLayoutPanel2.Controls.Add(oldPassword, 0, 0);
            tableLayoutPanel2.Controls.Add(newPassword, 0, 1);
            tableLayoutPanel2.Location = new Point(379, 178);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(375, 219);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // newPasswordCheck
            // 
            newPasswordCheck.AnimateReadOnly = false;
            newPasswordCheck.BorderStyle = BorderStyle.None;
            newPasswordCheck.Depth = 0;
            newPasswordCheck.Dock = DockStyle.Fill;
            newPasswordCheck.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            newPasswordCheck.Hint = "New password again";
            newPasswordCheck.LeadingIcon = null;
            newPasswordCheck.Location = new Point(3, 115);
            newPasswordCheck.MaxLength = 50;
            newPasswordCheck.MouseState = MaterialSkin.MouseState.OUT;
            newPasswordCheck.Multiline = false;
            newPasswordCheck.Name = "newPasswordCheck";
            newPasswordCheck.Password = true;
            newPasswordCheck.ScrollBars = RichTextBoxScrollBars.None;
            newPasswordCheck.Size = new Size(369, 50);
            newPasswordCheck.TabIndex = 4;
            newPasswordCheck.Text = "";
            newPasswordCheck.TrailingIcon = null;
            newPasswordCheck.KeyUp += newPasswordCheckInput_KeyUp;
            // 
            // changePassword
            // 
            changePassword.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            changePassword.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            changePassword.Depth = 0;
            changePassword.Dock = DockStyle.Fill;
            changePassword.HighEmphasis = true;
            changePassword.Icon = null;
            changePassword.Location = new Point(4, 174);
            changePassword.Margin = new Padding(4, 6, 4, 6);
            changePassword.MouseState = MaterialSkin.MouseState.HOVER;
            changePassword.Name = "changePassword";
            changePassword.NoAccentTextColor = Color.Empty;
            changePassword.Size = new Size(367, 39);
            changePassword.TabIndex = 3;
            changePassword.Text = "Change Password";
            changePassword.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            changePassword.UseAccentColor = false;
            changePassword.UseVisualStyleBackColor = true;
            changePassword.Click += changePasswordButton_Click;
            // 
            // oldPassword
            // 
            oldPassword.AnimateReadOnly = false;
            oldPassword.BorderStyle = BorderStyle.None;
            oldPassword.Depth = 0;
            oldPassword.Dock = DockStyle.Fill;
            oldPassword.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            oldPassword.Hint = "Current password";
            oldPassword.LeadingIcon = null;
            oldPassword.Location = new Point(3, 3);
            oldPassword.MaxLength = 50;
            oldPassword.MouseState = MaterialSkin.MouseState.OUT;
            oldPassword.Multiline = false;
            oldPassword.Name = "oldPassword";
            oldPassword.Password = true;
            oldPassword.ScrollBars = RichTextBoxScrollBars.None;
            oldPassword.Size = new Size(369, 50);
            oldPassword.TabIndex = 2;
            oldPassword.Text = "";
            oldPassword.TrailingIcon = null;
            oldPassword.KeyUp += oldPasswordInput_KeyUp;
            // 
            // newPassword
            // 
            newPassword.AnimateReadOnly = false;
            newPassword.BorderStyle = BorderStyle.None;
            newPassword.Depth = 0;
            newPassword.Dock = DockStyle.Fill;
            newPassword.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            newPassword.Hint = "New password";
            newPassword.LeadingIcon = null;
            newPassword.Location = new Point(3, 59);
            newPassword.MaxLength = 50;
            newPassword.MouseState = MaterialSkin.MouseState.OUT;
            newPassword.Multiline = false;
            newPassword.Name = "newPassword";
            newPassword.Password = true;
            newPassword.ScrollBars = RichTextBoxScrollBars.None;
            newPassword.Size = new Size(369, 50);
            newPassword.TabIndex = 1;
            newPassword.Text = "";
            newPassword.TrailingIcon = null;
            newPassword.KeyUp += newPasswordInput_KeyUp;
            // 
            // exitTab
            // 
            exitTab.ImageKey = "exit.png";
            exitTab.Location = new Point(4, 39);
            exitTab.Name = "exitTab";
            exitTab.Size = new Size(1186, 740);
            exitTab.TabIndex = 2;
            exitTab.Text = "Logout";
            exitTab.ToolTipText = "Logout";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "password.png");
            imageList1.Images.SetKeyName(1, "exit.png");
            imageList1.Images.SetKeyName(2, "chat.png");
            imageList1.Images.SetKeyName(3, "send.png");
            imageList1.Images.SetKeyName(4, "users.png");
            // 
            // ChattyMo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 750);
            Controls.Add(materialTabControl1);
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = materialTabControl1;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ChattyMo";
            Sizable = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChattyMo";
            materialTabControl1.ResumeLayout(false);
            chatTab.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            usersTab.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            passwordChangeTab.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private TabPage chatTab;
        private TabPage passwordChangeTab;
        private ImageList imageList1;
        private TabPage exitTab;
        private TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialFloatingActionButton chatSendButton;
        private MaterialSkin.Controls.MaterialMultiLineTextBox2 sendText;
        private MaterialSkin.Controls.MaterialListView messagesListView;
        private TableLayoutPanel tableLayoutPanel2;
        private MaterialSkin.Controls.MaterialTextBox oldPassword;
        private MaterialSkin.Controls.MaterialTextBox newPassword;
        private MaterialSkin.Controls.MaterialTextBox newPasswordCheck;
        private MaterialSkin.Controls.MaterialButton changePassword;
        private ColumnHeader dateTimeColumn;
        private ColumnHeader userColumn;
        private ColumnHeader messageColumn;
        private TabPage usersTab;
        private TableLayoutPanel tableLayoutPanel3;
        private MaterialSkin.Controls.MaterialTextBox usernameSearch;
        private MaterialSkin.Controls.MaterialButton searchUsersButton;
        private MaterialSkin.Controls.MaterialListView userListView;
        private ColumnHeader idUserColumn;
        private ColumnHeader usernameUserColumn;
        private ColumnHeader createdAtUserColumn;
        private ColumnHeader lastActivityUserColumn;
    }
}