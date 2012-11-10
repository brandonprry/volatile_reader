using System;

namespace VolatileReader.Evtx
{
	public interface IType
	{
		byte Type { get; set; }
		
		int Length { get; set; }
		
		string String { get; set; }
	}
}

