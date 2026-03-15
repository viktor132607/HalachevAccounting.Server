using Microsoft.AspNetCore.Mvc;

namespace HalachevAccounting.Controllers;

[ApiController]
[Route("api/home")]
public class HomeController : ControllerBase
{
	[HttpGet]
	public IActionResult Get()
	{
		return Ok("API running");
	}
}