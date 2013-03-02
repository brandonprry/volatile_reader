using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace VolatileReader.Evtx
{
	public class LogRoot : INode
	{
		public LogRoot (BinaryReader log, long chunkOffset, uint length)
		{	
			this.Position = log.BaseStream.Position;
			this.ChunkOffset = chunkOffset;
			this.Nodes = new List<INode>();
			this.Length = length;
		
			while (this.Length >= 0 && !this.ReachedEOS)
			{
				INode node = LogNode.NewNode(log, this, chunkOffset, this);
				this.Nodes.Add(node);
				this.Length -= node.Length;
				
				if (node is _x00)
					this.ReachedEOS = true;
			}
			
			this.SubstitutionArray = new SubstitutionArray(log, chunkOffset, this);
		}
		
		public long Position { get; set; }
		
		public SubstitutionArray SubstitutionArray { get; set; }
		
		public int TagState { get; set; }
		public int ElementType { get; set; }
		
		public List<INode> Nodes { get; set; }
		
		public string String { get; set; }
		
		public long Length { get; set; }
	
		public bool ReachedEOS { get; set; }
		
		public string DeferedXML { get; set; } 
		
		public EventLog ParentLog { get; set; }
		
		public long AmountRead { get; set; }
		
		public long Offset { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
		public string ToXML()
		{
			string xml = string.Empty;
			
			foreach (INode node in this.Nodes)
				xml += node.ToXML();
			
			return xml;
		}

		#region INode implementation
		public INode Parent {
			get {
				return null;
			}
			set {
				return;
			}
		}

		public long ChunkOffset { get; set; }
		#endregion
	}
}

