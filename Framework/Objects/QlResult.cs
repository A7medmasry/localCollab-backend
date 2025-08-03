using Microsoft.AspNetCore.Mvc;

namespace Framework.Objects
{
	public static class QlResult
	{
		/// <summary>
		/// Creates an OkObjectResult with a success message.
		/// </summary>
		/// <param name="successMessage">The success message.</param>
		/// <returns>An ActionResult containing the success message.</returns>
		public static ActionResult Success(string successMessage)
		{
			var result = new
			{
				success_message = successMessage
			};

			return new OkObjectResult(result);
		}

		/// <summary>
		/// Creates an OkObjectResult with a success message and additional data.
		/// </summary>
		/// <param name="successMessage">The success message.</param>
		/// <param name="data">The additional data.</param>
		/// <returns>An ActionResult containing the success message and data.</returns>
		public static ActionResult Success(string successMessage, object data)
		{
			var result = new
			{
				success_message = successMessage,
				data = data
			};

			return new OkObjectResult(result);
		}

		public static IActionResult Unauthorized(string errorMessage)
		{
			var result = new
			{
				error_message = errorMessage
			};

			return new UnauthorizedObjectResult(result);
		}

		public static IActionResult BadRequest(string errorMessage)
		{
			var result = new
			{
				error_message = errorMessage
			};

			return new BadRequestObjectResult(result);
		}
	}
}
