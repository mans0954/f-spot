// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GLib {

	using System;

#region Autogenerated code
	public interface AsyncResult : GLib.IWrapper {

		IntPtr UserData { 
			get;
		}
		GLib.Object SourceObject { 
			get;
		}
	}

	[GLib.GInterface (typeof (AsyncResultAdapter))]
	public interface AsyncResultImplementor : GLib.IWrapper {

		IntPtr UserData { get; }
		GLib.Object SourceObject { get; }
	}
#endregion
}