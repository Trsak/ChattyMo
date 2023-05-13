using Windows.UI.Xaml.Controls;
using ChattyMoUWPGUI.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;

// Dokumentaci k šabloně Prázdná aplikace najdete na adrese https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChattyMoUWPGUI.View;

/// <summary>
///     Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
/// </summary>
public sealed partial class ChatPage : Page
{
    public ChatPage()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<ChatViewModel>();
    }

    public ChatViewModel ViewModel => (ChatViewModel) DataContext;
}