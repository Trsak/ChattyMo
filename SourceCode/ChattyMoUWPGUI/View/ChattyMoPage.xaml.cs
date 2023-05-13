using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ChattyMoUWPGUI.Context;
using ChattyMoUWPGUI.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationViewBackRequestedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs;
using NavigationViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs;

namespace ChattyMoUWPGUI.View;

public sealed partial class ChattyMoPage : Page
{
    private readonly IReadOnlyCollection<MenuItem> NavigationItems;

    public ChattyMoPage()
    {
        InitializeComponent();

        NavigationItems = new[]
        {
            new MenuItem(ChatItem, typeof(ChatPage), "Chat"),
            new MenuItem(ListUsersItem, typeof(UserListPage), "Users"),
            new MenuItem(ChangePasswordItem, typeof(ChangePasswordPage), "Change password"),
            new MenuItem(LogoutItem, null, "Logout")
        };

        Window.Current.SetTitleBar(TitleBarBorder);

        DataContext = Ioc.Default.GetRequiredService<ChattyMoViewModel>();
    }

    public ChattyMoViewModel ViewModel => (ChattyMoViewModel) DataContext;

    private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (NavigationItems.FirstOrDefault(item => item.Item == args.InvokedItemContainer)?.PageType is Type pageType)
        {
            NavigationFrame.Navigate(pageType);
        }
        else
        {
            UserContext.SetUser(null);
            HttpClientManager.Client.DefaultRequestHeaders.Authorization = null;

            Window.Current.Content = new AuthPage();
        }
    }

    private void NavigationFrame_OnNavigated(object sender, NavigationEventArgs e)
    {
        NavigationView.IsBackEnabled = ((Frame) sender).BackStackDepth > 0;
    }

    private void NavigationView_OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (NavigationFrame.BackStack.LastOrDefault() is PageStackEntry entry)
        {
            NavigationView.SelectedItem = NavigationItems.First(item => item.PageType == entry.SourcePageType).Item;

            NavigationFrame.GoBack();
        }
    }

    private void ChattyMo_OnLoaded(object sender, RoutedEventArgs e)
    {
        NavigationView.SelectedItem = ChatItem;

        NavigationFrame.Navigate(typeof(ChatPage));

        ApplicationView.GetForCurrentView().TryResizeView(new Size(1150, 900));
    }
}

public sealed class MenuItem
{
    public MenuItem(NavigationViewItem viewItem, Type? pageType, string? name = null)
    {
        Item = viewItem;
        PageType = pageType;
        Name = name;
    }

    public NavigationViewItem Item { get; }

    public Type? PageType { get; }

    public string? Name { get; }
}