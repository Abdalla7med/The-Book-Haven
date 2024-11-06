using System;
using System.ComponentModel.DataAnnotations;

public class DateRangeValidation : ValidationAttribute
{
    private readonly int _daysLimit;

    public DateRangeValidation(int daysLimit)
    {
        _daysLimit = daysLimit;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dueDate)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime maxDueDate = today.AddDays(_daysLimit);

            if (dueDate >= today && dueDate <= maxDueDate)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"The due date must be within {_daysLimit} days from today.");
        }
        return new ValidationResult("Invalid due date.");
    }
}
