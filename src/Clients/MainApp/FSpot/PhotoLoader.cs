//
// PhotoLoader.cs
//
// Author:
//   Stephane Delcroix <sdelcroix@novell.com>
//   Ruben Vermeersch <ruben@savanne.be>
//   Larry Ewing <lewing@novell.com>
//
// Copyright (C) 2004-2010 Novell, Inc.
// Copyright (C) 2007-2009 Stephane Delcroix
// Copyright (C) 2010 Ruben Vermeersch
// Copyright (C) 2004-2005 Larry Ewing
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

using FSpot.Core;
using FSpot.Imaging;

namespace FSpot
{
	[Obsolete ("nuke or rename this")]
	public class PhotoLoader
	{
		public PhotoQuery query;

		public Gdk.Pixbuf Load (int index)
		{
			return Load (query, index);
		}

		static public Gdk.Pixbuf Load (IBrowsableCollection collection, int index)
		{
			IPhoto item = collection [index];
			return Load (item);
		}

		static public Gdk.Pixbuf Load (IPhoto item)
		{
			using (var img = App.Instance.Container.Resolve<IImageFileFactory> ().Create (item.DefaultVersion.Uri)) {
				Gdk.Pixbuf pixbuf = img.Load ();
				return pixbuf;
			}
		}

		static public Gdk.Pixbuf LoadAtMaxSize (IPhoto item, int width, int height)
		{
			using (var img = App.Instance.Container.Resolve<IImageFileFactory> ().Create (item.DefaultVersion.Uri)) {
				Gdk.Pixbuf pixbuf = img.Load (width, height);
				return pixbuf;
			}
		}

		public PhotoLoader (PhotoQuery query)
		{
			this.query = query;
		}
	}
}
