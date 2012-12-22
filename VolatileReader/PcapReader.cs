using System;

namespace VolatileReader
{
	public partial class PcapReader : Gtk.Window
	{
		public PcapReader () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

