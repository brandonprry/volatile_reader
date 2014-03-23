using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x03 : INode
	{
		private _x03 (){}
		
		public _x03 (BinaryReader reader, long chunkOffset, LogRoot root, INode parent)
		{
			this.Position = reader.BaseStream.Position;
			this.LogRoot = root;
			this.SelfEnclosed = true;
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public int SubstitutionArray { get; set; }
		public long Position { get; set; }
		public string String { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public LogRoot LogRoot { get; set; }
		public string ToXML() 
		{	
			this.LogRoot.CurrentOpenTags.RemoveAt (this.LogRoot.CurrentOpenTags.Count - 1);
			return " />"; 
		}
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

