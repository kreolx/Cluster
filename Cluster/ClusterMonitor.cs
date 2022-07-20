namespace Cluster
{
	public class ClusterMonitor
	{
		private readonly IClusterClient _client;

		public ClusterMonitor(IClusterClient client)
		{
			_client = client;
		}
	}
}
