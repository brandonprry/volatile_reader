using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x02 : INode
	{
		private _x02 (){}
		
		public _x02 (BinaryReader reader, long chunkOffset, ref LogRoot root)
		{
			this.Position = reader.BaseStream.Position;
			this.LogRoot = root;
			root.TagState = 0;
		}
		
		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { return ">"; }
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

