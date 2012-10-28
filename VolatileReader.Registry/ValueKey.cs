using System;
using System.IO;

namespace VolatileReader.Registry
{
	public class ValueKey
	{
		public ValueKey (BinaryReader hive)
		{
			byte[] buf = hive.ReadBytes(2);
			
			if (buf[0] != 0x76 && buf[1] != 0x6b)
				throw new NotSupportedException("Bad vk header");
			
			buf = hive.ReadBytes(2);
			
			this.NameLength = BitConverter.ToInt16(buf,0);
			
			//used later possibly -- datalength
			byte[] databuf = hive.ReadBytes(4);
			
			this.DataOffset = BitConverter.ToInt32(hive.ReadBytes(4),0);
			
			//type
			buf = hive.ReadBytes(4);
			
			this.ValueType = buf[0];
			
			//flag and trash, two words
			hive.BaseStream.Position += 4;
			
			buf = hive.ReadBytes(this.NameLength);
			this.Name = (this.NameLength == 0) ? "Default" : System.Text.Encoding.UTF8.GetString(buf);
			
			if (this.DataLength < 5)
				this.Data = databuf;
			else
			{
				hive.BaseStream.Position = 0x1000 + this.DataOffset + 0x04;
				this.DataLength = BitConverter.ToInt32 (databuf,0);
				this.Data = hive.ReadBytes(this.DataLength);
			}
		}
		
		public short NameLength { get; set; }
		
		public int DataLength { get; set; }
		
		public int DataOffset { get; set; }
		
		public byte ValueType { get; set; }
		
		public string Name { get; set; }
		
		public byte[] Data { get; set; }
	}
}

