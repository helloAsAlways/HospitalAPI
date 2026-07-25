using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]

public class userController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetName()
    {
        return Ok("Sammy");
    }
}