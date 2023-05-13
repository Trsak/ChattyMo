using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using ChattyMoUWPGUI.Context;
using ChattyMoUWPGUI.Model.Response;
using ChattyMoUWPGUI.Model.Respository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChattyMoUWPGUI.ViewModel;

public partial class ChatViewModel : ObservableObject
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly Timer _messageUpdateTimer;

    [ObservableProperty]
    private bool _isSendButtonEnabled;

    private long _lastMessageId;

    [ObservableProperty]
    private string? _textMessage;

    [ObservableProperty]
    private bool _wasNewMessageAdded;

    public ChatViewModel(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
        IsSendButtonEnabled = false;
        ChatMessages = new ObservableCollection<ChatMessage>();

        ReloadChatMessages(false);
        _messageUpdateTimer = new Timer(UpdateMessagesTask, null, 500, 5000);
    }

    public ObservableCollection<ChatMessage> ChatMessages { get; }

    partial void OnTextMessageChanging(string? value)
    {
        if (value is {Length: > 0})
        {
            if (!IsSendButtonEnabled) IsSendButtonEnabled = true;

            return;
        }

        if (IsSendButtonEnabled) IsSendButtonEnabled = false;
    }


    [RelayCommand]
    private async Task SendChatMessage()
    {
        IsSendButtonEnabled = false;

        try
        {
            await _chatMessageRepository.SendMessage(TextMessage);
            await ReloadChatMessages();
            TextMessage = null;
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private async void UpdateMessagesTask(object state)
    {
        await ReloadChatMessages();
    }

    private async Task ReloadChatMessages(bool shouldSaveLastMessageId = true)
    {
        try
        {
            var messages = await _chatMessageRepository.GetLatestMessages();

            if (messages.Count == 0) return;


            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                ChatMessages.Clear();
                foreach (var message in messages.Reverse()) ChatMessages.Add(message);

                if (_lastMessageId != messages.Last().Id)
                {
                    if (shouldSaveLastMessageId) _lastMessageId = messages.Last().Id;
                    WasNewMessageAdded = true;
                }

                WasNewMessageAdded = false;
            });
        }
        catch (Exception)
        {
            if (UserContext.User == null) _messageUpdateTimer.Dispose();
        }
    }
}