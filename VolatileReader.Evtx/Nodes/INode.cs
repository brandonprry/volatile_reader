using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public interface INode
	{
		int Length { get; }
		
		byte Header { get; }
		
		int EndOfStream { get; set; }
	}
}

