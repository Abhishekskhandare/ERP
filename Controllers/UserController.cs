using ERP.Agent;
using ERP.DTO;
using ERP.EFModels;
using ERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ERP.Helper;

namespace ERP.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserAgent _agent;

		public UserController(IUserAgent agent)
		{
			_agent = agent;
		}

		[HttpPost]
		[Route("register")]
		public async Task<Result> Register(DTOUserRegister userRegister)
		{
			Result result = new Result();
			try
			{
				result.Success = await _agent.Register(userRegister);
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}

			return result;
		}


		[HttpPost]
		[Route("login")]
		public async Task<Result> Login(DTOUserLogin userLogin)
		{
			Result result = new Result();
			try
			{

				User user = await _agent.Login(userLogin);

				string token = await GenerateToken(user);
				result.Success = true;
				result.Data = token;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
			}

			return result;
		}



		private async Task<string> GenerateToken(User user)
		{
			IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user?.FirstName),
				new Claim(ClaimTypes.Email, user?.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user?.Role?.Name)
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
			var tokenDescriptor = new JwtSecurityToken(
			  issuer: configuration.GetSection("Jwt")["Issuer"],
			  audience: configuration.GetSection("Jwt")["Audience"],
			  claims: claims,
			  expires: DateTime.Now.AddDays(1),
			  signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
		}
		// change password   -  rutuja



		// login   -- aniket
	}
}









