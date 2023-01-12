using FluentValidation;

namespace FluentValidationTest.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Name).NotNull();
            // Sample for complex validator, re-using the address validator
            RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
        }
    }
}