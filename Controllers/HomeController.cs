using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class HomeController : ControllerBase
	{
		public HomeController() { }

		[HttpGet]
		[Route("getplpproducts")]
		public IActionResult GetPlpProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			// Mocked product list for PLP
			var products = new List<object>
		{
			new
			{
				Id = 101,
				Name = "Wireless Noise-Canceling Headphones",
				Price = 199.99,
				Rating = 4.7,
				ImageUrl = "https://example.com/images/headphones.jpg",
				InStock = true
			},
			new
			{
				Id = 102,
				Name = "Ergonomic Mechanical Keyboard",
				Price = 129.50,
				Rating = 4.5,
				ImageUrl = "https://example.com/images/keyboard.jpg",
				InStock = true
			},
			new
			{
				Id = 103,
				Name = "Ultra-Wide Gaming Monitor",
				Price = 450.00,
				Rating = 4.8,
				ImageUrl = "https://example.com/images/monitor.jpg",
				InStock = false
			}
		};

			// Paginate in-memory
			var paginatedProducts = products
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			// Return structured JSON payload
			var response = new
			{
				TotalCount = products.Count,
				CurrentPage = page,
				PageSize = pageSize,
				Data = paginatedProducts
			};

			return Ok(response);
		}
	}
}
