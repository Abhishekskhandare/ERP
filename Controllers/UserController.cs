using ERP.Agent;
using ERP.Data;
using ERP.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ERPDbContext _context;

		public UserController(ERPDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Route("register")]
		public async Task<Result> Register(DTOUserRegister userRegister)
		{
			Result result = new Result();
			try
			{
				UserAgent agent = new UserAgent(_context);
				result.Success = await agent.Register(userRegister);
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}

			return result;
		}

		[HttpGet]
		[Route("dbtest")]
		public async Task<Result> DbTest()
		{
			var result = new Result();
			try
			{
				// Check basic connectivity
				if (await _context.Database.CanConnectAsync())
				{
					var userCount = await _context.Users.CountAsync();
					result.Success = true;
					result.Data = new { CanConnect = true, UserCount = userCount };
				}
				else
				{
					result.Success = false;
					result.Message = "Unable to connect to the database.";
				}
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message + (ex.InnerException is null ? string.Empty : " | " + ex.InnerException.Message);
			}

			return result;
		}

		// change password   -  rutuja

		// login   -- aniket
	}
}
