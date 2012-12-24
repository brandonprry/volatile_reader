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
		
		public string[] GetPossibleEnvironmentVariables(string[] strings, int minLength)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[A-Za-z]{3,}=[A-Za-z]+");
			
			List<string> vars = new List<string>();
			foreach(string str in strings)
				if (str.Length > minLength && r.IsMatch(str))
					vars.Add(str);
			
			return vars.ToArray();
		}
		
		public string Filepath { get; set; }
		
		public string[] GetASCIIStrings(int minLength)
		{	
			List<string> strings = new List<string>();
			
			using(Stream stream = File.OpenRead(this.Filepath))
			{
				using (BinaryReader pagefile = new BinaryReader(stream))
				{
					pagefile.BaseStream.Position = 0x1000;
					
					long length = pagefile.BaseStream.Length;
					StringBuilder strBuilder = new StringBuilder();
					while (pagefile.BaseStream.Position < length && (length - pagefile.BaseStream.Position) > minLength)
					{
						int bytes = (pagefile.BaseStream.Length - pagefile.BaseStream.Position) > 4096 ? 4096 : (int)(pagefile.BaseStream.Length - pagefile.BaseStream.Position);
						byte[] buffer = pagefile.ReadBytes(bytes);
						
						for (int i = 0; i < buffer.Length-minLength;)
						{
							byte b = buffer[i];
							
							if (b == 0x09 || b == 0x0a || b == 0x0d || (b >= 32 && b <= 126))
							{
								int k = 1;
								while ((b == 0x09 || b == 0x0a || b == 0x0d || (b >= 32 && b <= 126)) && i+k < buffer.Length)
								{
									strBuilder.Append((char)b);
									b = buffer[i+k++];
								}
								
								i += strBuilder.Length;
								
								if (strBuilder.Length >= minLength)
								{
									strings.Add(strBuilder.ToString());
									Console.WriteLine(strBuilder);
									strBuilder = new StringBuilder();
								}
							}
							else
								i++;
						}
					}
				}
			}
			
			return strings.ToArray();
		}
	}
}

