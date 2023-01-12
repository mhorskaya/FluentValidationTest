using FluentValidation;

namespace FluentValidationTest.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleSet("Names", () =>
            {
                RuleFor(x => x.FirstName).NotNull();
                RuleFor(x => x.LastName).NotNull();
            });

            RuleFor(x => x.Id).NotEqual(0);

            // We can make it stop on the first error. In this case if it is null.
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotNull().NotEqual("foo");
        }

        //// Only partial rulesets can be validated with options.IncludeRuleSets() method.
        //public void Test()
        //{
        //    var validator = new PersonValidator();
        //    var person = new Person();
        //    var result = validator.Validate(person, options => options.IncludeRuleSets("Names"));
        //}
    }
}