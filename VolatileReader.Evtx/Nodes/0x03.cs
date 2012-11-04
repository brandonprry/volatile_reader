using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x03 : INode
	{
		private _x03 (){}
		
		public _x03 (BinaryReader reader, long chunkOffset)
		{
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

