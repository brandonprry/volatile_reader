using System;
using Gtk;
using VolatileReader.Registry;
using System.IO;
using VolatileReader.Evt;
using VolatileReader.Evtx;
using VolatileReader.Pcap;
using VolatileReader.Pagefile;
using System.Collections.Generic;

namespace VolatileReader
{
	public partial class VolatileReader : Gtk.Window
	{
		VBox _vbox;
		TreeView _tv;
		Entry _searchBox;
		string _lastFileName;
		
		public VolatileReader () : 
				base("Volatile Reader")
		{
			this.Build ();
			SetDefaultSize(800,600);
			SetPosition(Gtk.WindowPosition.Center);
			DeleteEvent += delegate(object o, DeleteEventArgs args) {
				Application.Quit();
			};
			
			_vbox = new VBox(true, 10);
			
			HBox hbox = new HBox(true, 10);
			_vbox.PackStart(hbox, true, true, 10);
			
			hbox = new HBox();
			
			Button registry = new Button("Open Registry Hives");
			Button eventLogs = new Button("Open Event Logs");
			Button pcap = new Button("    Open Pcap File    ");
			Button hardDisk = new Button("Analyze Hard Disk");
			
			hbox.PackStart(registry, true, true, 10);
			hbox.PackStart(eventLogs, true, true, 10);
			
			_vbox.PackStart(hbox, true, true, 10);
			
			hbox = new HBox(true, 10);
			
			hbox.PackStart(pcap, true, true, 10);
			hbox.PackStart(hardDisk, true, true, 10);
			
			_vbox.PackStart(hbox, true, true, 10);
			
			_vbox.PackStart(new HBox(true, 10), true, true, 10);
			
			this.Add(_vbox);
			this.ShowAll();
		}
		
		private void AddChildrenToView(NodeKey key, TreeStore store, TreeIter iter)
		{
			if (key.ChildValues != null)
				foreach (ValueKey val in key.ChildValues)
					store.AppendValues(iter, val.Name, val.String);
			
			if (key.ChildNodes != null)
			{
				foreach (NodeKey node in key.ChildNodes)
				{	
					TreeIter child = store.AppendValues(iter, node.Name, "");
					AddChildrenToView(node, store, child);
				}
			}
		}
	}
}

