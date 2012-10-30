using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evt
{
	public class LegacyEventLog
	{
		public LegacyEventLog (string filename)
		{
			using (FileStream stream = File.OpenRead(filename))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					uint headerLength = reader.ReadUInt32();
					uint signature = reader.ReadUInt32();
					reader.BaseStream.Position += 12;
					uint footerOffset  = reader.ReadUInt32();
					uint nextIndex = reader.ReadUInt32();
					uint fileLength = reader.ReadUInt32();
					reader.BaseStream.Position += 12;
					uint endHeaderLength = reader.ReadUInt32();
					int totalEvents = (int)nextIndex - 1;
					
					reader.BaseStream.Position = footerOffset;
					
					this.Items = new List<LogItem>();
					for (int c = 0; c <= totalEvents; c++)
						this.Items.Add(new LogItem(reader));
				}
			}
		}
		
		public List<LogItem> Items { get; set; }
	}
}

