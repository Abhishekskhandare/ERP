using ERP.DTO;

namespace ERP.Agent
{
	public interface IUserAgent
	{
		public Task<bool> Register(DTOUserRegister userRegister);

	}
}
