
using ERP.Data;
using ERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Service
{
	public class UserService
	{
		private readonly ERPDbContext _context;

		// Constructor for DI - preferred
		public UserService(ERPDbContext context)
		{
			_context = context;
		}

		// Parameterless constructor fallback (not recommended)
		public UserService()
		{
			_context = new ERPDbContext();
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
			return user;
		}

		public async Task<bool> CreateUser(User user, UserDetail detail)
		{
			_context.Add(user);
			await _context.SaveChangesAsync();
			User? newUser = await GetUserByEmail(user.Email);
			if (newUser != null)
			{
				detail.UserId = newUser.Id;
				_context.Add(detail);
				await _context.SaveChangesAsync();
			}
			return true;
		}
	}
}
