using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x04 : INode
	{
		private _x04 (){}

		public _x04 (BinaryReader reader, long chunkOffset, LogRoot root, INode parent)
		{
			this.Position = reader.BaseStream.Position;
			this.LogRoot = root;
			root.TagState = 0;
			this.SelfEnclosed = true;
			this.Parent = parent;
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long Position { get; set; }
		public string String { get; set; }
		public int SubstitutionArray { get; set; }
		public bool SelfEnclosed { get; set; }
		
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML()
		{	
			string str = string.Empty;

			if (this.LogRoot.CurrentOpenTags.Count != 0)
				str = "</" + this.LogRoot.CurrentOpenTags[this.LogRoot.CurrentOpenTags.Count-1] + ">";
			else
				str = "</" + this.Parent.String + ">";

			this.LogRoot.CurrentOpenTags.RemoveAt(this.LogRoot.CurrentOpenTags.Count-1);
			return str;
		}
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

