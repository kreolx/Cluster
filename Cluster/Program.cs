using System.Collections.Generic;
using Cluster.Mocks;

namespace Cluster
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new ClusterClientMock(new Dictionary<int, bool>
			{
				[1] = true,
				[2] = true,
				[3] = true,
				[4] = true,
			},
			new (int from, int to)[] 
			{
				(1,2),
				(2,3),
				(3,4)
			});
		}
	}
}
