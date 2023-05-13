using System.Net.Http.Headers;
using ChattyMoWinFormsGUI.Model.Repository;
using ChattyMoWinFormsGUI.Presenter;
using ChattyMoWinFormsGUI.View;

namespace ChattyMoWinFormsGUI;

internal static class Program
{
    private static readonly HttpClient Client = new();

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        InitHttpClient();

        var authRepository = new UserRepository(Client);
        var chatMessageRepository = new ChatMessageRepository(Client);

        var chatty = new ChattyMo();
        var authForm = new AuthForm(chatty);

        var userPresenter = new UserPresenter(authRepository, authForm, chatty);
        var chatMessagePresenter = new ChatMessagePresenter(chatMessageRepository, chatty);

        Application.Run(authForm);
    }

    private static void InitHttpClient()
    {
        Client.BaseAddress = new Uri("http://localhost:8080/");
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
}