using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x02 : INode
	{
		private _x02 (){}
		
		public _x02 (BinaryReader reader, long chunkOffset)
		{
			
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

