using System;
using System.IO;
using System.Text;

namespace VolatileReader.Evt
{
	public class LogItem
	{
		public LogItem (BinaryReader log)
		{
			long pos = log.BaseStream.Position;
			log.BaseStream.Position -= 4;
			
			uint el = log.ReadUInt32();
			log.BaseStream.Position -= el - 4;
			
			uint r = log.ReadUInt32();
			uint rn = log.ReadUInt32();
			uint tg = log.ReadUInt32();
			uint tw = log.ReadUInt32();
			uint eid = log.ReadUInt32();
			ushort et = log.ReadUInt16();
			ushort ns = log.ReadUInt16();
			ushort ec = log.ReadUInt16();
			ushort rf = log.ReadUInt16();
			uint crn = log.ReadUInt32();
			uint so = log.ReadUInt32 ();
			uint usl = log.ReadUInt32();
			uint uso = log.ReadUInt32();
			uint dl = log.ReadUInt32();
			uint dof = log.ReadUInt32();
			
			StringBuilder sb = new StringBuilder();
			char tmp = '\r';
			while (tmp != '\0')
				sb.Append(tmp = BitConverter.ToChar(log.ReadBytes(2),0));
			
			this.SourceName = sb.ToString();
			
			sb = new StringBuilder();
			tmp = '\r';
			while (tmp != '\0')
				sb.Append(tmp = BitConverter.ToChar(log.ReadBytes(2),0));
			
			this.ComputerName = sb.ToString();
			
			byte[] uname = log.ReadBytes((int)usl);
			
			this.UserSID = Encoding.ASCII.GetString(uname);
			
			sb = new StringBuilder();
			tmp = '\r';
			while(tmp != '\0')
				sb.Append(tmp = BitConverter.ToChar(log.ReadBytes(2),0));
			
			this.Strings = sb.ToString();
			
			this.Data = log.ReadBytes((int)dl);

			this.TimeGenerated = GetTime(tg);
			this.TimeWritten = GetTime(tw);
			log.BaseStream.Position = pos;
		}
		
		public string SourceName { get; set; }
		
		public string ComputerName { get; set; }
		
		public string UserSID { get; set; }
		
		public string Strings { get; set; }
		
		public byte[] Data { get; set; }
		
		public DateTime TimeGenerated { get; set; }
		
		public DateTime TimeWritten { get; set; }
		
		private DateTime GetTime(uint time)
        {
            DateTime output = new DateTime(1970, 1, 1, 0, 0, 0);
            output = output.AddSeconds(time);
            return output;
        }
	}
}

