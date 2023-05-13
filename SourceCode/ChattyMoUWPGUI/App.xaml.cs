using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using ChattyMoUWPGUI.Helpers;
using ChattyMoUWPGUI.Model.Respository;
using ChattyMoUWPGUI.Services;
using ChattyMoUWPGUI.View;
using ChattyMoUWPGUI.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ChattyMoUWPGUI;

sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();

        ApplicationView.PreferredLaunchViewSize = new Size(650, 350);
        ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        if (Window.Current.Content is null)
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IDialogService, DialogService>()
                    .AddSingleton<IUserRepository, UserRepository>()
                    .AddSingleton<IChatMessageRepository, ChatMessageRepository>()
                    .AddTransient<ChattyMoViewModel>()
                    .AddTransient<ChatViewModel>()
                    .AddTransient<UserListViewModel>()
                    .AddTransient<ChangePasswordViewModel>()
                    .AddTransient<AuthFormViewModel>()
                    .BuildServiceProvider());

            Window.Current.Content = new AuthPage();

            TitleBarHelper.StyleTitleBar();
            TitleBarHelper.ExpandViewIntoTitleBar();
        }

        if (e.PrelaunchActivated == false)
        {
            CoreApplication.EnablePrelaunch(true);

            Window.Current.Activate();
        }
    }
}