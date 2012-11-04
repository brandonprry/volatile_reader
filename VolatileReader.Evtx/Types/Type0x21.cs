using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x21 : IType
	{
		public Type0x21 (BinaryReader log, int size, long chunkOffset)
		{
			Console.WriteLine(log.BaseStream.Position);
			this.Root = new LogRoot(log, chunkOffset);
		}
		
		public LogRoot Root { get; set; }
		
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

