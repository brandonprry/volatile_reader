using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Pcap
{
	public class PcapFile
	{
		public PcapFile (string filepath)
		{
			using (Stream stream = System.IO.File.OpenRead(filepath))
			{
				using (BinaryReader pcap = new BinaryReader(stream))
				{
					uint magic = pcap.ReadUInt32();
					this.Major = pcap.ReadUInt16();
					this.Minor = pcap.ReadUInt16();
					this.Zone = pcap.ReadInt32();
					this.SigFig = pcap.ReadUInt32();
					this.MaxLength = pcap.ReadUInt32();
					this.Network = pcap.ReadUInt32();
					
					this.Packets = new List<Packet>();
					
					while (pcap.BaseStream.Position < pcap.BaseStream.Length)
						this.Packets.Add(PacketFactory.create(pcap));
					
					Console.WriteLine("hi");
				}
			}
		}
		
		public IList<Packet> Packets { get; set; }
		
		public int Major { get; set; }
		
		public int Minor { get; set; }
		
		public int Zone { get; set; }
		
		public uint SigFig { get; set; }
		
		public uint MaxLength { get; set; }
		
		public uint Network { get; set; }
	}
}

