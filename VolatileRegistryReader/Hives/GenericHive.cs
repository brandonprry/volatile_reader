using System;
using VolatileReader.Registry;

namespace VolatileRegistryReader
{
	public class GenericHive : RegistryHive
	{
		RegistryHive _hive = null;
		public GenericHive (string filename) : base (filename)
		{
			_hive = this;
		}
		
		public GenericHive (RegistryHive hive)
		{
			_hive = hive;
			base.Filepath = this.Filepath;
			base.RootKey = this.RootKey;
			base.WasExported = this.WasExported;
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

