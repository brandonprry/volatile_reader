using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0C : INode
	{
		private _x0C (){}
		
		public _x0C (BinaryReader log, long chunkOffset, LogRoot root, INode parent)
		{
			this.Position = log.BaseStream.Position;
			this.LogRoot = root;
			this.ChunkOffset = chunkOffset;
			this.Length = 10;
			this.SelfEnclosed = true;
			log.BaseStream.Position += 1;
			int templateID = log.ReadInt32();
			int ptr = log.ReadInt32();

			if (this.ChunkOffset + ptr < log.BaseStream.Position) {
				this.Template = root.ParentLog.Templates [ptr.ToString ()];
			}
			else {
				this.Template = new Template (log, chunkOffset, root) { Parent = this };
				root.ParentLog.Templates [ptr.ToString ()] = this.Template;
			}	

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

