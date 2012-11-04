using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0C : INode
	{
		private _x0C (){}
		
		public _x0C (BinaryReader log, long chunkOffset)
		{
			this.ChunkOffset = chunkOffset;
			log.BaseStream.Position += 1;
			int templateID = log.ReadInt32();
			int ptr = log.ReadInt32();
			
			log.BaseStream.Position = this.ChunkOffset + ptr;
			
			int nextTemplate = log.ReadInt32();
			int templateID2 = log.ReadInt32();
			
			log.BaseStream.Position -= 4;
			
			byte[] g = log.ReadBytes(16);
			Guid templateGuid = new Guid(g);
			int templateLength = log.ReadInt32();
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

