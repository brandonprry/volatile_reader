using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0D : INode
	{
		private _x0D (){}
		
		public _x0D (BinaryReader log, long chunkOffset, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			short index = log.ReadInt16();
			byte type = log.ReadByte();
			this.LogRoot = root;
			this.SelfEnclosed = true;
	}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long Position { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { throw new Exception(); }
		
		public long Length { 
			get
			{
				throw new Exception();
			}
			set {}
		}
		#endregion
	}
}

