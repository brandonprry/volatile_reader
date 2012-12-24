using System;
using System.IO;
using System.Collections.Generic;

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
			
			this.DataLength
				 = BitConverter.ToInt32(hive.ReadBytes(4),0);
			
			//dataoffset
			byte[] databuf = hive.ReadBytes(4);
			
			this.ValueType = hive.ReadInt32();
			
			//flag and trash, two words -- wordplay is fun
			hive.BaseStream.Position += 4;
			
			buf = hive.ReadBytes(this.NameLength);
			this.Name = (this.NameLength == 0) ? "Default" : System.Text.Encoding.UTF8.GetString(buf);
			
			if (this.DataLength < 5)
				this.Data = databuf;
			else
			{
				this.DataOffset = BitConverter.ToInt32(databuf, 0);
				hive.BaseStream.Position = 0x1000 + this.DataOffset + 0x04;
				this.DataLength = BitConverter.ToInt32 (databuf,0);
				this.Data = hive.ReadBytes(this.DataLength);
			}
			
			if (this.ValueType == 1)
				this.String = System.Text.Encoding.Unicode.GetString(this.Data);
			else if (this.ValueType == 2)
				this.String = System.Text.Encoding.Unicode.GetString(this.Data);
			else if (this.ValueType == 3)
				this.String = BitConverter.ToString(this.Data);
			else if (this.ValueType == 4)
				this.String = BitConverter.ToString(this.Data);
			else if (this.ValueType == 7)
			{
				List<string> strings = new List<string>();
				List<byte> bytes = new List<byte>();
				
				foreach (byte b in this.Data)
				{
					bytes.Add(b);
					
					if (b == 0x00)
					{
						strings.Add(System.Text.Encoding.Unicode.GetString(bytes.ToArray()));
						bytes = new List<byte>();
					}
				}
				
				this.String = string.Empty;
				foreach (string str in strings)
					this.String += str + "\t";
			}
			
			Console.WriteLine(this.Name + ": " + this.String);
		}
		
		public short NameLength { get; set; }
		
		public int DataLength { get; set; }
		
		public int DataOffset { get; set; }
		
		public int ValueType { get; set; }
		
		public string Name { get; set; }
		
		public byte[] Data { get; set; }
		
		public string String { get; set; }
	}
}

