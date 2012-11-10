using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0F : INode
	{
		private _x0F (){}
		
		public _x0F (BinaryReader log, long chunkOffset, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			this.LogRoot = root;
			char oth1 = log.ReadChar();
			short oth2 = log.ReadInt16();
			
			if (oth1 != 1 && oth2 != 1)
				throw new Exception("Bad 0x0f node -- oth1: " + oth1 + " :: oth2: " + oth2);
		}
		
		public long Position { get; set; }
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { return string.Empty; }
		public long Length 
		{
			get
			{
				return 4;
			}
			
			set {}
		}
		#endregion
	}
}

