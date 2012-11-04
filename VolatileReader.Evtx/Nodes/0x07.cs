using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x07 : INode
	{
		private _x07 (){}
		
		public _x07 (BinaryReader log, long chunkOffset)
		{
			short length = log.ReadInt16();	
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

