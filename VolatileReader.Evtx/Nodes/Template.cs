using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Template : INode
	{
		private Template (){}
		
		public Template (BinaryReader reader)
		{
			
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

