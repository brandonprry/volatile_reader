using System;
using System.Collections.Generic;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Template : INode
	{
		public Template (BinaryReader log, long chunkOffset, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			this.LogRoot = root;
			this.ChunkOffset = chunkOffset;
			log.BaseStream.Position += 1;
			int templateID = log.ReadInt32();
			int ptr = log.ReadInt32();
			
			log.BaseStream.Position = this.ChunkOffset + ptr;
			
			int nextTemplate = log.ReadInt32();
			int templateID2 = log.ReadInt32();
			
			log.BaseStream.Position -= 4;
			
			byte[] g = log.ReadBytes(16);
			Guid templateGuid = new Guid(g);
			int templateLength = log.ReadInt32();
			
			this.ChildNodes = new List<INode>();
			
			while(!root.ReachedEOS)
			{
				INode node = LogNode.NewNode(log, this, chunkOffset, root);
				
				this.ChildNodes.Add(node);
				
				if (node is _x00)
				{
					root.ReachedEOS = true;
					break;
				}
			}
			
			this.Length = 1 + 4 + 4 + 4 + 4 + 12 + 4;
		}
		
		public LogRoot LogRoot { get; set; }
		
		public long Position { get; set; }
		public List<INode> ChildNodes { get; set; }
		
		#region INode implementation
		public string ToXML ()
		{
			string xml = string.Empty;
			foreach (INode node in this.ChildNodes)
				xml += node.ToXML();
			
			return xml;
		}

		public INode Parent {
get; set; 
		}

		public long ChunkOffset {
			get; set; 
		}

		public long Length {
			get; set; 
		}
		#endregion
	}
}

