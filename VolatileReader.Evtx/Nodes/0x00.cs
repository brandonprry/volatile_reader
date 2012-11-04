using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x00 : INode
	{
		private _x00 (){}
		
		public _x00 (BinaryReader reader, long chunkOffset)
		{
		}

		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

