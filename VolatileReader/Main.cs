using System;
using Gtk;

namespace VolatileReader
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			VolatileReader win = new VolatileReader ();
			win.Show ();
			Application.Run ();
		}
	}
}
