using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Root : INode
	{
		private Root (){}
		
		public Root (BinaryReader reader)
		{
			
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

