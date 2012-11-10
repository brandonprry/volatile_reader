using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0E : INode
	{
		private _x0E (){}
		
		public _x0E (BinaryReader log, long chunkOffset, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			this.Index = log.ReadInt16();
			this.Type = log.ReadByte();
			
			this.LogRoot = root;
			this.TagState = root.TagState;
		}
		
		public short Index { get; set; }
		public int TagState { get; set; }
		public byte Type { get; set; }
		
		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() 
		{ 
			string xml = this.LogRoot.DeferedXML;
			this.LogRoot.DeferedXML = string.Empty;
			
			if (this.TagState == 0)
				xml += this.LogRoot.SubstitutionArray.Types[this.Index].String;
			else if (this.TagState == 1)
				xml += "=\"" + this.LogRoot.SubstitutionArray.Types[this.Index].String + "\"";
			else throw new Exception();
			
			return xml;
		}
		
		public long Length { 
			get
			{
				return 4;
			}
			set {}
		}
		#endregion
	}
}

