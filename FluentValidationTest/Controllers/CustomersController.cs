using FluentValidationTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Customer model)
        {
            return Ok(model);
        }
    }
}