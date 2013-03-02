using System;
using Gtk;
using VolatileReader.Evtx;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		new EventLog("/media/2936fc27-5d5c-4643-9c84-21bd8e3e0b8e/Backup/aha/Application.evtx");
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
