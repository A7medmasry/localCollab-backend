using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Exception thrown when a bad request is encountered.
	/// </summary>
	[Serializable]
	public class QlBadRequestException : QlException
	{
		public QlBadRequestException() { }

		public QlBadRequestException(string message)
			: base(message)
		{
		}

		public QlBadRequestException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QlBadRequestException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
