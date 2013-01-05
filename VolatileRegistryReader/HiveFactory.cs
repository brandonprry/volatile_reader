using System;
using VolatileReader.Registry;

namespace VolatileRegistryReader
{
	public static class HiveFactory
	{
		public static RegistryHive GetTypedHive(string filename)
		{
			RegistryHive hive = new RegistryHive(filename);

			try
			{
				foreach (NodeKey key in hive.RootKey.ChildNodes)
				{
					if (key.Name == "Select")
					{
						return new SYSTEMHive(hive);
					}
					else if (key.Name == "SAM")
					{
						return new SAMHive(hive);
					}
					else if (key.Name == "Microsoft")
					{
						return new SOFTWAREHive(hive);
					}
					else if (key.Name == "Policy")
					{
						return new SECURITYHive(hive);
					}
				}
			}
			catch
			{
				return new GenericHive(hive);
			}

			return new GenericHive(hive);
		}
	}
}

