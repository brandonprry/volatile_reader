using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x13 : IType
	{
		public Type0x13 (BinaryReader log, int size)
		{
			string sid = "S-";
			int version = (int)log.ReadByte();
			int elements = (int)log.ReadByte();
			
			byte[] h = log.ReadBytes(4);
			byte[] l = log.ReadBytes(2);
			
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(h);
				Array.Reverse(l);
			}
			
			sid += version;
			
			uint high = BitConverter.ToUInt32(h,0);
			ushort low = BitConverter.ToUInt16(l,0);
			
			long id = (high << 16) ^ low;
			
			sid += "-" + id;
			
			byte[] data = log.ReadBytes(elements*4);
			
			for (int i = 0; i < elements;i++)
			{
				int a = i*4;
				byte[] r = new byte[4] { data[a], data[a+1], data[a+2], data[a+3] };
				
				sid += "-" + BitConverter.ToInt32(r,0);
			}
			
			this.SID = sid;
			this.String = this.SID;
			this.Length = 8 + elements*4;
		}
		
		public string SID { get; set; }
		
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

		public int Length {get; set;}
		#endregion
	}
}

