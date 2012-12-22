using System;

namespace VolatileReader
{
	public partial class LegacyEventLogReader : Gtk.Window
	{
		public LegacyEventLogReader () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

