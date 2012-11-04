using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x15 : IType
	{
		public Type0x15(BinaryReader log, int size)
		{
			this.Length = 8;
			
			byte[] data = log.ReadBytes(8);
			
			this.String = BitConverter.ToString(data);
			Console.WriteLine(this.String);
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

