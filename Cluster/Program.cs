using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cluster.Mocks;

namespace Cluster
{
	class Program
	{
		static void Main(string[] args)
		{
			//   1-2
			//    \|
			//     3
			//    /|
			//   4-5-(6)
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
		}

		private static Task<bool> IsSplittedAsync(IClusterClient client, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
