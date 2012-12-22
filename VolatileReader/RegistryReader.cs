using System;

namespace VolatileReader
{
	public partial class RegistryReader : Gtk.Window
	{
		public RegistryReader () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

