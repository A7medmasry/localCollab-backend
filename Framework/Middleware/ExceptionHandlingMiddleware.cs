using Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Framework.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				// Process the HTTP request
				await _next(context);
			}
			catch (Exception ex)
			{
				// Handle any exceptions thrown downstream
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			// Determine the response status code based on exception type
			var statusCode = exception switch
			{
				QlBadRequestException _ => StatusCodes.Status400BadRequest,
				QlInvalidOperationException _ => StatusCodes.Status400BadRequest,
				QlUnauthorizedException _ => StatusCodes.Status401Unauthorized,
				QlForbiddenException _ => StatusCodes.Status403Forbidden,
				QlNotFoundException _ => StatusCodes.Status404NotFound,
				QlException _ => StatusCodes.Status500InternalServerError,
				_ => StatusCodes.Status500InternalServerError
			};


			if (statusCode == StatusCodes.Status500InternalServerError)
			{
				// TODO: Log the exception details to a file or external service
			}


			var errorResponse = new Dictionary<string, object>();
			errorResponse.Add("error_message", exception.Message);

			// Prepare a JSON response with error details
			var result = JsonSerializer.Serialize(errorResponse);

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			await context.Response.WriteAsync(result);
		}
	}
}
