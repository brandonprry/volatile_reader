using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0A : INode
	{
		private _x0A (){}
		
		public _x0A (BinaryReader log, INode parent, LogRoot root, long chunkOffset)
		{
			int length2 = BitConverter.ToInt16 (log.ReadBytes (2), 0);
			long whoa = parent.Length - length2;

			//string 

			Console.WriteLine (length2);
		}
		
		#region INode implementation
		public INode Parent { get; set; }
		public long Position { get; set; }
		public string String { get; set; }
		public bool SelfEnclosed { get; set; }
		
		public int SubstitutionArray { get; set; }
		public long ChunkOffset { get; set; }
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

