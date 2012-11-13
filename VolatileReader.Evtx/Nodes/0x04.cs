using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x04 : INode
	{
		private _x04 (){}
		
		public _x04 (BinaryReader reader, long chunkOffset, ref LogRoot root)
		{
			this.Position = reader.BaseStream.Position;
			this.LogRoot = root;
			root.TagState = 0;
			this.SelfEnclosed = true;
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long Position { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { return string.Empty; }
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

