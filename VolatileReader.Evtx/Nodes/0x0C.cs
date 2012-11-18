using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0C : INode
	{
		private _x0C (){}
		
		public _x0C (BinaryReader log, long chunkOffset, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			this.LogRoot = root;
			this.ChunkOffset = chunkOffset;
			this.Length = 1 + 4 + 4 + 4 + 4 + 16 + 4;
			this.SelfEnclosed = true;
			this.Template = new Template(log, chunkOffset, ref root) { Parent = this };
			this.Length += this.Template.Length;
		}
		
		public Template Template { get; set; }
		
		#region INode implementation
		public INode Parent { get; set; }
		public int SubstitutionArray { get; set; }
		public long Position { get; set; }
		public long ChunkOffset { get; set; }
		public string String { get; set; }
		public bool SelfEnclosed { get; set; }
		
		public LogRoot LogRoot { get; set; }
		public string ToXML() { return this.Template.ToXML(); }
		public long Length { get; set; }
		#endregion
	}
}

