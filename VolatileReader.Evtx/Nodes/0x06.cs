using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x06 : INode
	{
		byte[] _str;
	
		private _x06 (){}
		
		public _x06 (BinaryReader log, long chunkOffset, LogRoot root, INode parent) 
		{		
			this.Position = log.BaseStream.Position;
			this.ChunkOffset = chunkOffset;
			this.SelfEnclosed = true;
			int ptr = log.ReadInt32();
			
			this.LogRoot = root;
			this.Length = 5; //tag length
			
			if (!root.ParentLog.Strings.ContainsKey(this.ChunkOffset + ptr))
			{
				log.BaseStream.Position  = this.ChunkOffset + ptr;
				
				int next = log.ReadInt32();
				int hash = log.ReadInt16();
				int length2 = log.ReadInt16();
				
				_str = log.ReadBytes((int)(length2*2));
				log.BaseStream.Position +=2;
				
				this.String = root.ParentLog.Strings[this.ChunkOffset + ptr] = System.Text.Encoding.Unicode.GetString(_str);
				this.Length +=(length2+1)*2;
			}
			else
				this.String = root.ParentLog.Strings[this.ChunkOffset + ptr];
			
			this.Length += 8;
		}
		
		public string String { get;  set; }
		
		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public int SubstitutionArray { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public string ToXML() 
		{ 
			this.LogRoot.DeferedXML += (" " + this.String); 
			return string.Empty;
		}
		public long Length 
		{
			get; set; 
		}
		#endregion
	}
}

