using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0B : INode
	{
		byte[] _str;
		
		private _x0B (){}
		
		public _x0B (BinaryReader log, long chunkOffset) 
		{
			short length = log.ReadInt16();
			_str = log.ReadBytes (length*2);
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			
			Console.WriteLine (this.String);
		}
		
		public string String { get; private set; }
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

