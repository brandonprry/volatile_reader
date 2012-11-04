using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0F : INode
	{
		private _x0F (){}
		
		public _x0F (BinaryReader log)
		{
			log.BaseStream.Position -= 1;
			this.Header = log.ReadByte();
			
			char oth1 = log.ReadChar();
			short oth2 = log.ReadInt16();
			
			if (oth1 != 1 && oth2 != 1)
				throw new Exception("Bad 0x0f node -- oth1: " + oth1 + " :: oth2: " + oth2);
		}

		#region INode implementation
		public int Length {
			get {
				return 1 + 1 + 2;
			}
		}
		
		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

