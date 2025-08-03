namespace TiktokLocalAPI.Helpers
{
	/// <summary>
	/// Provides helper methods for constructing MySQL database connection strings using environment variables.
	/// </summary>
	public static class MySqlHelper
	{
		/// <summary>
		/// Retrieves the value of a required environment variable.
		/// </summary>
		/// <param name="key">The name of the environment variable.</param>
		/// <returns>The value of the environment variable.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the environment variable is not set or empty.</exception>
		private static string GetRequiredEnvironmentVariable(string key)
		{
			var value = Environment.GetEnvironmentVariable(key);
			if (string.IsNullOrEmpty(value))
			{
				throw new InvalidOperationException($"Required environment variable '{key}' is not set.");
			}
			return value;
		}

		/// <summary>
		/// Constructs the MySQL database connection string using environment variables.
		/// </summary>
		/// <returns>A formatted MySQL connection string.</returns>
		/// <remarks>
		/// The following environment variables must be set:
		/// DATABASE_URL, DATABASE_PORT, DATABASE_TABLE_NAME, DATABASE_USER, DATABASE_PASSWORD.
		/// </remarks>
		public static string GetDbConnectionString()
		{
			return string.Format(
				"Server={0};Port={1};Database={2};User={3};Password={4};Connect Timeout=60;",
				GetRequiredEnvironmentVariable("DATABASE_URL"),
				GetRequiredEnvironmentVariable("DATABASE_PORT"),
				GetRequiredEnvironmentVariable("DATABASE_TABLE_NAME"),
				GetRequiredEnvironmentVariable("DATABASE_USER"),
				GetRequiredEnvironmentVariable("DATABASE_PASSWORD")
			);
		}
	}
}
