using System;

namespace VolatileReader
{
	public partial class LegacyEventLog : Gtk.Window
	{
		public LegacyEventLog () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

