using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Exception thrown when a requested resource is not found.
	/// </summary>
	[Serializable]
	public class QlNotFoundException : QlException
	{
		public QlNotFoundException() { }

		public QlNotFoundException(string message)
			: base(message)
		{
		}

		public QlNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QlNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
