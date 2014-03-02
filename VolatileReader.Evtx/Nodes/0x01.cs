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

		public _x01 (BinaryReader log, long chunkOffset, int flags, LogRoot root, INode parent)
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
			long i = 0;
			if (!root.ParentLog.Strings.ContainsKey ((long)(this.ChunkOffset + _ptr))) {
				log.BaseStream.Position = this.ChunkOffset + _ptr;
				
				_next = log.ReadInt32 ();
				_hash = log.ReadInt16 ();
				_length2 = log.ReadInt16 ();
				
				_posdiff = 2;
				this.Length = _length + 6; //6? it works...
				
				_str = log.ReadBytes ((int)(_length2 * 2));
				log.BaseStream.Position += _posdiff + (_addFour ? 4 : 0);
				this.String = root.ParentLog.Strings [this.ChunkOffset + _ptr] = System.Text.Encoding.Unicode.GetString (_str);
				i = this.Length - (11 + (_length2 + 1) * 2 + (_addFour ? 4 : 0));
			} else {
				this.String = root.ParentLog.Strings [this.ChunkOffset + _ptr];
				_length2 = 0;
				log.BaseStream.Position += (_addFour ? 4 : 0);
				i = this.Length - (11 + (_addFour ? 4 : 0));
			}
			
			this.ChildNodes = new List<INode> ();
			i -= 8;
			while (i > 0 && !root.ReachedEOS) {
				Console.WriteLine ("Current length: " + i);
				INode node = LogNode.NewNode (log, this, chunkOffset, this.LogRoot);
				this.ChildNodes.Add (node);
				i -= node.Length;
			}
		}

		public long Position { get; set; }

		public List<INode> ChildNodes { get; set; }

		public string String { get; set; }

		public LogRoot LogRoot { get; set; }

		public bool SelfEnclosed { get; set; }

		public bool IsValue { get; set; }

		public int SubstitutionArray { get; set; }

		#region INode implementation

		public INode Parent { get; set; }

		public long ChunkOffset { get; set; }

		public string ToXML ()
		{
			string xml = "\n<" + this.String;
			
			foreach (INode child in this.ChildNodes)
				xml += child.ToXML ();

			return xml;
		}

		public long Length {
			get;
			set; 
		}

		#endregion
	}
}

