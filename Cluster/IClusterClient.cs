using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cluster.Dto;
using Cluster.Exceptions;

namespace Cluster
{
	public interface IClusterClient
	{
		/// <summary>
		/// Gets list of all nodes in cluster.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Collection of nodes.</returns>
		/// <exception cref="ClusterUnavailableException">Throws if cluster is unavailable.</exception>
		Task<IReadOnlyCollection<NodeDto>> GetAllNodesAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Gets state of specified node.
		/// </summary>
		/// <param name="nodeId">Id of node.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>State of node.</returns>
		/// <exception cref="ClusterUnavailableException">Throws if cluster is unavailable.</exception>
		/// <exception cref="NodeNotExistsException">Throws if specified node does not exists.</exception>
		Task<NodeStateDto> GetNodeStateAsync(int nodeId, CancellationToken cancellationToken);

		/// <summary>
		/// Gets list of nodes related to specified node.
		/// </summary>
		/// <param name="nodeId">Id of node.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Collection of related nodes.</returns>
		/// <exception cref="ClusterUnavailableException">Throws if cluster is unavailable.</exception>
		/// <exception cref="NodeUnavailableException">Throws if specified node is unavailable.</exception>
		/// <exception cref="NodeNotExistsException">Throws if specified node does not exists.</exception>
		Task<IReadOnlyCollection<NodeDto>> GetRelatedNodesAsync(int nodeId, CancellationToken cancellationToken);
	}
}
