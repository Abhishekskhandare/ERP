using ERP.Agent;
using ERP.DTO;
using ERP.EFModels;
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
		public async Task<Result> Register(DTOUserRegister userRegister)
		{
			Result result = new Result();
			try
			{
				UserAgent agent = new UserAgent();
				result.Success = await agent.Register(userRegister);
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}

			return result;
		}


        [HttpPost]
        [Route("login")]
        public async Task<Result> Login(DTOUserLogin userLogin)
        {
            Result result = new Result();
            try
            {
                UserAgent agent = new UserAgent();
                User user = await agent.Login(userLogin);
                result.Success = true;
                result.Data = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Phone,
                    user.RoleId
                };
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
