
using ERP.Data;
using ERP.EFModels;

namespace ERP.Service
{
	public class UserService
	{
		public bool IsUserExist(string email)
		{
			ERPContext context = new ERPContext();
			User? user =  context.Users.FirstOrDefault(u => u.Email == email);
			if (user == null)
			{
				return false;
			}
			else
			{
				if (user.IsActive == true) { return true; }
				else return false;
			}

		}
	}
}
