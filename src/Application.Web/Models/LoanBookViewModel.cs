using System.ComponentModel.DataAnnotations;
using Application.Shared;
public class LoanBookViewModel
{
    public ReadBookDto Book { get; set; }
    public Guid MemberId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Due date is required.")]
    [DataType(DataType.Date)]
    [DateRangeValidation(7, ErrorMessage = "The due date must be within 7 days from today.")]
    public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(7);
}
