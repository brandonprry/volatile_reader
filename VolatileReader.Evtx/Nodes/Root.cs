using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Root : INode
	{
		private Root (){}
		
		public Root (BinaryReader reader)
		{
			
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

