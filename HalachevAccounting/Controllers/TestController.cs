using Microsoft.AspNetCore.Mvc;

namespace HalachevAccounting.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TestController : ControllerBase
	{
		[HttpGet("add")]
		public IActionResult Add(int a, int b)
		{
			return Ok(a + b);
		}
	}
}