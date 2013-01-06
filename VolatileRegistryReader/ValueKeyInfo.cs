using System;
using VolatileReader.Registry;
using Gtk;
using System.Collections.Generic;

namespace VolatileRegistryReader
{
	public partial class ValueKeyInfo : Gtk.Window
	{
		public ValueKeyInfo (ValueKey key) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.SetSizeRequest(300,200);
			VBox vbox = new VBox(false, 10);

			Label name = new Label("Name: " + key.Name);

			if (key.ValueType == 1 && key.Data != null)
				this.String = System.Text.Encoding.Unicode.GetString(key.Data);
			else if (key.ValueType == 2 && key.Data != null)
				this.String = System.Text.Encoding.Unicode.GetString(key.Data);
			else if (key.ValueType == 3 && key.Data != null)
				this.String = BitConverter.ToString(key.Data).Replace('-', ' ');
			else if (key.ValueType == 4 && key.Data != null)
				this.String = BitConverter.ToString(key.Data).Replace('-', ' ');
			else if (key.ValueType == 7 && key.Data != null)
			{
				List<string> strings = new List<string>();
				List<byte> bytes = new List<byte>();
				
				foreach (byte b in key.Data)
				{
					bytes.Add(b);
					
					if (b == 0x00)
					{
						strings.Add(System.Text.Encoding.Unicode.GetString(bytes.ToArray()));
						bytes = new List<byte>();
					}
				}
				
				this.String = string.Empty;
				foreach (string str in strings)
					this.String += str + "\n";
			}

			Label data = new Label(this.String.Trim());

			vbox.PackStart(name, false, false, 0);
			data.Wrap = true;
			data.Justify = Justification.Fill;
			vbox.PackStart(new Label("Data type: " + key.ValueType), false, false, 0);
			vbox.PackStart(data, true, true, 0);

			this.Add(vbox);
			this.ShowAll();
		}
		
		
		public string String { get; set; }
	}
}

