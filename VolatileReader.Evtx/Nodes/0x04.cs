using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x04 : INode
	{
		private _x04 (){}
		
		public _x04 (BinaryReader reader, long chunkOffset)
		{
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

