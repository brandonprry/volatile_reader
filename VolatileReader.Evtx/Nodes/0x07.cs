using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x07 : INode
	{
		private _x07 (){}
		
		public _x07 (BinaryReader log)
		{
			log.BaseStream.Position -= 1;
			this.Header = log.ReadByte();
			short length = log.ReadInt16();	
		}
		
		#region INode implementation
		public int Length {
			get {
				return 3;
			}
		}

		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

