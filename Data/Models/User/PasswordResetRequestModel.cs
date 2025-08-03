namespace TiktokLocalAPI.Data.Models.User
{
	/// <summary>
	/// Represents a password reset request entity.
	/// </summary>
	public class PasswordResetRequestModel
	{
		/// <summary>
		/// Gets or sets the unique identifier for the password reset request.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier associated with the request.
		/// </summary>
		public Guid Guid { get; set; }

		/// <summary>
		/// Gets or sets the token used for the password reset.
		/// </summary>
		public required string Token { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the reset request expires.
		/// </summary>
		public DateTime ExpiryDate { get; set; }
	}
}
