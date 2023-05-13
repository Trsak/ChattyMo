using ChattyMoWinFormsGUI.Model.Repository;
using ChattyMoWinFormsGUI.Model.Response;
using ChattyMoWinFormsGUI.View;

namespace ChattyMoWinFormsGUI.Presenter;

public class ChatMessagePresenter
{
    private readonly IChattyMoView _chatView;
    private readonly IChatMessageRepository _repository;

    public ChatMessagePresenter(IChatMessageRepository repository, IChattyMoView view)
    {
        _chatView = view;
        _repository = repository;

        _chatView.ChatMessagePresenter = this;
    }

    public async Task<bool> SendMessage()
    {
        return await _repository.SendMessage(_chatView.ChatMessage);
    }

    public async Task<ICollection<ChatMessages>> GetLatestMessages()
    {
        return await _repository.GetLatestMessages();
    }
}