using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x08 : INode
	{
		private _x08 (){}
		
		public _x08 (BinaryReader reader) 
		{
			throw new Exception("hfs the elusive 0x08");
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long Position { get; set; }
		public int SubstitutionArray { get; set; }
		public long ChunkOffset { get; set; }
		
		public bool SelfEnclosed { get; set; }
		
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

