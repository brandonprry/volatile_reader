using System;
using VolatileReader.Registry;
using Gtk;

namespace VolatileRegistryReader
{
	public partial class NodeKeyInfo : Gtk.Window
	{
		public NodeKeyInfo (NodeKey key) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

			int childNodes = key.ChildNodes.Count;
			int values = key.ChildValues.Count;
			byte[] classnameData = key.ClassnameData;
			string name = key.Name;
			DateTime timestamp = key.Timestamp;

			Label nameLabel = new Label("Name: " + name);
			Label timestampLabel = new Label("Timestamp: " + timestamp.ToLongDateString());
			Label childNodesLabel = new Label("Child nodes: " + childNodes);
			Label valueCountLabel = new Label("Values: " + values);
			//Label data = new Label(BitConverter.ToString(classnameData).Replace('-', ' '));

			VBox box = new VBox();
			box.PackStart(nameLabel, false, false, 30);
			box.PackStart(timestampLabel, false, false, 30);
			box.PackStart(childNodesLabel, false, false, 30);
			box.PackStart(valueCountLabel, false, false, 30);
			//data.Wrap = true;
			//box.PackStart(data, false, false, 0);

			this.Add(box);

			this.ShowAll();

		}
	}
}

