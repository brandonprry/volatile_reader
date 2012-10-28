using System;
using Gtk;
using VolatileReader.Registry;

namespace VolatileReader
{
	public partial class VolatileReader : Gtk.Window
	{
		VBox _vbox;
		public VolatileReader () : 
				base("Volatile Reader")
		{
			this.Build ();
			SetDefaultSize(250,200);
			SetPosition(Gtk.WindowPosition.Center);
			DeleteEvent += delegate(object o, DeleteEventArgs args) {
				Application.Quit();
			};
			
			MenuBar bar = new MenuBar();
			
			Menu fileMenu  = new Menu();
			MenuItem fileMenuItem = new MenuItem("File");
			fileMenuItem.Submenu = fileMenu;
			
			MenuItem exit = new MenuItem("Exit");
			exit.Activated += delegate(object sender, EventArgs e) {
				Application.Quit();
			};
			
			MenuItem openFile = new MenuItem("Open file...");
			openFile.Activated += OpenFile;
			
			fileMenu.Append(openFile);
			fileMenu.Append(exit);
			
			bar.Append(fileMenuItem);
			
			_vbox = new VBox(false, 2);
			_vbox.PackStart(bar, false, false, 0);
			
			this.Add(_vbox);
			this.ShowAll();
		}
		
		void OpenFile(object sender, EventArgs e)
		{
		    FileChooserDialog fc = new FileChooserDialog("Choose the registry hive or event log to open",
	                        							this,
	                        							FileChooserAction.Open,
	                        							"Cancel",ResponseType.Cancel,
	                        							"Open",ResponseType.Accept);

			if (fc.Run() == (int)ResponseType.Accept) 
			{
				string file = fc.Filename;
				Console.WriteLine("Reading: " + file);
				
				RegistryHive hive = new RegistryHive(file);
				
				TreeView tv = new TreeView();
				_vbox.Add(tv);
				
				TreeViewColumn paths = new TreeViewColumn();
				paths.Title = "Registry Keys";
				
				CellRendererText keyCell = new CellRendererText();
				paths.PackStart(keyCell, true);
				
				TreeViewColumn values = new TreeViewColumn();
				values.Title = "Registry Values";
				
				CellRendererText valuesCell = new CellRendererText();
				values.PackStart(valuesCell, true);
				
				tv.AppendColumn(paths);
				tv.AppendColumn(values);
				
				paths.AddAttribute(keyCell, "text", 0);
				values.AddAttribute(valuesCell, "text", 1);
				
				TreeStore store = new TreeStore(typeof(string), typeof(string));
				
				TreeIter root = store.AppendValues(hive.RootKey.Name);
				
				AddChildrenToView(hive.RootKey, store, root);
				
				tv.Model = store;
				
				this.ShowAll();
			}
			
			fc.Destroy();
			
			
		}
		
		private void AddChildrenToView(NodeKey key, TreeStore store, TreeIter iter)
		{
			if (key.ChildValues != null)
				foreach (ValueKey val in key.ChildValues)
					store.AppendValues(iter, val.Name, BitConverter.ToString(val.Data));
			
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

