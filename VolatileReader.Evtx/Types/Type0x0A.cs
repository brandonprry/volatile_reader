using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x0A : IType
	{
		public Type0x0A (BinaryReader log, int size)
		{
			this.String = BitConverter.ToString(log.ReadBytes(8));
			this.Length = 8;
		}
		
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

