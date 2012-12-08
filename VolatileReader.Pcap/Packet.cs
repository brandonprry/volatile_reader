using System;
using System.IO;

namespace VolatileReader.Pcap
{
	public class Packet
	{
		public Packet (BinaryReader pcap)
		{
			this.TimestampSeconds = pcap.ReadUInt32();
			this.TimestampMilliseconds = pcap.ReadUInt32();
			this.OctetLength = pcap.ReadUInt32();
			this.Length = pcap.ReadUInt32();
			
			this.Data = new byte[this.OctetLength];
			
			for (uint i = 0; i < this.OctetLength; i++)
				this.Data[i] = pcap.ReadByte();
		}
		
		public uint TimestampSeconds { get; set; }
		
		public uint TimestampMilliseconds { get; set; }
		
		public uint OctetLength { get; set; }
		
		public uint Length { get; set; }
		
		public byte[] Data { get; set; }
	}
}

