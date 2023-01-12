using FluentValidation;

namespace FluentValidationTest.Models
{
    public class Ticket
    {
        public List<Order> Orders { get; set; } = null!;
    }

    public class Order
    {
        public int Total { get; set; }
    }

    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            // This rule acts on the whole collection (using RuleFor)
            RuleFor(x => x.Orders)
              .Must(x => x.Count <= 10).WithMessage("No more than 10 orders are allowed");

            // This rule acts on each individual element (using RuleForEach)
            RuleForEach(x => x.Orders)
              .Must(order => order.Total > 0).WithMessage("Orders must have a total of more than 0");

            // The above 2 rules could be re-written as:
            RuleFor(x => x.Orders)
              .Must(x => x.Count <= 10).WithMessage("No more than 10 orders are allowed")
              .ForEach(orderRule =>
              {
                  orderRule.Must(order => order.Total > 0).WithMessage("Orders must have a total of more than 0");
              });
        }
    }
}