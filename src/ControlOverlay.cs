/*
 * ControlBox.cs
 *
 * Copyright 2007 Novell Inc.
 *
 * Author
 *   Larry Ewing <lewing@novell.com>
 *
 * See COPYING for license information.
 */

using System;
using Gtk;
using FSpot.Widgets;
using Cairo;

namespace FSpot {
	public class ControlOverlay : Window {
		Widget host;
		Window host_toplevel;
		bool composited;
		VisibilityType visibility;
		double target_opacity;
		int round = 12;
		Delay hide; 

		public enum VisibilityType
		{
			None,
			Partial,
			Full
		}

		public VisibilityType Visibility {
			get { return visibility; }
			set {
				visibility = value;
				switch (visibility) {
				case VisibilityType.None:
					FadeToTarget (0.0);
					break;
				case VisibilityType.Partial:
					FadeToTarget (0.3);
					break;
				case VisibilityType.Full:
					FadeToTarget (0.7);
					break;
				}
			}

		}

		protected override void OnDestroyed ()
		{
			hide.Stop ();
			base.OnDestroyed ();
		}

		public bool HideControls ()
		{
			int x, y;
			Gdk.ModifierType type;
			
			if (IsRealized) {
				GdkWindow.GetPointer (out x, out y, out type);
				if (Allocation.Contains (x, y)) {
					hide.Start ();
					return true;
				}
			}

			hide.Stop ();
			Hide ();
			Visibility = VisibilityType.None;
			return false;
		}
		
		public ControlOverlay (Gtk.Widget host) : base (WindowType.Popup)
		{
			this.host = host;
			Decorated = false;
			DestroyWithParent = true;
			
			host_toplevel = (Gtk.Window) host.Toplevel;
			
			TransientFor = host_toplevel;

			this.ModifyFg (Gtk.StateType.Normal, new Gdk.Color (127, 127, 127));
			this.ModifyBg (Gtk.StateType.Normal, new Gdk.Color (0, 0, 0));

			host_toplevel.ConfigureEvent += HandleHostConfigure;
			host_toplevel.SizeAllocated += HandleHostSizeAllocated;
			
			AddEvents ((int) (Gdk.EventMask.PointerMotionMask));
			hide = new Delay (2000, new GLib.IdleHandler (HideControls));
		}

		protected virtual void ShapeSurface (Context cr, Cairo.Color color)
		{
			cr.Operator = Operator.Source;
			cr.Source = new SolidPattern (new Cairo.Color (0, 0, 0, 0));
			cr.Paint ();
			cr.Operator = Operator.Over;

			cr.Source = new SolidPattern (color);
			cr.MoveTo (round, 0);
			cr.Arc (Allocation.Width - round, round, round, - Math.PI * 0.5, 0);
			cr.Arc (Allocation.Width - round, Allocation.Height - round, round, 0, Math.PI * 0.5);
			cr.Arc (round, Allocation.Height - round, round, Math.PI * 0.5, Math.PI);
			cr.Arc (round, round, round, Math.PI, Math.PI * 1.5);
			cr.ClosePath ();
			cr.Fill ();			
		}

		bool FadeToTarget (double target)
		{
			CompositeUtils.SetWinOpacity (this, target);
			Visible = target > 0.0;

			if (Visible) {
				hide.Stop ();
				hide.Start ();
			}

			return false;
		}

		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			Gdk.Color c = Style.Background (State);
			Context cr = CairoUtils.CreateContext (GdkWindow);
			
			ShapeSurface (cr, new Cairo.Color (c.Red / ushort.MaxValue,
							   c.Blue / ushort.MaxValue, 
							   c.Green / ushort.MaxValue,
							   0.5));

			((IDisposable)cr).Dispose ();
			return base.OnExposeEvent (args);
		}

		protected override bool OnMotionNotifyEvent (Gdk.EventMotion args)
		{
			this.Visibility = VisibilityType.Full;
			base.OnMotionNotifyEvent (args);
			return false;
		}

		protected override void OnSizeAllocated (Gdk.Rectangle rec)
		{
			base.OnSizeAllocated (rec);
			QueueDraw ();
		}

		private void HandleHostSizeAllocated (object o, SizeAllocatedArgs args)
		{
			Relocate ();
		}

		private void HandleHostConfigure (object o, ConfigureEventArgs args)
		{
			Relocate ();
		}

		private void Relocate ()
		{
			int x, y;
			if (!host_toplevel.IsMapped)
				return;

			Realize ();
			host_toplevel.GdkWindow.GetOrigin (out x, out y);

			x += (int) (host_toplevel.Allocation.Width * 0.5);
			y += (int) (host_toplevel.Allocation.Height * 0.8);
			
			x -= (int) (Allocation.Width * 0.5);
			Move (x, y);
		}
		
		protected override void OnRealized ()
		{
			bool composited = CompositeUtils.IsComposited (Screen) && CompositeUtils.SetRgbaColormap (this);
			AppPaintable = composited;
			
			base.OnRealized ();

			if (!composited) {
				Gdk.Pixmap bitmap = new Gdk.Pixmap (GdkWindow, 
								    Allocation.Width, 
								    Allocation.Height, 1);
				
				Context cr = CairoUtils.CreateContext (bitmap);
				ShapeSurface (cr, new Cairo.Color (1, 1, 1));
				((IDisposable)cr).Dispose ();
				ShapeCombineMask (bitmap, 0, 0);
				bitmap.Dispose ();
			}
			
			Visibility = VisibilityType.Full;
		}

		public void Dissmiss ()
		{
			
		}
		
		protected override void OnMapped ()
		{
			base.OnMapped ();
			Relocate ();
		}
	}
}
