/*
 * FSpot.Extensions.ComplexMenuItemNode
 *
 * Author(s)
 * 	Stephane Delcroix  <stephane@delcroix.org>
 *
 * This is free software. See COPYING for details.
 *
 */

using Mono.Addins;
using FSpot.Widgets;
using System;

namespace FSpot.Extensions
{
	[ExtensionNode ("ComplexMenuItem")]
	public class ComplexMenuItemNode : MenuNode
	{
		[NodeAttribute]
		string widget_type;

		[NodeAttribute]
		string command_type;

		public event EventHandler Changed;

		ICommand cmd;

		public override Gtk.MenuItem GetMenuItem ()
		{
			Console.WriteLine ("POOOOOOOOOOONG");
			ComplexMenuItem item = System.Activator.CreateInstance (Type.GetType (widget_type)) as ComplexMenuItem;
			cmd = (ICommand) Addin.CreateInstance (command_type);
			
			if (item != null)
				item.Changed += OnChanged;
			return item;
		}

		private void OnChanged (object o, EventArgs e)
		{
			if (cmd != null)
				cmd.Run (o, e);
		}
	}

}
