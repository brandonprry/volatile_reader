using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class SubstitutionArray 
	{
		private SubstitutionArray (){}
		
		public SubstitutionArray (BinaryReader reader, long chunkOffset, LogRoot root)
		{
			this.ElementCount = reader.ReadInt32();
			this.ChunkOffset = chunkOffset;
			this.Length = 2;
			
			if (this.ElementCount != 0)
			{
				short[][] sizetype = new short[this.ElementCount][];
				
				for (int i = 0; i < this.ElementCount; i++)
				{
					sizetype[i] = new short[2];
					
					short size = reader.ReadInt16();
					byte type = reader.ReadByte();
					reader.BaseStream.Position++; //unknown
					
					sizetype[i][0] = size;
					sizetype[i][1] = type;
				}
				
				this.Types = new List<IType>();
				
				for (int i = 0; i < this.ElementCount; i++)
					this.Types.Add(LogType.NewType(reader, sizetype[i], this.ChunkOffset, root));
			}
		}
		
		public int ElementCount { get; private set; }
		
		public List<IType> Types { get; private set; }
		
		public long ChunkOffset { get; set; }
		
		public int Length { get; private set; }
	}
}

