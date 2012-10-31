using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class LogItem
	{
		public LogItem (BinaryReader log, int len)
		{
			long pos = log.BaseStream.Position;
			byte op = log.ReadByte();
				
			if (op == 0x0f)
			{
				this.TagLength = 4;
				this.DataLength = 0;
				this.Length = 4;
			}
			else if (op == 0x0c)
			{
				log.BaseStream.Position +=1;
				uint tid = log.ReadUInt32();
				uint ptr = log.ReadUInt32();
				this.TagLength = 8+1+1;
				uint next = log.ReadUInt32();
				uint templateID = log.ReadUInt32();
				log.BaseStream.Position -= 4;
				byte[] gdata = log.ReadBytes(16);
				Guid guid = new Guid(gdata);
				this.TagLength += 16+4+4;
				uint dlength = log.ReadUInt32();
				this.DataLength = dlength;
				this.Length = this.TagLength + this.DataLength;
				
				this.Children = new List<LogItem>();
				
				this.Children.Add(new LogItem(log, len));
			}
			else
				throw new Exception(String.Format("Don't know op: {0:x2}", op));
			
		
			log.BaseStream.Position += this.Length-1; //length - opcode
		}
		
		public List<LogItem> Children { get; set; }
		
		public uint Length { get; set; }
		
		public uint TagLength { get; set; }
		
		public uint DataLength { get; set; }
		
		public string ToXML()
		{
			return string.Empty;
		}
	}
}

