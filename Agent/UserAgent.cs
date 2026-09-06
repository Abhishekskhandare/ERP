using ERP.DTO;
using ERP.Models;
using ERP.Service;
using Microsoft.AspNetCore.Components.Forms;

namespace ERP.Agent
{
	public class UserAgent
	{
		private readonly ERP.Data.ERPDbContext? _context;

		public UserAgent()
		{
		}

		public UserAgent(ERP.Data.ERPDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Register(DTOUserRegister userRegister)
		{
			try
			{

				bool isValidate = await IsRegisterValidate(userRegister);
				if(!isValidate) throw new Exception("Validation failed.");

				UserService service;
				if (_context != null)
					service = new UserService(_context);
				else
					service = new UserService();
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


				User? existingUser = await service.GetUserByEmail(user.Email);
				if (existingUser != null)
				{
					if(existingUser.IsActive == true)
					{
						throw new Exception("User already exists.");
					}
					else
					{
						throw new Exception("User is inactive please connect with Admin to activate yourself.");
					}
				}
				return await service.CreateUser(user, detail);

			}
			catch (Exception ex)
			{
				throw;
			}
		}
		#region Private

		// validation by saurabh
		private async Task<bool> IsRegisterValidate(DTOUserRegister userRegister)
		{
			return true;
			//throw new NotImplementedException();
		}

		#endregion
	}
}
