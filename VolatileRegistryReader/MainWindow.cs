using System;
using System.IO;
using System.Linq;
using Gtk;
using VolatileReader.Registry;
using System.Collections.Generic;
using VolatileRegistryReader;
using System.Threading;

public partial class MainWindow: Gtk.Window
{	
	ProgressBar pulseBar = new ProgressBar();
	Label reading = new Label("");
	VBox _vbox = null;
	HBox _hbox = null;
	TreeView _tv = null;
	List<RegistryHive> _hives = new List<RegistryHive>();
	List<string> _filenames = new List<string>();
	long _currentProgress = 0;
	long _total = 0;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		this.Title = "VolatileMinds Registry Reader";
		SetPosition (Gtk.WindowPosition.Center);
		DeleteEvent += delegate(object o, DeleteEventArgs args) {
			Application.Quit ();
		};
		
		MenuBar bar = new MenuBar ();
		
		Menu fileMenu = new Menu ();
		MenuItem fileMenuItem = new MenuItem ("File");
		fileMenuItem.Submenu = fileMenu;
		
		MenuItem exit = new MenuItem ("Exit");
		exit.Activated += delegate(object sender, EventArgs e) {
			Application.Quit ();
		};
		
		MenuItem openFile = new MenuItem ("Open file...");

		openFile.Activated += OpenFile;
		
		fileMenu.Append (openFile);
		fileMenu.Append (exit);
		
		bar.Append (fileMenuItem);
		
		_vbox = new VBox (false, 2);
		_vbox.PackStart (bar, false, false, 0);
		
		this.Add (_vbox);
		this.ShowAll ();
	}
	
	protected void OpenFile (object sender, EventArgs e)
	{
		FileChooserDialog fc = new FileChooserDialog ("Choose the directory containing the hives to open",
	                        							this,
	                        							FileChooserAction.SelectFolder,
	                        							"Cancel", ResponseType.Cancel,
	                        							"Open", ResponseType.Accept);

		if (fc.Run () == (int)ResponseType.Accept) {
			string dir = fc.Filename;
			fc.Destroy();
			List<Thread> threads = new List<Thread>();
			Window window = new Gtk.Window(Gtk.WindowType.Toplevel);
			VBox progressBox = new VBox(false, 5);
			reading = new Label("Reading hives, please wait...");
			pulseBar = new ProgressBar();
			progressBox.PackStart(reading, true, true, 0);
			progressBox.PackStart(pulseBar, true, true, 0);
			reading.Show();
			pulseBar.Show ();
			progressBox.Show();
			window.Add(progressBox);
			window.SetPosition(Gtk.WindowPosition.CenterOnParent);
			window.SetSizeRequest(500,100);
			window.Title = "Loading...";
			window.Show();
			_hives = new List<RegistryHive>();
			_filenames = new List<string>();
			foreach (var file in System.IO.Directory.GetFiles(dir))
			{
				using (BinaryReader reader = new BinaryReader(File.OpenRead(file)))
				{
					reader.BaseStream.Position = 0;

					if (reader.BaseStream.Length > 4)
					{
						byte[] magic = reader.ReadBytes(4);
						if (magic[0] == 'r' && magic[1] == 'e' && magic[2] == 'g' && magic[3] == 'f')
						{
							_total += reader.BaseStream.Length;
							_filenames.Add(file);
						}
					}
				}
			}

			threads = GetReadThreads ();
			foreach (Thread thread in threads)
				thread.Start();

			new Thread(new ThreadStart(delegate {
				foreach (Thread thread in threads)
				{
					while (thread.IsAlive)
					{
						Application.Invoke(delegate {
							pulseBar.Pulse();
						});
						System.Threading.Thread.Sleep(100); 
					}
				}

				Application.Invoke(delegate {
					window.Destroy();
					Populate();
				});
			})).Start();
		}
		else
			fc.Destroy();
	}

	private List<Thread> GetReadThreads()
	{	
		if (_filenames.Count == 0)
			return null;

		List<Thread> threads = new List<Thread>();
		foreach (var file in _filenames)
		{
			Thread thread = new Thread(new ThreadStart( delegate {
				try
				{
					Console.WriteLine ("Reading: " + file);
					_hives.Add(HiveFactory.GetTypedHive(file));
					_currentProgress += new FileInfo(file).Length;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Couldn't read file: " + file + "\n" + ex.ToString());
				}
			}));
			threads.Add(thread);
		}
		
		return threads;
	}
	
	private void Populate()
	{

		this.Remove(_vbox);
		_vbox = new VBox(false, 3);
		//_vbox.
		MenuBar bar = new MenuBar ();
		
		Menu fileMenu = new Menu ();
		MenuItem fileMenuItem = new MenuItem ("File");
		fileMenuItem.Submenu = fileMenu;
		
		MenuItem exit = new MenuItem ("Exit");
		exit.Activated += delegate(object sender, EventArgs e) {
			Application.Quit ();
		};
		
		MenuItem openFile = new MenuItem ("Open file...");
		openFile.Activated += OpenFile;
		
		fileMenu.Append (openFile);
		fileMenu.Append (exit);
		
		bar.Append (fileMenuItem);
		
		VBox menu = new VBox (false, 2);
		menu.PackStart (bar, false, false, 0);
		_vbox.PackStart(menu, false, false, 0);
		
		_hbox = new HBox();	

		ScrolledWindow sw = new ScrolledWindow();
		_tv= new TreeView();
		sw.Add(_tv);

		_tv.RowActivated += HandleRowActivated;

		CellRendererText tsText  = new CellRendererText();
		TreeViewColumn timestamp = new TreeViewColumn();
		timestamp.Title = "Filename";
		timestamp.PackStart(tsText, true);
		timestamp.AddAttribute(tsText, "text", 0);
		
		CellRendererText dlText = new CellRendererText();
		TreeViewColumn datalength = new TreeViewColumn();
		datalength.Title = "Hive type";
		datalength.PackStart(dlText, true);
		datalength.AddAttribute(dlText, "text", 1);
		
		_tv.AppendColumn(timestamp);
		_tv.AppendColumn(datalength);
		
		TreeStore store = new TreeStore(typeof(string), typeof(string));

		_hives = _hives.OrderBy(h => h.GetType().Name).ToList();

		foreach (RegistryHive hive in _hives)
		{
			string[] strs = hive.Filepath.Split(System.IO.Path.DirectorySeparatorChar);
			store.AppendValues(strs[strs.Length-1], hive.GetType().Name);
		}
		
		_tv.Model = store;
		
		_hbox.PackStart(sw, true, true, 0);
		_vbox.PackStart (_hbox, true, true, 0);
		
		this.Add(_vbox);
		this.ShowAll();
	}

	private void HandleRowActivated (object o, RowActivatedArgs args)
	{
		int row = int.Parse(args.Path.ToString()); //this is silly?
		RegistryReader reader = new RegistryReader(_hives[row]);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
