using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x06 : INode
	{
		byte[] _str;
		int _posdiff;
	
		private _x06 (){}
		
		public _x06 (BinaryReader log, long chunkOffset, LogRoot root) 
		{		
			this.Position = log.BaseStream.Position;
			this.ChunkOffset = chunkOffset;
			this.SelfEnclosed = true;
			int ptr = log.ReadInt32();
			
			log.BaseStream.Position  = this.ChunkOffset + ptr;
			
			this.LogRoot = root;
			
			int next = log.ReadInt32();
			int hash = log.ReadInt16();
			int length2 = log.ReadInt16();
			
			this.Length = 5;
			
			_str = log.ReadBytes((int)(length2*2));
			_posdiff = 2;
			log.BaseStream.Position +=_posdiff;
			
			this.String = System.Text.Encoding.Unicode.GetString(_str);
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

