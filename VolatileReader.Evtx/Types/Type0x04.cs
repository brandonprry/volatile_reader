using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x04 : IType
	{
		public Type0x04  (BinaryReader log, int size)
		{
			this.Length = 1;
			this.Data = log.ReadByte();
			this.String = this.Data.ToString();
			
		}
		
		public string String { get; set; }
		public byte Data { get; set; }
		
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

