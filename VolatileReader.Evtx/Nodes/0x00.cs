using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x00 : INode
	{
		private _x00 (){}
		
		public _x00 (BinaryReader reader, long chunkOffset, LogRoot root, INode parent)
		{
			this.LogRoot = root;
			this.Position = reader.BaseStream.Position;
			this.SelfEnclosed = true;
		}

		#region INode implementation
		public long Position { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public int SubstitutionArray { get; set; }
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		public string String { get; set; }
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

