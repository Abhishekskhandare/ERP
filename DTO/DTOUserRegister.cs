namespace ERP.DTO
{
	public class DTOUserRegister
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string Email { get; set; } = null!;
		public string? Phone { get; set; }
		public string Password { get; set; } = null!;
		public string AddressLine1 { get; set; } = null!;
		public string? AddressLine2 { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? PostalCode { get; set; }
		public string Country { get; set; } = null!;
	}
}
