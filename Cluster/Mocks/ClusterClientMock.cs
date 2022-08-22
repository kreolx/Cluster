using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cluster.Dto;
using Cluster.Exceptions;

namespace Cluster.Mocks
{
	public class ClusterClientMock : IClusterClient
	{
		private readonly Dictionary<int, bool> _states;
		private readonly (int from, int to)[] _edges;

		public ClusterClientMock(
			Dictionary<int, bool> states,
			(int from, int to)[] edges)
		{
			_states = states;
			_edges = edges;

			foreach (var edge in _edges)
			{
				CheckNodeIsExists(edge.from);
				CheckNodeIsExists(edge.to);
			}

			IsAvailable = true;
		}

		public bool IsAvailable { get; set; }

		public Task<IReadOnlyCollection<NodeDto>> GetAllNodesAsync(CancellationToken cancellationToken)
		{
			CheckClusterIsAvailable();

			var result = _states.Keys
				.Select(id => new NodeDto { Id = id })
				.OrderBy(n => n.Id)
				.ToArray();

			return Task.FromResult<IReadOnlyCollection<NodeDto>>(result);
		}

		public Task<NodeStateDto> GetNodeStateAsync(int nodeId, CancellationToken cancellationToken)
		{
			CheckClusterIsAvailable();
			CheckNodeIsExists(nodeId);

			var result = new NodeStateDto
			{
				IsAvailable = _states[nodeId]
			};

			return Task.FromResult(result);
		}

		public Task<IReadOnlyCollection<NodeDto>> GetRelatedNodesAsync(int nodeId, CancellationToken cancellationToken)
		{
			CheckClusterIsAvailable();
			CheckNodeIsExists(nodeId);

			if (!_states[nodeId])
			{
				throw new NodeUnavailableException($"Node {nodeId} is unavailable");
			}

			var outbound = _edges.Where(e => e.from == nodeId).Select(e => e.to);
			var inbound = _edges.Where(e => e.to == nodeId).Select(e => e.from);
			var result = outbound
				.Union(inbound)
				.Where(id => id != nodeId)
				.Select(id => new NodeDto { Id = id })
				.OrderBy(n => n.Id)
				.ToArray();

			return Task.FromResult<IReadOnlyCollection<NodeDto>>(result);
		}

		public void DisableNode(int nodeId)
		{
			CheckNodeIsExists(nodeId);
			_states[nodeId] = false;
		}

		private void CheckClusterIsAvailable()
		{
			if (!IsAvailable)
			{
				throw new ClusterUnavailableException();
			}
		}

		private void CheckNodeIsExists(int nodeId)
		{
			if (!_states.ContainsKey(nodeId))
			{
				throw new NodeNotExistsException($"Node {nodeId} does not exists");
			}
		}
	}
}
