using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x06 : IType
	{
		public Type0x06 (BinaryReader log, int size)
		{
			this.Length = 2;
			this.Data = log.ReadInt16();
			
			this.String = this.Data.ToString();
		}
		
		
		public string String { get; set; }
		public short Data { get; set; }
		
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

