//
// FilterSet.cs
//
// Author:
//   Larry Ewing <lewing@novell.com>
//   Ruben Vermeersch <ruben@savanne.be>
//
// Copyright (C) 2006-2010 Novell, Inc.
// Copyright (C) 2006 Larry Ewing
// Copyright (C) 2010 Ruben Vermeersch
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

/*
 * Filters/FilterSet.cs
 *
 * Authors:
 *   Larry Ewing <lewing@novell.com>
 *   Stephane Delcroix <stephane@delcroix.org>
 *
 * I don't like per file copyright notices.
 */

using System.Collections;

namespace FSpot.Filters {
	public class FilterSet : IFilter {
		public ArrayList list;

		public FilterSet () {
			list = new ArrayList ();
		}

		public void Add (IFilter filter)
		{
			list.Add (filter);
		}

		public bool Convert (FilterRequest req)
		{
			bool changed = false;
			foreach (IFilter filter in list) {
				changed |= filter.Convert (req);
			}
			return changed;
		}
	}
}
