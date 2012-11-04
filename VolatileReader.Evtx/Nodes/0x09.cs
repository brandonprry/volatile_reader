using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x09 : INode
	{
		private _x09 (){}
		
		public _x09 (BinaryReader reader)
		{
			throw new NotImplementedException();
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

