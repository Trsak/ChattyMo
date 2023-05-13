using System.Globalization;
using System.Windows.Controls;

namespace ChattyMoWPFGUI.Domain;

public class MaxMinCharsValidationRule : ValidationRule
{
    public int Min { get; set; }
    public int Max { get; set; }

    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        var inputValue = (string) value;
        if (inputValue == "" || value == null) return ValidationResult.ValidResult;

        if (inputValue.Length < Min) return new ValidationResult(false, $"Input must have at least {Min} characters.");

        if (inputValue.Length > Max)
            return new ValidationResult(false, $"Input can not have more than {Max} characters.");

        return ValidationResult.ValidResult;
    }
}