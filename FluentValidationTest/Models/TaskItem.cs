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
                .Matches("^[a-zA-Z0-9 ]*$") // Regex
                .WithMessage("Description must be alphanumeric or spaces only") // Custom message
                .NotEmpty();

            RuleFor(t => t.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("DueDate must be in the future.");

            // Conditional validations
            When(t => t.RemindMe, () =>
            {
                RuleFor(t => t.ReminderMinutesBeforeDueDate)
                    .NotNull()
                    .WithMessage("ReminderMinutesBeforeDueDate must be set")
                    .GreaterThan(0)
                    .WithMessage("ReminderMinutesBeforeDueDate must be greater than 0")
                    .Must(value => value % 15 == 0) // Custom logic can be written inside Must.
                    .WithMessage("ReminderMinutesBeforeDueDate must be a multiple of 15");
            })
            .Otherwise(() =>
            {
                RuleFor(t => t.ReminderMinutesBeforeDueDate).Null();
            });

            // Validations for collections
            RuleForEach(t => t.SubItems)
                .NotEmpty()
                .WithMessage("Values in the SubItems array cannot be empty");

            // Custom validators, see below extension methods
            RuleFor(t => t.SubItems)
                .ListMustContainFewerThan(5);

            RuleFor(t => t.SubItems)
                .ListMustContainMoreThan(1);

            // Also can be written like this
            RuleFor(x => x.SubItems).Custom((list, context) =>
            {
                if (list.Count > 10)
                {
                    context.AddFailure("The list must contain 10 items or fewer");
                }
            });
        }
    }

    // We can also write custom validators like these
    public static class MyCustomValidators
    {
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must(list => list.Count < num).WithMessage("The list contains too many items");
        }

        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainMoreThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must((rootObject, list, context) =>
            {
                context.MessageFormatter.AppendArgument("MinElements", num);
                return list.Count > num;
            })
            .WithMessage("{PropertyName} must contain more than {MinElements} items.");
        }
    }
}