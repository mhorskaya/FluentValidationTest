using FluentValidation;

namespace FluentValidationTest.Models
{
    public class TaskItem
    {
        //[Required]
        //[MinLength(3)]
        public string Description { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public bool RemindMe { get; set; }

        public int? ReminderMinutesBeforeDueDate { get; set; }

        public List<string> SubItems { get; set; } = null!;
    }

    public class TaskItemValidator : AbstractValidator<TaskItem>
    {
        public TaskItemValidator()
        {
            RuleFor(t => t.Description)
                .Matches("^[a-zA-Z0-9 ]*$")
                .WithMessage("Description must be alphanumeric or spaces only")
                .NotEmpty();

            RuleFor(t => t.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("DueDate must be in the future.");

            When(t => t.RemindMe, () =>
            {
                RuleFor(t => t.ReminderMinutesBeforeDueDate)
                    .NotNull()
                    .WithMessage("ReminderMinutesBeforeDueDate must be set")
                    .GreaterThan(0)
                    .WithMessage("ReminderMinutesBeforeDueDate must be greater than 0")
                    .Must(value => value % 15 == 0)
                    .WithMessage("ReminderMinutesBeforeDueDate must be a multiple of 15");
            });

            RuleForEach(t => t.SubItems)
                .NotEmpty()
                .WithMessage("Values in the SubItems array cannot be empty");
        }
    }
}