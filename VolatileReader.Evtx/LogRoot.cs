using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace VolatileReader.Evtx
{
	public class LogRoot : INode
	{
		public LogRoot (BinaryReader log, long chunkOffset, int length)
		{	
			this.Position = log.BaseStream.Position;
			this.ChunkOffset = chunkOffset;
			this.Nodes = new List<INode>();
		
			this.ReachedEOS = false;
			while (!this.ReachedEOS)
			{
				INode node = LogNode.NewNode(log, this, chunkOffset, this);
				this.Nodes.Add(node);
				
				if (node is _x00)
				{
					this.ReachedEOS = true;
					break;
				}
			}
		
				
			this.SubstitutionArray = new SubstitutionArray(log, this.ChunkOffset, this);

		}
		
		public long Position { get; set; }
		
		public SubstitutionArray SubstitutionArray { get; set; }
		
		public int TagState { get; set; }
		
		public int ElementType { get; set; }
		
		public List<INode> Nodes { get; set; }
		
		public long Length 
		{
			get
			{
				throw new Exception();
			}
			
			set {}
		}
		
		public bool ReachedEOS { get; set; }
		
		public string DeferedXML { get; set; } 
		
		public EventLog ParentLog { get; set; }
		
		public string ToXML()
		{
			string xml = string.Empty;
			
			foreach (INode node in this.Nodes.OrderBy(n => n.Position))
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

