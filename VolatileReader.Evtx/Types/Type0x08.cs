using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x08 : IType
	{
		public Type0x08 (BinaryReader log, int size)
		{
			this.Length = 4;
			this.String = BitConverter.ToString(log.ReadBytes(4));
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

		public int Length { get; set; }
		#endregion
	}
}

