#nullable enable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ChattyMoUWPGUI.Control;

[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_WarningIcon", Type = typeof(FontIcon))]
public sealed class ValidationTextBox : ContentControl
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(ValidationTextBox),
        new PropertyMetadata(default(string)));

    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
        nameof(HeaderText),
        typeof(string),
        typeof(ValidationTextBox),
        new PropertyMetadata(default(string)));

    public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
        nameof(PlaceholderText),
        typeof(string),
        typeof(ValidationTextBox),
        new PropertyMetadata(default(string)));

    public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
        nameof(PropertyName),
        typeof(string),
        typeof(ValidationTextBox),
        new PropertyMetadata(PropertyNameProperty, OnPropertyNamePropertyChanged));

    private INotifyDataErrorInfo? oldDataContext;
    private TextBox? textBox;

    private FontIcon? warningIcon;

    public ValidationTextBox()
    {
        DataContextChanged += ValidationTextBox_DataContextChanged;
    }

    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string HeaderText
    {
        get => (string) GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public string PlaceholderText
    {
        get => (string) GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public string PropertyName
    {
        get => (string) GetValue(PropertyNameProperty);
        set => SetValue(PropertyNameProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        textBox = (TextBox) GetTemplateChild("PART_TextBox")!;
        warningIcon = (FontIcon) GetTemplateChild("PART_WarningIcon")!;

        textBox.TextChanged += TextBox_TextChanged;
    }


    private static void OnPropertyNamePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (args.NewValue is not string {Length: > 0} propertyName) return;

        ((ValidationTextBox) sender).RefreshErrors();
    }

    private void ValidationTextBox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (oldDataContext is not null) oldDataContext.ErrorsChanged -= DataContext_ErrorsChanged;

        if (args.NewValue is INotifyDataErrorInfo dataContext)
        {
            oldDataContext = dataContext;

            oldDataContext.ErrorsChanged += DataContext_ErrorsChanged;
        }

        RefreshErrors();
    }

    private void DataContext_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        RefreshErrors();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        Text = ((TextBox) sender).Text;
    }

    private void RefreshErrors()
    {
        if (this.warningIcon is not FontIcon warningIcon ||
            PropertyName is not string propertyName ||
            DataContext is not INotifyDataErrorInfo dataContext)
            return;

        var result = dataContext.GetErrors(propertyName).OfType<ValidationResult>().FirstOrDefault();

        warningIcon.Visibility = result is not null ? Visibility.Visible : Visibility.Collapsed;

        if (result is not null) ToolTipService.SetToolTip(warningIcon, result.ErrorMessage);
    }
}