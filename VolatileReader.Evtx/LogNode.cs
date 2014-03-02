using System;
using System.IO;
using System.Collections.Generic;

namespace VolatileReader.Evtx
{
	public static class LogNode
	{
		public static INode NewNode (BinaryReader log, INode parent, long chunkOffset, LogRoot root)
		{	
			long pos = log.BaseStream.Position;
			byte op = log.ReadByte();	
			int flags = op >> 4;
			
			//This is important, you will come across bytes in the hex editor
			//that start a node with BA, BC, etc and this is what will "fix" them
			op = (byte)(op & 0x0f);
			
			Console.WriteLine("New subnode of type " + op + " at offset: " + ((log.BaseStream.Position - chunkOffset)-1));
			
			INode node = null;
			if (op == 0x0f)
				node = new _x0F (log, chunkOffset, root, parent);
			else if (op == 0x0a)
				node = new _x0A (log, parent, root, chunkOffset);
			else if (op == 0x0c)
				node = new _x0C(log, chunkOffset, root, parent);
			else if (op == 0x01)
				node = new _x01(log, chunkOffset, flags, root, parent);
			else if (op == 0x02)
				node = new _x02(log, chunkOffset, root, parent);
			else if (op == 0x03)
				node = new _x03(log, chunkOffset, root, parent);
			else if (op == 0x04)
				node = new _x04(log, chunkOffset, root, parent);
			else if (op == 0x07)
				node = new _x07(log, chunkOffset, root, parent);
			else if (op == 0x06)
				node = new _x06(log, chunkOffset, root, parent);
			else if (op == 0x05)
				node = new _x05(log, chunkOffset, root, parent);
			else if (op == 0x0b)
				node = new _x0B(log, chunkOffset, root, parent);
			else if (op == 0x0d)
				node = new _x0D(log, chunkOffset, root, parent);
			else if (op == 0x0e)
				node = new _x0E(log, chunkOffset, root, parent);
			else if (op == 0x00)
				node = new _x00(log, chunkOffset, root, parent);
			else
				throw new Exception(String.Format("Don't know op: 0x{0:x2} at position {1}", op, log.BaseStream.Position-1));
			
			return node;
		}
	}
}

