using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace VolatileReader.Pagefile
{
	public class PageFile
	{
		public PageFile (string filepath)
		{
			this.Filepath = filepath;
		}
		
		public string Filepath { get; set; }
		
		public string[] GetASCIIStrings(int minLength)
		{
			List<char> goodChars = new List<char>();
			
			goodChars.Add((char)0x09);
			goodChars.Add((char)0x0a);
			goodChars.Add((char)0x0d);
			
			int i = 32;
			while (i <= 126)
				goodChars.Add((char)(i++));
			
			List<string> strings = new List<string>();
			
			using(Stream stream = File.OpenRead(this.Filepath))
			{
				using (BinaryReader pagefile = new BinaryReader(stream))
				{
					pagefile.BaseStream.Position = 0x1000;
					
					long length = pagefile.BaseStream.Length;
					while (pagefile.BaseStream.Position < length && (length - pagefile.BaseStream.Position) > minLength)
					{
						StringBuilder strBuilder = new StringBuilder();
						char b = pagefile.ReadChar();
						
						if (goodChars.Contains(b))
						{
							strBuilder.Append(b);
							
							while (goodChars.Contains(b))
							{
								b = pagefile.ReadChar();
								
								if (goodChars.Contains(b))
									strBuilder.Append(b);
							}
							
							if (strBuilder.Length >= minLength)
							{
								strings.Add(strBuilder.ToString());
								Console.WriteLine(strBuilder);
							}
						}
					}
				}
			}
			
			return strings.ToArray();
		}
	}
}

