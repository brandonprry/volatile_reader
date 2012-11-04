using System;
using System.IO;

namespace VolatileReader.Evtx
{
	public static class LogType
	{
		public static IType NewType(BinaryReader log, int[] sizetype, long chunkOffset)
		{
			int size = sizetype[0];
			int type = sizetype[1];
			
			if (type == 0x00)
				return new Type0x00(log, size);
			else if (type == 0x01)
				return new Type0x01(log, size);
			else if (type == 0x02)
				return new Type0x02(log, size);
			else if (type == 0x03)
				return new Type0x03(log, size);
			else if (type == 0x04)
				return new Type0x04(log, size);
			else if (type == 0x05)
				return new Type0x05(log, size);
			else if (type == 0x06)
				return new Type0x06(log, size);
			else if (type == 0x08)
				return new Type0x08(log, size);
			else if (type == 0x09)
				return new Type0x09(log, size);
			else if (type == 0x0A)
				return new Type0x0A(log, size);
			else if (type == 0x0B)
				return new Type0x0B(log, size);
			else if (type == 0x0C)
				return new Type0x0C(log, size);
			else if (type == 0x0D)
				return new Type0x0D(log, size);
			else if (type == 0x0E)
				return new Type0x0E(log, size);
			else if (type == 0x0F)
				return new Type0x0F(log, size);
			else if (type == 0x10)
				return new Type0x10(log, size);
			else if (type == 0x11)
				return new Type0x11(log, size);
			else if (type == 0x12)
				return new Type0x12(log, size);
			else if (type == 0x13)
				return new Type0x13(log, size);
			else if (type == 0x14)
				return new Type0x14(log, size);
			else if (type == 0x15)
				return new Type0x15(log, size);
			else if (type == 0x21)
				return new Type0x21(log, size, chunkOffset);
			else if (type == 0x81)
				return new Type0x81(log, size);
			else if (type == 0x83)
				return new Type0x83(log, size);
			else if (type == 0x84)
				return new Type0x84(log, size);
			else if (type == 0x85)
				return new Type0x85(log, size);
			else if (type == 0x86)
				return new Type0x86(log, size);
			else if (type == 0x87)
				return new Type0x87(log, size);
			else if (type == 0x88)
				return new Type0x88(log, size);
			else if (type == 0x89)
				return new Type0x89(log, size);
			else if (type == 0x8A)
				return new Type0x8A(log, size);
			else if (type == 0x8B)
				return new Type0x8B(log, size);
			else if (type == 0x8C)
				return new Type0x8C(log, size);
			else if (type == 0x8F)
				return new Type0x8F(log, size);
			else if (type == 0x91)
				return new Type0x91(log, size);
			else if (type == 0x92)
				return new Type0x92(log, size);
			else if (type == 0x94)
				return new Type0x94(log, size);
			else if (type == 0x95)
				return new Type0x95(log, size);
			else
				throw new Exception("Don't know type: " + type + " at offset: " + (log.BaseStream.Position - 1));
		}
	}
}

