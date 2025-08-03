namespace Framework.Enums
{
	public enum ActivationStatus
	{
		/// <summary>
		/// The user account is newly created and pending confirmation.
		/// </summary>
		New,

		/// <summary>
		/// The user account has been confirmed and is active.
		/// </summary>
		Confirmed,

		/// <summary>
		/// The user account has been blocked by an administrator.
		/// </summary>
		Blocked,

		/// <summary>
		/// The user account has been deactivated and is no longer active.
		/// </summary>
		Deactivated
	}
}
