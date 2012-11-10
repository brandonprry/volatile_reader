using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x00 : INode
	{
		private _x00 (){}
		
		public _x00 (BinaryReader reader, long chunkOffset, ref LogRoot root)
		{
			this.LogRoot = root;
			this.Position = reader.BaseStream.Position;
		}

		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		public string ToXML() { return string.Empty; }
		public LogRoot LogRoot { get; set; }
		public long Length 
		{
			get
			{
				return 1;
			}
			
			set {}
		}
		#endregion
	}
}

