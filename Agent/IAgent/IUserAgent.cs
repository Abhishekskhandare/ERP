using ERP.DTO;
using ERP.EFModels;

namespace ERP.Agent
{
	public interface IUserAgent
	{
		public Task<bool> Register(DTOUserRegister userRegister);
		public Task<User> Login(DTOUserLogin userLogin);


	}
}
