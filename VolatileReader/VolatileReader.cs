using System;
using Gtk;
using VolatileReader.Registry;
using System.IO;
using VolatileReader.Evt;

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
				
				using (FileStream stream = File.OpenRead(file))
				{
					using (BinaryReader reader = new BinaryReader(stream))
					{
						byte[] h = reader.ReadBytes(10);
						
						if (h[0] == 'r' && h[1] == 'e' && h[2] == 'g' && h[3] == 'f')
						{
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
						}
						else if (h[4] == 'L' && h[5] == 'f' && h[6] ==  'L' && h[7] ==  'e')
						{
							LegacyEventLog log = new LegacyEventLog(file);
							
							TreeView tv = new TreeView();
							_vbox.Add(tv);
							
							CellRendererText twText = new CellRendererText();
							TreeViewColumn timeWritten = new TreeViewColumn();
							timeWritten.Title = "Time Written";
							timeWritten.PackStart(twText, true);
							timeWritten.AddAttribute(twText, "text", 0);
							
							CellRendererText tgText = new CellRendererText();
							TreeViewColumn timeGenerated = new TreeViewColumn();
							timeGenerated.Title = "Time Generated";
							timeGenerated.PackStart(tgText, true);
							timeGenerated.AddAttribute(tgText, "text", 1);
							
							CellRendererText snText = new CellRendererText();
							TreeViewColumn sourceName = new TreeViewColumn();
							sourceName.Title = "Source Name";
							sourceName.PackStart(snText, true);
							sourceName.AddAttribute(snText, "text", 2);
							
							CellRendererText cnText = new CellRendererText();
							TreeViewColumn computerName = new TreeViewColumn();
							computerName.Title = "Computer Name";
							computerName.PackStart(cnText, true);
							computerName.AddAttribute(cnText, "text", 3);
							
							CellRendererText sText = new CellRendererText();
							TreeViewColumn strings = new TreeViewColumn();
							strings.Title = "Strings";
							strings.PackStart(sText, true);
							strings.AddAttribute(sText, "text", 4);
							
							tv.AppendColumn(timeWritten);
							tv.AppendColumn(timeGenerated);
							tv.AppendColumn(sourceName);
							tv.AppendColumn(computerName);
							tv.AppendColumn(strings);
							
							TreeStore store = new TreeStore(typeof(string),typeof(string),typeof(string),typeof(string),typeof(string));
							
							foreach (LogItem item in log.Items)
								store.AppendValues(item.TimeWritten.ToString(), item.TimeGenerated.ToString(), item.SourceName, item.ComputerName, item.Strings);
							
							tv.Model = store;
						}
					}
				}
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

