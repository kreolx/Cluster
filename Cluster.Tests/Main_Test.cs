using Cluster.Mocks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cluster.Tests
{
    public class Main_Test
    {
        [Fact]
        public async void IsSplittedAsync_should_return_false_when_cluster_is_not_splited()
        {
            var client = new ClusterClientMock(new Dictionary<int, bool>
            {
                [1] = true,
                [2] = true,
                [3] = true,
                [4] = true,
                [5] = true,
                [6] = false,
            },
            new (int from, int to)[]
            {
                (1,2),
                (2,3),
                (1,3),
                (3,4),
                (4,5),
                (3,5),
                (5,6)
            });
            var result = await ClusterCheckManager.CheckClusterStateAsync(client, default);
            Assert.False(result);
        }

        [Fact]
        public async void IsSplittedAsync_should_return_True_when_cluster_is_not_splited()
        {
            var client = new ClusterClientMock(new Dictionary<int, bool>
            {
                [1] = true,
                [2] = true,
                [3] = true,
                [4] = true,
                [5] = true,
                [6] = false,
            },
            new (int from, int to)[]
            {
                (1,2),
                (2,3),
                (1,3),
                (4,5),
                (5,6)
            });
            var result = await ClusterCheckManager.CheckClusterStateAsync(client, default);
            Assert.True(result);
        }
    }
}
