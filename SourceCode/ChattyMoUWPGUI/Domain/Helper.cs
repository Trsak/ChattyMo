using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ChattyMoUWPGUI.Domain;

public static class Helper
{
    public static readonly DependencyProperty AutoScrollProperty =
        DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(Helper),
            new PropertyMetadata(false, AutoScrollPropertyChanged));

    public static bool GetAutoScroll(DependencyObject obj)
    {
        return (bool) obj.GetValue(AutoScrollProperty);
    }

    public static void SetAutoScroll(DependencyObject obj, bool value)
    {
        obj.SetValue(AutoScrollProperty, value);
    }

    private static void AutoScrollPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var scrollViewer = d as ScrollViewer;

        if (scrollViewer != null && (bool) e.NewValue) scrollViewer.ChangeView(0.0f, double.MaxValue, 1.0f);
    }
}