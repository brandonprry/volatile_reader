using System;
using VolatileReader.Registry;
using System.Collections.Generic;

namespace VolatileRegistryReader
{
	public class SAMHive : RegistryHive
	{
		RegistryHive _hive = null;
		public SAMHive (string filename) : base (filename)
		{
			_hive = this;
		}

		public SAMHive(RegistryHive hive)
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

		public string[] DumpHashes(SYSTEMHive hive)
		{
			string bootkey = hive.GetBootKey();
			string hbootkey = this.GetHBootKey();
			string[] users = this.GetUsers();

			List<string> hashes = new List<string>();
			foreach (string user in users)
			{
			}

			return hashes.ToArray();
		}

		public string[] DumpPasswordHints(SOFTWAREHive hive)
		{
			string[] users = this.GetUsers();

			List<string> hints = new List<string>();
			foreach (string user in users)
			{
			}

			return hints.ToArray();
		}

		public string GetHBootKey()
		{
			return string.Empty;
		}

		public string[] GetUsers()
		{
			return new string[10];
		}
	}
}

