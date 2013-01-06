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

			int childNodes = key.ChildNodes != null ? key.ChildNodes.Count : 0;
			int values = key.ChildValues != null ? key.ChildValues.Count : 0;
			byte[] classnameData = key.ClassnameData != null ? key.ClassnameData : new byte[] {};
			string name = key.Name != null ? key.Name : string.Empty;
			DateTime timestamp = key.Timestamp != null ? key.Timestamp : DateTime.MinValue;

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

