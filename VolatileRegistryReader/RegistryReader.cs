using System;
using VolatileReader.Registry;
using Gtk;

namespace VolatileRegistryReader
{
	public partial class RegistryReader : Gtk.Window
	{
		TreeStore _store = null;
		TreeIter _root;
		TreeView _tv;
		public RegistryReader (RegistryHive hive) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.SetSizeRequest(1024,768);
			VBox _vbox = new VBox(true, 5);

			ScrolledWindow sw = new ScrolledWindow();
			_tv= new TreeView();
			sw.Add(_tv);
			_vbox.Add(sw);
			
			TreeViewColumn paths = new TreeViewColumn();
			paths.Title = "Registry Keys";

			CellRendererText keyCell = new CellRendererText();
			paths.PackStart(keyCell, true);

			TreeViewColumn obj = new TreeViewColumn();

			_tv.AppendColumn(obj);
			_tv.AppendColumn(paths);
			
			paths.AddAttribute(keyCell, "text", 0);
			
			_store = new TreeStore(typeof(string), typeof(object));
			
			_root = _store.AppendValues(hive.RootKey.Name, hive.RootKey);

			AddChildrenToView(hive.RootKey, _store, _root);
			
			_tv.Model = _store;
			_tv.RowActivated += HandleRowActivated;

			this.Add(_vbox);
			this.ShowAll();
		}

		void HandleRowActivated (object o, RowActivatedArgs args)
		{
			TreeIter it;
			_tv.Selection.GetSelected(out it);
			object ob = _store.GetValue(it, 1);

			if (ob is ValueKey)
			{
				ValueKeyInfo info = new ValueKeyInfo(ob as ValueKey);
				info.Show();
			}
			else if (ob is NodeKey)
			{
				NodeKeyInfo info = new NodeKeyInfo(ob as NodeKey);
				info.Show();
			}
			else
				throw new Exception("Don't know type: " + ob.GetType().ToString());
		}

		private void AddChildrenToView(NodeKey key, TreeStore store, TreeIter iter)
		{
			if (key.ChildValues != null)
				foreach (ValueKey val in key.ChildValues)
					store.AppendValues(iter, val.Name, val);
			
			if (key.ChildNodes != null)
			{
				foreach (NodeKey node in key.ChildNodes)
				{	
					TreeIter child = store.AppendValues(iter, node.Name, node);
					AddChildrenToView(node, store, child);
				}
			}
		}
	}
}

