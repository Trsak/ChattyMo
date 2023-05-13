using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace ChattyMoUWPGUI.Helpers;

public static class TitleBarHelper
{
    public static void StyleTitleBar()
    {
        var titleBar = ApplicationView.GetForCurrentView().TitleBar;

        // Transparent colors
        titleBar.ForegroundColor = Colors.Transparent;
        titleBar.BackgroundColor = Colors.Transparent;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.InactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
    }

    /// <summary>
    ///     Sets up the app UI to be expanded into the title bar.
    /// </summary>
    public static void ExpandViewIntoTitleBar()
    {
        var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        coreTitleBar.ExtendViewIntoTitleBar = true;
    }
}