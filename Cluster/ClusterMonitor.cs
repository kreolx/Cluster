using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster
{
	public class ClusterMonitor
	{
		private readonly IClusterClient _client;
		private bool _state;

		public ClusterMonitor(IClusterClient client)
		{
			_client = client;
			_state = false;
		}

		public event EventHandler<ClusterStateChanged> ClusterStateChanged;

		public async Task CheckClusterStateAsync(CancellationToken cancellationToken)
		{
			var currentState = await ClusterCheckManager.CheckClusterStateAsync(_client, cancellationToken);
			if (currentState != _state)
			{
				ClusterStateChanged.Invoke(currentState, default);
			}
			_state = currentState;
		}
	}
}
