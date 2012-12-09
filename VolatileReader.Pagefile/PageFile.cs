using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Pagefile
{
	public class Pagefile
	{
		public Pagefile (string filepath)
		{
			this.Filepath = filepath;
		}
		
		public string Filepath { get; set; }
		
		public string[] GetASCIIStrings()
		{
			List<byte> goodChars = new List<byte>();
			
			goodChars.Add(0x09);
			goodChars.Add(0x0a);
			goodChars.Add(0x0d);
			
			int i = 32;
			while (i <= 126)
				goodChars.Add((byte)(i++));
			
			List<string> strings = new List<string>();
			
			using(Stream stream = File.OpenRead(this.Filepath))
			{
				using (BinaryReader pagefile = new BinaryReader(stream))
				{
					
				}
			}
			
			return strings.ToArray();
		}
	}
}

