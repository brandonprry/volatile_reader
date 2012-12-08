using System;
using System.IO;

namespace VolatileReader.Pcap
{
	public static class PacketFactory
	{
		public static Packet create(BinaryReader pcap)
		{
			Packet pkt = new Packet(pcap);
			
			return pkt;
		}
	}
}

