namespace TiktokLocalAPI.Core.DTO.Session
{
	/// <summary>
	/// Represents a data transfer object (DTO) used to refresh a user session via a refresh token.
	/// </summary>
	public class RefreshSessionDto
	{
		/// <summary>
		/// Gets or sets the refresh token associated with the current session.
		/// This token is used to request a new authentication token without re-authenticating.
		/// </summary>
		public string RefreshToken { get; set; }
	}
}
