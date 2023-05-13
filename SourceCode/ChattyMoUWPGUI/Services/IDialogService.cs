using System.Threading.Tasks;

namespace ChattyMoUWPGUI.Services;

public interface IDialogService
{
    Task ShowMessageDialogAsync(string title, string message);
}