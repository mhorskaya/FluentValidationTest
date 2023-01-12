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
            //if (string.IsNullOrEmpty(model.Description))
            //{
            //    return BadRequest(new { Error = "Description cannot be empty" });
            //}

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var taskItem = new TaskItem();
            var validator = new TaskItemValidator();
            var result = validator.Validate(taskItem);

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