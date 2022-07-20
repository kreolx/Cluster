using System;

namespace Cluster.Exceptions
{
	/// <summary>
	/// Throws when trying to access an unavailable node.
	/// </summary>
	public class NodeUnavailableException : Exception
	{
		public NodeUnavailableException()
		{
		}

		public NodeUnavailableException(string message) : base(message)
		{
		}

		public NodeUnavailableException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
