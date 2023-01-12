using System.ComponentModel.DataAnnotations;

namespace FluentValidationTest
{
    public class TaskItem
    {
        public string Description { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public bool RemindMe { get; set; }

        public int? ReminderMinutesBeforeDueDate { get; set; }

        public List<string> SubItems { get; set; }
    }
}