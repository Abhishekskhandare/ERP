using ERP.Agent;
using ERP.DTO;
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

		[HttpPost]
		[Route("register")]
		public Result Register(DTOUserRegister userRegister)
		{
			Result result = new Result();
			try
			{
				UserAgent agent = new UserAgent();
				result.Success = agent.Register(userRegister);
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}

			return result;
		}
	}
}
