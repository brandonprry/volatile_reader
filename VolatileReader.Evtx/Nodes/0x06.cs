using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x06 : INode
	{
		byte[] _str;
		int _posdiff;
	
		private _x06 (){}
		
		public _x06 (BinaryReader log) 
		{		
			log.BaseStream.Position -= 1;
			this.Header = log.ReadByte();
			int ptr = log.ReadInt32();
			
			log.BaseStream.Position  = 4096 + ptr;
			
			int next = log.ReadInt32();
			int hash = log.ReadInt16();
			int length2 = log.ReadInt16();
			
			_str = log.ReadBytes((int)(length2*2));
			_posdiff = ((length2+1)*2)-(length2*2);
			log.BaseStream.Position +=_posdiff;
			
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			
			Console.WriteLine(this.String);
		}
		
		public string String { get; private set; }
		
		#region INode implementation
		public int Length {
			get {
				return 1 + 4 + 4 + 2 + _str.Length + _posdiff;
			}
		}

		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

