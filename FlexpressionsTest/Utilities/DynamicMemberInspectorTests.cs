//  Flexpressions
//  Copyright © 2012 Andrew Rissing
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights 
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//  of the Software, and to permit persons to whom the Software is furnished to do so, 
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all 
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//  PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
//  FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Flexpressions.Utilities;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest.Utilities
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="DynamicMemberInspector"/>.
	/// </summary>
	[TestClass]
	public class DynamicMemberInspectorTests
	{
		/// <summary>
		/// Exercises the DynamicMemberInspector.
		/// </summary>
		[TestMethod]
		public void DynamicMemberInspectorExercise()
		{
			uint a = 1;
			ushort b = 2;
			ushort c = 3;
			byte d = 4;
			byte e = 5; 
			byte f = 6;
			byte g = 7;
			byte h = 8;
			byte i = 9;
			byte j = 10;
			byte k = 11;

			var guid = new Guid(a, b, c, d, e, f, g, h, i, j, k);
			var dmi = (dynamic)DynamicMemberInspector_Accessor.Wrap(guid);

			Assert.AreEqual<uint>(a, (uint)dmi._a);
			Assert.AreEqual<ushort>(b, (ushort)dmi._b);
			Assert.AreEqual<ushort>(c, (ushort)dmi._c);
			Assert.AreEqual<byte>(d, (byte)dmi._d);
			Assert.AreEqual<byte>(e, (byte)dmi._e);
			Assert.AreEqual<byte>(f, (byte)dmi._f);
			Assert.AreEqual<byte>(g, (byte)dmi._g);
			Assert.AreEqual<byte>(h, (byte)dmi._h);
			Assert.AreEqual<byte>(i, (byte)dmi._i);
			Assert.AreEqual<byte>(j, (byte)dmi._j);
			Assert.AreEqual<byte>(k, (byte)dmi._k);
		}
		/// <summary>
		/// Attempts to operate on a non-existant field using the DynamicMemberInspector.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(RuntimeBinderException))]
		public void DynamicMemberInspectorOnNonExistantField()
		{
			var dmi = (dynamic)DynamicMemberInspector_Accessor.Wrap(Guid.NewGuid());
			var value = dmi._z;
		}
	}
}