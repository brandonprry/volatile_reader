using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

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
					
					int numOldLow, numOldHigh;
					int numCurLow, numCurHigh;
					int nextRecLow, nextRecHigh;
					
					numOldLow = reader.ReadInt32();
					numOldHigh = reader.ReadInt32();
					numCurLow = reader.ReadInt32();
					numCurHigh = reader.ReadInt32();
					nextRecLow = reader.ReadInt32();
					nextRecHigh = reader.ReadInt32();
					
					int numOld = (numOldHigh << 32) ^ numOldLow;
					int numCur = (numCurHigh << 32) ^ numCurLow;
					int nextRec = (nextRecHigh << 32) ^ nextRecLow;
					
					byte[] headerPart, versionMinor, versionMajor, headerLen, chunkCount;
					
					headerPart = reader.ReadBytes(4);
					versionMinor = reader.ReadBytes(2);
					versionMajor = reader.ReadBytes(2);
					headerLen = reader.ReadBytes(2);
					chunkCount = reader.ReadBytes(2);
					
					if (!BitConverter.IsLittleEndian)
					{
						Array.Reverse(headerLen);
						Array.Reverse(headerPart);
						Array.Reverse(versionMajor);
						Array.Reverse(versionMinor);
						Array.Reverse(chunkCount);
					}
					
					int chunkc = BitConverter.ToInt16(chunkCount,0);
					
					
					long chunkOffset = 0x1000;
					reader.BaseStream.Position = chunkOffset;
					
					int n = 1;
					//I break out later
					for (int k = 0; k < chunkc; k++)
					{
						h = reader.ReadBytes(8);
						
						if (!(h[0] == 'E' && h[1] == 'l' && h[2] == 'f' && h[3] == 'C' && h[4] == 'h' && h[5] == 'n' && h[6] == 'k'))
							throw new Exception("Bad chunk at offset: " + reader.BaseStream.Position);
						
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
						
						if (crcData == 0)
							continue; //empty chunk
						
						reader.BaseStream.Position = chunkOffset + nextOffset; //(512+4096)
						
						this.Roots = new List<LogRoot>();
						for (ulong i = first; i <= last; i++)
						{
							long pos = reader.BaseStream.Position;
							
							h = reader.ReadBytes(2);
							
							if (h[0] != '*' || h[1] != '*')
								throw new Exception("Bad event at position: " + (reader.BaseStream.Position-2));
							
							reader.BaseStream.Position += 2; //junk? always 0x0000?
							
							uint el = reader.ReadUInt32();
							long rid = reader.ReadInt64();
							ulong ts = reader.ReadUInt64();
							
							ts /= 1000;
							ts -= 116444736000000;
							
							int secs = (int)(ts / 10000);
							DateTime timestamp = GetTime (secs);
							this.Roots.Add(new LogRoot(reader, chunkOffset) { Parent = this });
							
							reader.BaseStream.Position += (el + pos) - reader.BaseStream.Position;
						}
						
						reader.BaseStream.Position = chunkOffset = (n*0x10000)+0x1000;
						n++;
					}
				}
			}
		}
		
		private List<LogRoot> Roots { get; set; }
		
		public List<LogItem> Items { get; set; }
		
		public XmlDocument XmlDocument { get; set; }
		
		private DateTime GetTime(int time)
        {
            DateTime output = new DateTime(1970, 1, 1, 0, 0, 0);
            output = output.AddSeconds(time);
            return output;
        }
	}
}









