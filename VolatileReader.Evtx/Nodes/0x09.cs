using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x09 : INode
	{
		private _x09 (){}
		
		public _x09 (BinaryReader reader)
		{
			throw new NotImplementedException();
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		
		public int SubstitutionArray { get; set; }
		public bool SelfEnclosed { get; set; }
		
		public long Position { get; set; }
		public string String { get; set; }
		
		public string ToXML() { throw new Exception(); }
		public long Length 
		{
			get
			{
				throw new Exception();
			}
			
			set {}
		}
		#endregion
	}
}

