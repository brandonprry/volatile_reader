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
			this.ReachedEOS = false;
			
			this.SubstitutionArrays = new List<SubstitutionArray>();
			
			long i = (log.BaseStream.Position + length) - 35;
			int k = 0;
			long l = 0;
			while (l < length)
			{
				this.ReachedEOS = false;
				while (!this.ReachedEOS)
				{
					INode node = LogNode.NewNode(log, this, chunkOffset, this);
					node.SubstitutionArray = k;
					this.Nodes.Add(node);
					l += node.Length;
					if (node is _x00)
					{
						this.ReachedEOS = true;
						break;
					}
				}
			
				this.SubstitutionArrays.Add(new SubstitutionArray(log, chunkOffset, this));
			}
		}
		
		public long Position { get; set; }
		
		public List<SubstitutionArray> SubstitutionArrays { get; set; }
		
		public int TagState { get; set; }
		public int ElementType { get; set; }
		
		public List<INode> Nodes { get; set; }
		
		public string String { get; set; }
		
		public long Length 
		{
			get
			{
				throw new Exception();
			}
			
			set {}
		}
		
		public int SubstitutionArray { get; set; }
		public bool ReachedEOS { get; set; }
		
		public string DeferedXML { get; set; } 
		
		public EventLog ParentLog { get; set; }
		
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

