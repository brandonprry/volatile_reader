using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0B : INode
	{
		byte[] _str;
		
		private _x0B (){}
		
		public _x0B (BinaryReader log, long chunkOffset, ref LogRoot root) 
		{
			this.Position = log.BaseStream.Position;
			short length = log.ReadInt16();
			_str = log.ReadBytes (length*2);
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			this.LogRoot = root;
			this.Length = (length*2) + 2;
		}
		
		public string String { get; private set; }
		
		#region INode implementation
		public long Position { get; set; }
		public INode Parent { get; set; }
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

