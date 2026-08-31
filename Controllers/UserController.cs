using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		public UserController()
		{
		}

		[HttpGet]
		[Route("abhishek")]
		public void GetUser()
		{

			// Implementation for getting user
		}
	}
}
