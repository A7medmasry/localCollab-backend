using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Exception thrown when a requested resource is not found.
	/// </summary>
	[Serializable]
	public class QlInvalidOperationException : QlException
	{
		public QlInvalidOperationException() { }
		public QlInvalidOperationException(string message)
			: base(message)
		{
		}
		public QlInvalidOperationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
		protected QlInvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
