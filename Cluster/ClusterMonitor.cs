using System;

namespace Cluster
{
	public class ClusterMonitor
	{
		private readonly IClusterClient _client;

		public ClusterMonitor(IClusterClient client)
		{
			_client = client;
		}

		public event EventHandler<ClusterStateChanged> ClusterIsSplitted;
	}
}
