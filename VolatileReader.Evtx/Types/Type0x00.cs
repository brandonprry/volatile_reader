using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x00 : IType
	{
		public Type0x00 (BinaryReader log, int size)
		{
			this.Length = 0;
			log.BaseStream.Position += size;
			this.String = string.Empty;
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

