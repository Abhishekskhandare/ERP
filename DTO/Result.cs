namespace ERP.DTO
{
	public class Result
	{
		public bool Success { get; set; }
		public string? Message { get; set; } = null;
		public object? Data { get; set; }
	}
}
