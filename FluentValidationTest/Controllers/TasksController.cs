using FluentValidationTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;

        public TasksController(ILogger<TasksController> logger) => _logger = logger;

        [HttpPost]
        public IActionResult Post(TaskItem model)
        {
            // We can validate model manually like this...
            //if (string.IsNullOrEmpty(model.Description))
            //{
            //    return BadRequest(new { Error = "Description cannot be empty" });
            //}

            // Or if there are validation attributes in the model class, we can check validity like this...
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            // Since FluentValidation is registered with ASP.NET project, rules defined in the class
            // `TaskItemValidator : AbstractValidator<TaskItem>` will be applied.

            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            // We can create an instance of the validator and call the Validate method as well.
            var taskItem = new TaskItem();
            var validator = new TaskItemValidator();
            var result = validator.Validate(taskItem);

            // or validator.ValidateAndThrow(taskItem)

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return Ok(result.ToString());
        }
    }
}