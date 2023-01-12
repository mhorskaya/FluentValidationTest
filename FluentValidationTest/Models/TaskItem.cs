﻿using FluentValidation;

namespace FluentValidationTest.Models
{
    public class TaskItem
    {
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
            RuleFor(t => t.Description).NotEmpty();
        }
    }
}