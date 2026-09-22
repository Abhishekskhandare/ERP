using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERP.Middleware
{
	public class LogMiddleware
	{

		private readonly RequestDelegate _next;

		public LogMiddleware(RequestDelegate next)
		{
			_next = next;
		}



		public async Task InvokeAsync(HttpContext context)
		{

			// Before the next middleware (Pre-processing)


			#region Before

			var stopwatch = Stopwatch.StartNew();
			var traceIdentifier = context.TraceIdentifier;
			context.Request.EnableBuffering();
			var requestBody = await ReadBodyAsync(context.Request);
			DateTime requestTime = DateTime.UtcNow;
			var logObj = new
			{
				Type = "REQUEST",
				httpVerb = context.Request.Method,
				API = context.Request.Path.Value,
				RequestBody = (context.Request.Method == "GET") ? context.Request.QueryString.ToString() : requestBody,
				Time = requestTime
			};

			Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(logObj));

			// Capture response body
			var originalResponseBody = context.Response.Body;
			await using var responseBodyMemoryStream = new MemoryStream();
			context.Response.Body = responseBodyMemoryStream;
			#endregion




			await _next(context); // Call the next middleware


			#region After
			stopwatch.Stop();
			context.Response.Body = responseBodyMemoryStream;
			responseBodyMemoryStream.Position = 0;
			var responseBody = await new StreamReader(responseBodyMemoryStream).ReadToEndAsync();
			var logData = new
			{
				LogType = "RESPONSE",
				httpVerb = context.Request.Method,
				API = context.Request.Path.Value,
				RequestBody = (context.Request.Method == "GET") ? context.Request.QueryString.ToString() : requestBody,
				StatusCode = context.Response.StatusCode,
				ResponseBody = responseBody,
				Timestamp = DateTime.UtcNow,
				ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
			};
			string jsonLog = JsonSerializer.Serialize(logData);
			Console.WriteLine(jsonLog);
			#endregion



		}


		private async Task<string> ReadBodyAsync(HttpRequest request)
		{
			request.Body.Position = 0;
			using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
			var body = await reader.ReadToEndAsync();
			request.Body.Position = 0; // Reset position for the controllers
			return body;
		}
	}
}
