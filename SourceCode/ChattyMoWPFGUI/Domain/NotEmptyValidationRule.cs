using System.Globalization;
using System.Windows.Controls;

namespace ChattyMoWPFGUI.Domain;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        var inputValue = (string) value;
        if (inputValue == "" || value == null) return ValidationResult.ValidResult;

        return string.IsNullOrWhiteSpace(inputValue ?? "")
            ? new ValidationResult(false, "Field is required.")
            : ValidationResult.ValidResult;
    }
}