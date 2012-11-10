using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public class Type0x11 : IType
	{
		public Type0x11 (BinaryReader log, int size)
		{
			this.Length = 8;
			
			ulong ts = log.ReadUInt64();
			
			ts /= 1000;
			ts -= 116444736000000;
			int secs = (int)(ts / 10000);
			
			this.Time = GetTime(secs);
			this.String = this.Time.ToString();
		}
		
		public string String { get; set; }
		public DateTime Time { get; set; }
		#region IType implementation
		public byte Type {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public int Length { get; set; }
		#endregion
		
		private DateTime GetTime(int time)
        {
            DateTime output = new DateTime(1970, 1, 1, 0, 0, 0);
            output = output.AddSeconds(time);
            return output;
        }
	}
}

