using ERP.DTO;
using ERP.EFModels;
using ERP.Service;

namespace ERP.Agent
{
	public class UserAgent
	{
		public bool Register(DTOUserRegister userRegister)
		{
			try
			{
				UserService service  = new UserService();
				User user = new User
				{
					FirstName = userRegister.FirstName,
					LastName = userRegister.LastName,
					Email = userRegister.Email,
					Phone = userRegister.Phone,
					Password = userRegister.Password,
					IsActive = true,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};

				UserDetail detail = new UserDetail();
				detail.AddressLine1 = userRegister.AddressLine1;
				detail.AddressLine2 = userRegister.AddressLine2;
				detail.City = userRegister.City;
				detail.State = userRegister.State;
				detail.PostalCode = userRegister.PostalCode;
				detail.Country = userRegister.Country;
				detail.CreatedAt = DateTime.Now;
				detail.UpdatedAt = DateTime.Now;

				if(service.IsUserExist(user.Email))
				{
					//if(IsUserActive(user.Email)))
					//{
					//	// user already exist
					//}
					//else
					//{
					//	// user is inactive plz connect with Admin to activate yourself.
					//}
				}

			}
			catch (Exception ex)
			{
				throw;
			}


			return true;
		
		}
	}
}
