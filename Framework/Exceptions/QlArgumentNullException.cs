using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Exception thrown when a bad request is encountered.
	/// </summary>
	[Serializable]
	public class QlArgumentNullException : QlException
	{
		public QlArgumentNullException() { }

		public QlArgumentNullException(string message)
			: base(message)
		{
		}

		public QlArgumentNullException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QlArgumentNullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
