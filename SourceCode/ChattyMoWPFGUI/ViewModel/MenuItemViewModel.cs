using System.Windows.Controls;

namespace ChattyMoWPFGUI.ViewModel;

public class MenuItemViewModel : ViewModelBase
{
    private string _icon;
    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            OnPropertyChanged();
        }
    }

    public UserControl? UserControl { get; set; }
}