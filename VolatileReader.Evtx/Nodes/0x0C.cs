using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class _x0C : INode
	{
		private _x0C (){}
		
		public _x0C (BinaryReader log)
		{
			log.BaseStream.Position -= 1;
			this.Header = log.ReadByte();
			
			log.BaseStream.Position += 1;
			int templateID = log.ReadInt32();
			int ptr = log.ReadInt32();
			
			log.BaseStream.Position = ((int)(log.BaseStream.Position / 4096) * 4096) + ptr;
			
			int nextTemplate = log.ReadInt32();
			int templateID2 = log.ReadInt32();
			
			log.BaseStream.Position -= 4;
			
			byte[] g = log.ReadBytes(16);
			Guid templateGuid = new Guid(g);
			int templateLength = log.ReadInt32();
		}
		
		#region INode implementation
		public int Length {
			get {
				return 1 + 1 + 4 + 4 + 4 + 4 + 12 + 4;
			}
		}

		public int EndOfStream { get; set; }
		
		public byte Header {get; private set;}
		#endregion
	}
}

