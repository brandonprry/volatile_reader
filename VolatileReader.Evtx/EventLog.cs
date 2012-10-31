using System;
using System.Collections.Generic;
using System.IO;

namespace VolatileReader.Evtx
{
	public class EventLog
	{
		public EventLog (string filename)
		{
			using (FileStream stream = File.OpenRead(filename))
			{
				using (BinaryReader reader = new BinaryReader(stream))
				{
					byte[] h = reader.ReadBytes(8);
					
					if (!(h[0] == 'E' && h[1] == 'l' && h[2] == 'f' && h[3] == 'F' && h[4] == 'i' && h[5] == 'l' && h[6] == 'e'))
						throw new Exception("Unsupported file type");
					
					reader.BaseStream.Position = 0x1000;
					
					while (true)
					{
						h = reader.ReadBytes(8);
						
						if (!(h[0] == 'E' && h[1] == 'l' && h[2] == 'f' && h[3] == 'C' && h[4] == 'h' && h[5] == 'n' && h[6] == 'k'))
						{
							Console.WriteLine("Bad chunk at offset: " + reader.BaseStream.Position);
							continue;
						}
						
						//we are at the first chunk. all chunks are 0x1000 (4096) bytes away from each other
						long pos = reader.BaseStream.Position - 8;
						
						ulong first = reader.ReadUInt64();
						ulong last = reader.ReadUInt64();
						ulong rfirst = reader.ReadUInt64(); //redundant first
						ulong rlast = reader.ReadUInt64(); //redundant last
						uint unk1 = reader.ReadUInt32();
						uint unk2 = reader.ReadUInt32();
						
						uint offset = reader.ReadUInt32();
						uint crcData = reader.ReadUInt32();
						uint crcHeader = reader.ReadUInt32();
						
						uint nextOffset = 0x200; //first event in a chunk
						
						this.Items = new List<LogItem>();
						while (true)
						{
							reader.BaseStream.Position = pos + nextOffset;
							
							h = reader.ReadBytes(4);
							if (h[0] != '*' && h[1] != '*')
								throw new Exception("Bad event");
							
							uint el = reader.ReadUInt32();
							ulong rid = reader.ReadUInt64();
							ulong ts = reader.ReadUInt64();
							
							ts /= 1000;
							ts -= 116444736000000;
							int secs = (int)(ts / 10000);
							
							DateTime timestamp = GetTime (secs);
							
							int rootLength = (int)(el - 28);
							
							while (rootLength > 0)
							{
								LogItem item = new LogItem(reader, rootLength);
								
								if (item != null)
								{
									this.Items.Add(item);
									rootLength -= (int)item.Length;
								}
							}
						}
						
						reader.BaseStream.Position = pos + 0x1000;
					}
				}
			}
		}
		
		public List<LogItem> Items { get; set; }
		
		public string ToXML()
		{
			string xml = "<Events>";
			
			foreach (LogItem item in this.Items)
				xml += item.ToXML();
			
			xml += "</Events>";
			
			return xml;
		}
		
		private DateTime GetTime(int time)
        {
            DateTime output = new DateTime(1970, 1, 1, 0, 0, 0);
            output = output.AddSeconds(time);
            return output;
        }
		
	}
}

