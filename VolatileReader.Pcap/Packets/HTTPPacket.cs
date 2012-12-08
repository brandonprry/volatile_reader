using System;
using System.IO;

namespace VolatileReader.Pcap
{
	public class HTTPPacket : Packet
	{
		public HTTPPacket (BinaryReader pcap) : base (pcap)
		{
		}
	}
}

