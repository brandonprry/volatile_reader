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
		
		private _x01 ()
		{
		}
		
		public _x01 (BinaryReader log, long chunkOffset, int flags, ref LogRoot root)
		{
			this.Position = log.BaseStream.Position;
			this.LogRoot = root;
			this.SelfEnclosed = false;
			this.LogRoot.TagState = 1;
			this.ChunkOffset = chunkOffset;
			_addFour = ((flags & 4) == 0) ? false : true;
			
			log.BaseStream.Position += 2; //short unk1
			_length = log.ReadInt32 ();
			_ptr = log.ReadInt32 ();
			
			log.BaseStream.Position = this.ChunkOffset + _ptr;
			
			_next = log.ReadInt32 ();
			_hash = log.ReadInt16 ();
			_length2 = log.ReadInt16 ();
			
			this.Length = _length + ((_length2+1)*2) + 1 + 2 + 4 + 4 + 4 + 2 + 2;
			
			_str = log.ReadBytes ((int)(_length2 * 2));
			_posdiff = ((_length2+1) * 2) - (_length2 * 2);
			log.BaseStream.Position += _posdiff + (_addFour ? 4 : 0);
			this.String = System.Text.Encoding.Unicode.GetString (_str);
			
			this.ChildNodes = new List<INode>();
			
			long i = 0;
			while(!root.ReachedEOS)
			{
				INode node = LogNode.NewNode(log, this, chunkOffset, root);
				
				this.ChildNodes.Add(node);
				
				if (node is _x04 || node is _x03)
					break;
				
				i += node.Length;
				if (node is _x00)
				{
					root.ReachedEOS = true;
					break;
				}
			}
		}
		
		public long Position { get; set; }
		public List<INode> ChildNodes { get; set; }
		
		public string String { get; private set; }
		
		public LogRoot LogRoot { get; set; }

		public bool SelfEnclosed { get; set; }
		
		
		public bool IsValue { get; set; }
		
		#region INode implementation
		public INode Parent { get; set; }

		public long ChunkOffset { get; set; }
		
		public string ToXML ()
		{
			string xml = "<" + this.String;
			
			foreach (INode child in this.ChildNodes)
				xml += child.ToXML();
			
			if (this.SelfEnclosed)
				xml += " />";
			else
				xml += "</" + this.String + ">";
			
			return xml;
		}
		
		public long Length 
		{
get; set; 
		}
		#endregion
	}
}

