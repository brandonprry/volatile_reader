using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0A : INode
	{
		private _x0A (){}
		
		public _x0A (BinaryReader reader)
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

