using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Template : INode
	{
		private Template (){}
		
		public Template (BinaryReader reader)
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

