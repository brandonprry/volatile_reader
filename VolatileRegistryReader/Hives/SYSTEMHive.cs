using System;
using VolatileReader.Registry;

namespace VolatileRegistryReader
{
	public class SYSTEMHive : RegistryHive
	{
		RegistryHive _hive = null;
		public SYSTEMHive (string filename) : base(filename)
		{
			_hive = this;
		}

		public SYSTEMHive (RegistryHive hive)
		{
			_hive = hive;
			base.Filepath = this.Filepath;
			base.RootKey = this.RootKey;
			base.WasExported = this.WasExported;
		}

		public string GetBootKey()
		{
			return string.Empty;
		}
		
		public string Filepath {
			get { return _hive.Filepath; }
			set { _hive.Filepath = value; }
		}
		
		public NodeKey RootKey {
			get { return _hive.RootKey; }
		}
		
		public bool WasExported {
			get { return _hive.WasExported; }
			set { _hive.WasExported = value; }
		}
	}
}

