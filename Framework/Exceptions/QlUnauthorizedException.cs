using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Exception thrown when an unauthorized action is attempted.
	/// </summary>
	[Serializable]
	public class QlUnauthorizedException : QlException
	{
		public QlUnauthorizedException() { }

		public QlUnauthorizedException(string message)
			: base(message)
		{
		}

		public QlUnauthorizedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QlUnauthorizedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
