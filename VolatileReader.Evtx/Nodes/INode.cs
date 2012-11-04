using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public interface INode
	{
		INode Parent { get; set; }
		
		long ChunkOffset { get; set; }
	}
}

