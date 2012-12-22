using System;

namespace VolatileReader
{
	public partial class PagefileReader : Gtk.Window
	{
		public PagefileReader () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

