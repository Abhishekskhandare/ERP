using ERP.EFModels;

namespace ERP.Service
{
	public interface IUserService
	{
		public Task<User?> GetUserByEmail(string email);
		public Task<bool> CreateUser(User user, UserDetail detail);


	}
}
