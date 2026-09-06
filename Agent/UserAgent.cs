using ERP.DTO;
using ERP.EFModels;
using ERP.Service;

namespace ERP.Agent
{
	public class UserAgent
	{
		public async Task<bool> Register(DTOUserRegister userRegister)
		{
			try

			{

				bool isValidate = await IsRegisterValidate(userRegister);
				if(!isValidate) throw new Exception("Validation failed.");

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



        // login -- aniket
        public async Task<User> Login(DTOUserLogin userLogin)
        {
            try
            {
                bool isValidate = await IsLoginValidate(userLogin);
                if (!isValidate) throw new Exception("Validation failed.");

                UserService service = new UserService();
                User? existingUser = await service.GetUserByEmail(userLogin.Email);

                if (existingUser == null || existingUser.Password != userLogin.Password)
                {
                    throw new Exception("Invalid email or password.");
                }

                if (existingUser.IsActive != true)
                {
                    throw new Exception("User is inactive please connect with Admin to activate yourself.");
                }

                return existingUser;
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


        //private async Task<bool> IsLoginValidate(DTOUserLogin userLogin)
        //{
        //    if (string.IsNullOrWhiteSpace(userLogin.Email) || string.IsNullOrWhiteSpace(userLogin.Password))
        //        return false;

        //    return true;
        //}


        private async Task<bool> IsLoginValidate(DTOUserLogin userLogin)
		{ if (string.IsNullOrWhiteSpace(userLogin.Email)) { throw new Exception("Email is required."); }
			if (string.IsNullOrWhiteSpace(userLogin.Password)) { throw new Exception("Password is required."); } return true; }
        #endregion
    }
}
