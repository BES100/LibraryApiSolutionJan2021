using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class StatusController : ControllerBase
    {
        
        // GET /status
        [HttpGet("status")]
        public ActionResult<GetStatusResponse> GetTheStatus()
        {
            var response = new GetStatusResponse
            {
                Message = "Looks Good",
                LastChecked = DateTime.Now
            };
           
            return Ok(response);
        }

        // GET /sayhi/Putintane
        [HttpGet("sayhi/{name}")]
        public ActionResult SayHello(string name)
        {
            return Ok($"Hello, {name}!");
        }

        [HttpGet("orders/{year:int}/{month:int:range(1,12)}/{day:int}")]
        public ActionResult GetOrdersFor(int year, int month, int day)
        {
            return Ok($"Getting orders for {year}/{month}/{day}");
        }

        // Query Strings
        [HttpGet("employees")]
        public ActionResult GetEmployees([FromQuery] string department = "All")
        {
            return Ok($"Showing employees in department {department}");

        }
    }


    // Method Description (Request | Response) Requests to the server, REsponses come from the server
    public class GetStatusResponse
    {
        public string Message { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
