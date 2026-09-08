using ERP.Agent;
using ERP.DTO;
using ERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserAgent _agent;

		public UserController(IUserAgent agent)
		{
			_agent = agent;
		}

		[HttpPost]
		[Route("register")]
		public async Task<Result> Register(DTOUserRegister userRegister)
		{
			Result result = new Result();
			try
			{
				result.Success = await _agent.Register(userRegister);
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}

			return result;
		}


		// change password   -  rutuja

		// login   -- aniket
	}
}
