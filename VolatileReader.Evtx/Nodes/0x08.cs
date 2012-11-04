using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x08 : INode
	{
		private _x08 (){}
		
		public _x08 (BinaryReader reader) 
		{
			throw new Exception("hfs the elusive 0x08");
		}
		
		#region INode implementation
		public int Length {
			get {
				throw new NotImplementedException ();
			}
		}

		public int EndOfStream { get; set; }
		
		public byte Header {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

