namespace TiktokLocalAPI.Core.OutDTO.Auth
{
	/// <summary>
	/// Represents the data returned when an authentication token is successfully renewed.
	/// </summary>
	public class RenewOutDto
	{
		/// <summary>
		/// Gets or sets the newly issued authentication token (JWT).
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Gets or sets the session token used for subsequent renewals.
		/// </summary>
		public string SessionToken { get; set; }
	}
}
