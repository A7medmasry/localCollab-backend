namespace TiktokLocalAPI.Core.OutDTO.Auth
{
	/// <summary>
	/// Represents the data returned after successful authentication,
	/// including the access (auth) token and refresh token.
	/// </summary>
	public class AuthOutDto
	{
		/// <summary>
		/// Gets or sets the authentication token (JWT) used for authorized access.
		/// </summary>
		public string AuthToken { get; set; }

		/// <summary>
		/// Gets or sets the refresh token used to obtain a new authentication token after expiration.
		/// </summary>
		public string RefreshToken { get; set; }
	}
}
