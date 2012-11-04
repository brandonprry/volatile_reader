using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class LogRoot
	{
		public LogRoot (BinaryReader log, long chunkOffset)
		{	
			this.ChunkPosition = chunkOffset;
			this.Nodes = new List<INode>();
			
			while (true)
			{
				INode node = LogNode.NewNode(log, null, chunkOffset);
				this.Nodes.Add(node);
				
				if (node is _x00)
					break;
			}
			
			SubstitutionArray arr = new SubstitutionArray(log) { ChunkOffset = ChunkPosition };
		}
		
		public List<INode> Nodes { get; set; }
		
		public EventLog Parent { get; set; }
		
		public long ChunkPosition { get; set; }
		
		public string ToXML()
		{
			return string.Empty;
		}
	}
}

