using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class Type0x21 : IType
	{
		public Type0x21 (BinaryReader log, int size, long chunkOffset, LogRoot root)
		{	
			this.ChildNodes = new List<INode>();	
			
			while (!root.ReachedEOS)
			{
				INode node = LogNode.NewNode(log, null, chunkOffset, root);
				this.ChildNodes.Add(node);
				
				if (node is _x00)
				{
					root.ReachedEOS = true;
					break;
				}
			}
			
			this.String = string.Empty;
			
			foreach (INode node in this.ChildNodes)
				this.String += node.ToXML();
		}
		
		public LogRoot Root { get; set; }
		
		public List<INode> ChildNodes { get; set; }
		
		public string String { get; set; }
		#region IType implementation
		public byte Type {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public int Length {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

