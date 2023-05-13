using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChattyMoWPFGUI.Command;
using ChattyMoWPFGUI.Context;
using ChattyMoWPFGUI.Model.Repository;
using ChattyMoWPFGUI.Model.Response;
using MaterialDesignThemes.Wpf;

namespace ChattyMoWPFGUI.ViewModel;

public class ChatViewModel : ViewModelBase
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly Timer _messageUpdateTimer;
    private string _chatMessage;
    private long _lastMessageId;
    private bool _wasNewMessageAdded;

    public ChatViewModel()
    {
        _chatMessageRepository = new ChatMessageRepository();

        SetButtonEnabled(true);
        MessageQueue = new SnackbarMessageQueue();
        SendMessageCommand = new ViewModelCommand(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);
        ChatMessages = new ObservableCollection<ChatMessage>();
        ReloadChatMessages();

        _messageUpdateTimer = new Timer(UpdateMessagesTask, null, 5000, 5000);
    }

    public ICommand SendMessageCommand { get; }
    public bool SendButtonEnabled { get; set; }
    public ISnackbarMessageQueue MessageQueue { get; }
    public ObservableCollection<ChatMessage> ChatMessages { get; }

    public string ChatMessageText
    {
        get => _chatMessage;

        set
        {
            _chatMessage = value;
            OnPropertyChanged();
        }
    }

    public bool WasNewMessageAdded
    {
        get => _wasNewMessageAdded;

        set
        {
            _wasNewMessageAdded = value;
            OnPropertyChanged();
        }
    }

    private async void UpdateMessagesTask(object state)
    {
        await ReloadChatMessages();
    }

    private bool CanExecuteSendMessageCommand(object obj)
    {
        return !string.IsNullOrWhiteSpace(ChatMessageText);
    }

    private async void ExecuteSendMessageCommand(object obj)
    {
        SetButtonEnabled(false);

        try
        {
            await _chatMessageRepository.SendMessage(ChatMessageText);
            await ReloadChatMessages();

            ChatMessageText = "";
            MessageQueue.Enqueue("Message was sent");
        }
        catch (Exception e)
        {
            MessageQueue.Enqueue(e.Message);
        }

        SetButtonEnabled(true);
    }

    private void SetButtonEnabled(bool enabled)
    {
        if (SendButtonEnabled == enabled) return;

        SendButtonEnabled = enabled;
        OnPropertyChanged(nameof(SendButtonEnabled));
    }

    private async Task ReloadChatMessages()
    {
        try
        {
            var messages = await _chatMessageRepository.GetLatestMessages();

            if (messages.Count == 0) return;

            if (_lastMessageId != messages.Last().Id)
            {
                _lastMessageId = messages.Last().Id;
                WasNewMessageAdded = true;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                ChatMessages.Clear();
                foreach (var message in messages.Reverse()) ChatMessages.Add(message);
            });

            WasNewMessageAdded = false;
        }
        catch (Exception)
        {
            if (UserContext.User == null) _messageUpdateTimer.Dispose();
        }
    }
}