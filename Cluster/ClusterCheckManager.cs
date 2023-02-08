using Cluster.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace Cluster
{
    public static class ClusterCheckManager
    {
        public static async Task<bool> CheckClusterStateAsync(IClusterClient client, CancellationToken cancellationToken)
        {
            var nodes = await client.GetAllNodesAsync(cancellationToken);
            var splitedCount = 0;
            foreach (var node in nodes)
            {
                var listrelated = new List<int>();

                var relatedNodes = await GetRelatedNodesIfAvailableAsync(client, node.Id, cancellationToken);
                if (relatedNodes != null)
                {
                    listrelated.AddRange(relatedNodes);
                    foreach (var relatedNode in relatedNodes)
                    {
                        var innerRelatedNodes = await GetRelatedNodesIfAvailableAsync(client, relatedNode, cancellationToken);
                        if (innerRelatedNodes != null)
                        {
                            listrelated.AddRange(innerRelatedNodes);
                        }
                    }
                }

                splitedCount += Check(nodes.Select(x => x.Id), listrelated);
            }

            return splitedCount > 1;
        }


        private static int Check(IEnumerable<int> nodes, IEnumerable<int> relatedNodes) 
        {
            if (relatedNodes.Any())
            {
                var listrelated = relatedNodes.Distinct().ToList();
                var notRelatedNodes = nodes.Except(listrelated).ToArray();
                return notRelatedNodes.Length > 1 ? 1 : 0;
            }
            else
            {
                return 1;
            }
        }

        private static async Task<IEnumerable<int>?> GetRelatedNodesIfAvailableAsync(IClusterClient client, int id, CancellationToken cancellationToken)
        {
            var relatedState = await client.GetNodeStateAsync(id, cancellationToken);
            if (relatedState.IsAvailable)
            {
                var internalRelatedNodes = await client.GetRelatedNodesAsync(id, cancellationToken);
                return internalRelatedNodes.Select(x => x.Id);
            }
            return null;
        }
    }
}
