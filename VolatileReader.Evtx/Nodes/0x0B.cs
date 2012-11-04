using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0B : INode
	{
		byte[] _str;
		
		private _x0B (){}
		
		public _x0B (BinaryReader log) 
		{
			log.BaseStream.Position -= 1;
			this.Header = log.ReadByte();
			short length = log.ReadInt16();
			_str = log.ReadBytes (length*2);
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			
			Console.WriteLine (this.String);
		}
		
		public string String { get; private set; }
		
		#region INode implementation
		public int Length {
			get {
				return 1 + 2 + _str.Length;
			}
		}

		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

