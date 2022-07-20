using System;

namespace Cluster.Exceptions
{
	/// <summary>
	/// Throws if cluster is unavailable.
	/// </summary>
	public class ClusterUnavailableException : Exception
	{
		public ClusterUnavailableException()
		{
		}

		public ClusterUnavailableException(string message) : base(message)
		{
		}

		public ClusterUnavailableException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
