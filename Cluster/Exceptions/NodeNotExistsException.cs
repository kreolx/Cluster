using System;

namespace Cluster.Exceptions
{
	/// <summary>
	/// Throws if node does not exists.
	/// </summary>
	public class NodeNotExistsException : Exception
	{
		public NodeNotExistsException()
		{
		}

		public NodeNotExistsException(string message) : base(message)
		{
		}

		public NodeNotExistsException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
