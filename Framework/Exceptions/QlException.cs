using System.Runtime.Serialization;

namespace Framework.Exceptions
{
	/// <summary>
	/// Base exception for the Ql project.
	/// </summary>
	[Serializable]
	public class QlException : Exception
	{
		public QlException() { }

		public QlException(string message)
			: base(message)
		{
		}

		public QlException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected QlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
