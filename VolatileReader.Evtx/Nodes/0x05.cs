using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x05 : INode
	{
		private _x05 (){}
		
		byte[] _str;
		
		public _x05 (BinaryReader log)
		{
			log.BaseStream.Position -= 1;
			this.Header = log.ReadByte();
			this.Type  = log.ReadByte();
			short length = log.ReadInt16();
			_str = log.ReadBytes((int)(length*2));
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			Console.WriteLine(this.String);
		}
		
		public byte Type { get; private set; }
		
		public string String { get; set; }
		
		#region INode implementation
		public int Length {
			get {
				return 1 + 1 + 2 + _str.Length;
			}
		}

		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

