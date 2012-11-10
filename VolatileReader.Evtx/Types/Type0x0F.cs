using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x0F : IType
	{
		public Type0x0F (BinaryReader log, int size)
		{
			this.Length = 16;
			byte[] g = log.ReadBytes(16);
			
			this.GUID = new Guid(g);
			
			this.String = this.GUID.ToString();
		}
		
		public Guid GUID { get; set; }
		
		public string String { get; set; }
		#region IType implementation
		public byte Type {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public int Length {get; set; }
		#endregion
	}
}

