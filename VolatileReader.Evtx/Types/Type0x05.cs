using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x05 : IType
	{
		public Type0x05  (BinaryReader log, int size)
		{
			throw new NotSupportedException();
		}
		
		public string String { get; set; }
		#region IType implementation
		byte IType.Type {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		int IType.Length {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

