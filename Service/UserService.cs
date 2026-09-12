
using ERP.Data;
using ERP.EFModels;
using Microsoft.EntityFrameworkCore;

namespace ERP.Service
{
	public class UserService
	{
		ERPContext context = new ERPContext();

		public async Task<User?> GetUserByEmail(string email)
		{
			User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
			return user;
		}

		public async Task<bool> CreateUser(User user, UserDetail detail)
		{
			context.Add(user);
			context.SaveChanges();
			User? newUser = await GetUserByEmail(user.Email);
			if(newUser != null)
			{
				detail.UserId = newUser.Id;
				context.Add(detail);
				context.SaveChanges();
			}
			return true;
		}
	}
}
