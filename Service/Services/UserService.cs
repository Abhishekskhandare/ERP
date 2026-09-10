using ERP.Data;
using ERP.EFModels;
using Microsoft.EntityFrameworkCore;

namespace ERP.Service { 
    public class UserService : IUserService
	{
        ERPContext context = new ERPContext();

        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) { user.Role = await context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId); }
            return user;
        }

        public async Task<bool> CreateUser(User user, UserDetail detail)
        {
            context.Add(user);
            context.SaveChanges();
            User? newUser = await GetUserByEmail(user.Email);
            if (newUser != null)
            {
                detail.UserId = newUser.Id;
                context.Add(detail);
                context.SaveChanges();
            }
            return true;
        }
    }
}
