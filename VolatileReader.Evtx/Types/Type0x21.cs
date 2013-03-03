using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class Type0x21 : IType
	{
		public Type0x21 (BinaryReader log, int size, long chunkOffset, LogRoot root)
		{	
			LogRoot newRoot = new LogRoot(log, chunkOffset, (uint)size, root.ParentLog);
			
			this.String = newRoot.ToXML();
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

