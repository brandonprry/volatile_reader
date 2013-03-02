using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0B : INode
	{
		byte[] _str;
		
		private _x0B (){}
		
		public _x0B (BinaryReader log, long chunkOffset, LogRoot root, INode parent) 
		{
			this.Position = log.BaseStream.Position;
			this.SelfEnclosed = true;
			short length = log.ReadInt16();
			_str = log.ReadBytes (length*2);
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			this.LogRoot = root;
			this.Length = (length*2) + 2;
		}
		
		public string String { get;  set; }
		
		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
		
		public bool SelfEnclosed { get; set; }
		public int SubstitutionArray { get; set; }
		
		public long ChunkOffset { get; set; }
		public LogRoot LogRoot { get; set; }
		public string ToXML() { throw new Exception(); }
		public long Length 
		{
			get
			{
				throw new Exception();
			}
			
			set {}
		}
		#endregion
	}
}

