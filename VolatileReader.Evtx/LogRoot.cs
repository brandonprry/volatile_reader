using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class LogRoot
	{
		public LogRoot (BinaryReader log)
		{	
			
			this.Nodes = new List<INode>();
			
			while (true)
			{
				INode node = LogNode.NewNode(log);
				this.Nodes.Add(node);
				
				if (node is _x00)
					break;
			}
			
			SubstitutionArray arr = new SubstitutionArray(log);
		}
		
		public List<INode> Nodes { get; set; }
		

		
		public string ToXML()
		{
			return string.Empty;
		}
	}
}

