using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ChattyMoUWPGUI.Services;

public sealed class DialogService : IDialogService
{
    public Task ShowMessageDialogAsync(string title, string message)
    {
        ContentDialog dialog = new();
        dialog.Title = title;
        dialog.CloseButtonText = "Close";
        dialog.DefaultButton = ContentDialogButton.Close;
        dialog.Content = message;

        return dialog.ShowAsync().AsTask();
    }
}