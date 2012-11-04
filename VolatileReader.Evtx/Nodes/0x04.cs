using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x04 : INode
	{
		private _x04 (){}
		
		public _x04 (BinaryReader reader)
		{
			reader.BaseStream.Position -= 1;
			this.Header = reader.ReadByte();
		}
		
		#region INode implementation
		public int Length {get {return 1;}}

		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

