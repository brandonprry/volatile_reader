using System;

namespace VolatileReader.Evtx
{
	public interface IType
	{
		int Length { get; set; }
		
		string String { get; set; }
	}
}

