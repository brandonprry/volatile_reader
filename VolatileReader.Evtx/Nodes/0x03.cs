using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x03 : INode
	{
		private _x03 (){}
		
		public _x03 (BinaryReader reader, long chunkOffset, ref LogRoot root)
		{
			this.Position = reader.BaseStream.Position;
			this.LogRoot = root;
			root.ElementType = 0;
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long Position { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { return string.Empty; }
		public long ChunkOffset { get; set; }
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

