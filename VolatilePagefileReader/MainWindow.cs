using System;
using Gtk;
using VolatileReader.Pagefile;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public partial class MainWindow: Gtk.Window
{	
	ProgressBar _pulseBar = null;
	VBox _vbox = null;
	TreeView _tv = null;
	string[] _strs = null;
	PageFile _pagefile = null;
	string[] _currentStrs = null;
	Dictionary<string, string> _regexes = null;
	Entry _regx = null;
	ComboBox _regxTitle = null;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		_regexes = new Dictionary<string, string>();

		_regexes.Add("Possible Email Addresses",@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
		_regexes.Add("Possible Environment Variables", @"^[A-Za-z]{3,}=[A-Za-z]+");
		_regexes.Add("Possible File Paths", @"[A-Za-z]:\\");
		_regexes.Add("Possible URLS", @"/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/");
		_regexes.Add("Custom", string.Empty);

		Build ();
		this.Title = "VolatileMinds Pagefile Analyzer";
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

	protected void OpenFile(object sender, EventArgs e)
	{
		FileChooserDialog fc = new FileChooserDialog ("Choose the directory containing the hives to open",
		                                              this,
		                                              FileChooserAction.Open,
		                                              "Cancel", ResponseType.Cancel,
		                                              "Open", ResponseType.Accept);
		
		if (fc.Run () == (int)ResponseType.Accept) {
			string filename = fc.Filename;
			fc.Destroy();

			_currentStrs = null;

			new Thread(new ThreadStart( delegate {
				_pagefile = new PageFile(filename);
				_strs = _pagefile.GetASCIIStrings(6);
				_currentStrs = _pagefile.GetPossibleEnvironmentVariables(_strs, 10);
			})).Start();

			new Thread(new ThreadStart( delegate {
					Populate ();
			})).Start ();
		}
		else fc.Destroy();
	}


	protected void Populate()
	{
		this.SetDefaultSize(1024,768);
		Window pingpong = new Window(Gtk.WindowType.Toplevel);
		pingpong.SetDefaultSize(500,50);
		pingpong.SetPosition(Gtk.WindowPosition.CenterOnParent);
		pingpong.Title = "Loading...";
		_pulseBar = new ProgressBar();
		pingpong.Add(_pulseBar);
		pingpong.ShowAll();
		pingpong.Show();
		
		while (_currentStrs == null)
		{
			Application.Invoke( delegate {
				_pulseBar.Pulse();
			});

			Thread.Sleep(100);
		}

		Application.Invoke( delegate {
			pingpong.Destroy();

			this.Remove(_vbox);
			_vbox = new VBox(false, 10);

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

			_vbox.PackStart(bar, false, false, 0);

			_regxTitle = ComboBox.NewText();
			_regxTitle.Changed += HandleChanged;

			foreach (KeyValuePair<string, string> pair in _regexes)
				_regxTitle.AppendText(pair.Key);

			_regx = new Entry();
			_regx.IsEditable = false;
			_regx.CanFocus = false;
	
			HBox comboRegexBox = new HBox(true, 10);
			comboRegexBox.SetSizeRequest(768, 50);

			comboRegexBox.PackStart(_regxTitle, false,false, 0);
			comboRegexBox.PackStart(_regx, true, true, 0);

			Button search = new Button("Search!");
			search.Clicked += HandleClicked;
			comboRegexBox.PackStart(search, false, false, 20);
				
			_vbox.PackStart(comboRegexBox, true, true, 10);
			ScrolledWindow sw = new ScrolledWindow();
			_tv = new TreeView();
			sw.Add(_tv);

			CellRendererText tsText  = new CellRendererText();
			TreeViewColumn match = new TreeViewColumn();
			match.Title = "Match";
			match.PackStart(tsText, true);
			match.AddAttribute(tsText, "text", 0);
			
			_tv.AppendColumn(match);

			TreeStore store = new TreeStore(typeof(string));
			
			foreach (string str in _currentStrs)
				store.AppendValues(str);
			
			_tv.Model = store;
			sw.SetSizeRequest(768,600);
			_vbox.PackStart(sw, false, false, 0);
			this.Add(_vbox);
			this.ShowAll();
		});
	}

	void HandleChanged (object sender, EventArgs e)
	{
		ComboBox box = sender as ComboBox;
		TreeIter iter;
		box.GetActiveIter(out iter);

		string sel = (string)box.Model.GetValue(iter, 0);

		if (sel == "Custom")
		{
			_regx.CanFocus = true;
			_regx.IsEditable = true;
			_regx.Text = string.Empty;
		}
		else
		{
			_regx.CanFocus = false;
			_regx.IsEditable = false;
			_regx.Text = _regexes[sel];
		}
	}

	private string[] GetMatches(string regex, int minLength)
	{
		System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(regex);
		
		List<string> vars = new List<string>();
		foreach(string str in _strs)
			if (str.Length > minLength && r.IsMatch(str))
				vars.Add(str);
		
		return vars.OrderByDescending(s => s.Length).ToArray();
	}

	void HandleClicked (object sender, EventArgs e)
	{
		_currentStrs = GetMatches(_regx.Text, 10);
		Populate();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
