using System;

namespace VolatileReader
{
	public partial class EventLogReader : Gtk.Window
	{
		public EventLogReader () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

