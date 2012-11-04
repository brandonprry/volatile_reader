using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0E : INode
	{
		private _x0E (){}
		
		public _x0E (BinaryReader log, long chunkOffset)
		{
			short index = log.ReadInt16();
			byte type = log.ReadByte();
			
			Console.WriteLine(type);
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

