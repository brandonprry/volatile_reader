using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0A : INode
	{
		private _x0A (){}
		
		public _x0A (BinaryReader reader)
		{
			throw new NotImplementedException();
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

