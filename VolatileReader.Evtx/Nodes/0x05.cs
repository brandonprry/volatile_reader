using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x05 : INode
	{
		private _x05 (){}
		
		byte[] _str;
		
		public _x05 (BinaryReader log, long chunkOffset, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			this.Type  = log.ReadByte();
			short length = log.ReadInt16();
			this.SelfEnclosed = true;
			
			this.LogRoot = root;
			this.TagState = root.TagState;
			
			_str = log.ReadBytes((int)(length*2));
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			this.Length = length + 1 + (length*2) + 2;
		}
		
		public byte Type { get; private set; }
		
		public string String { get; set; }
		
		public int TagState { get; set; }
		
		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { return GetXML();}
		public long Length 
		{
get; set; 
		}
		#endregion
		
		private string GetXML()
		{
			string xml = this.LogRoot.DeferedXML;
			this.LogRoot.DeferedXML = string.Empty;
			if (this.TagState == 0)
			{
				return xml + this.String;
			}
			else if (this.TagState == 1)
			{
				return xml + "=\"" + this.String + "\"";
			}
			else throw new Exception("Don't know state: " + this.LogRoot.TagState);
		}
	}
}

