using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0D : INode
	{
		private _x0D (){}
		
		public _x0D (BinaryReader log, long chunkOffset)
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

