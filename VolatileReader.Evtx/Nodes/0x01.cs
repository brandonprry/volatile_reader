using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public class _x01 : INode
	{
		int _length;
		int _ptr;
		int _next;
		short _hash;
		short _length2;
		int _posdiff;
		byte[] _str;
		bool _addFour = false;
		
		private _x01 (){}
		
		public _x01 (BinaryReader log, long chunkOffset, int flags)
		{
			this.ChunkOffset = chunkOffset;
			_addFour = ((flags & 4) == 0) ? false : true;
			
			log.BaseStream.Position += 2; //short unk1
			_length = log.ReadInt32();
			_ptr = log.ReadInt32();
			
			log.BaseStream.Position = this.ChunkOffset+_ptr;
			
			_next = log.ReadInt32();
			_hash = log.ReadInt16();
			_length2 = log.ReadInt16();
			
			_str = log.ReadBytes((int)(_length2*2));
			_posdiff = ((_length2+1)*2)-(_length2*2);
			log.BaseStream.Position +=_posdiff+(_addFour?4:0);
			this.String = System.Text.Encoding.Unicode.GetString(_str);
			Console.WriteLine(this.String);
		}
		
		public string String { get; private set; }
		
		public List<INode> ChildNodes { get; private set; }
		
		#region INode implementation
		public INode Parent { get; set; }
		public long ChunkOffset { get; set; }
		#endregion
	}
}

