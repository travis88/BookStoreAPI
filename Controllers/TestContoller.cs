using Microsoft.AspNetCore.Mvc;
using AspNetCorePublisherWebAPI.Models;

namespace AspNetCorePublisherWebAPI.Controllers 
{
    [Route("api/test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var model = new Message
            {
                Id = 1,
                Text = "Message, from the Get action."
            }; 

            return Ok(model);
        }
    }
}