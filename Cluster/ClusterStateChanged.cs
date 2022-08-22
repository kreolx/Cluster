using System;

namespace Cluster
{
	public class ClusterStateChanged : EventArgs
	{
		public ClusterStateChanged(bool isSplitted)
		{
			IsSplitted = isSplitted;
		}

		public bool IsSplitted { get; }
	}
}
