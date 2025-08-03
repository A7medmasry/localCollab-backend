using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Exception thrown when a forbidden action is attempted.
	/// </summary>
	[Serializable]
	public class QlForbiddenException : QlException
	{
		public QlForbiddenException() { }

		public QlForbiddenException(string message)
			: base(message)
		{
		}

		public QlForbiddenException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QlForbiddenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
