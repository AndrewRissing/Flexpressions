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
using System.Collections.Generic;
using Flexpressions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest.Extensions
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="TypeExtensions"/>.
	/// </summary>
	[TestClass]
	public class TypeExtensionsTests
	{
		/// <summary>
		/// Calls the GetFriendlyName with a null argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetFriendlyNameNullArgument()
		{
			TypeExtensions.GetFriendlyName(null);
		}
		/// <summary>
		/// Calls the GetFriendlyName with various types to test the result.
		/// </summary>
		[TestMethod]
		public void GetFriendlyNameAssortedTests()
		{
			var tests = new Dictionary<string, string>
			{
				{ "Int32",																								typeof(int).GetFriendlyName(false) },
				{ "System.Int32",																						typeof(int).GetFriendlyName(true) },
				{ "String",																								typeof(string).GetFriendlyName(false) },
				{ "System.String",																						typeof(string).GetFriendlyName(true) },
				{ "List<T>",																							typeof(List<>).GetFriendlyName(false) },
				{ "System.Collections.Generic.List<T>",																	typeof(List<>).GetFriendlyName(true) },
				{ "List<Int32>",																						typeof(List<int>).GetFriendlyName(false) },
				{ "System.Collections.Generic.List<System.Int32>",														typeof(List<int>).GetFriendlyName(true) },
				{ "Dictionary<List<Int32>,Guid>",																		typeof(Dictionary<List<int>,Guid>).GetFriendlyName(false) },
				{ "System.Collections.Generic.Dictionary<System.Collections.Generic.List<System.Int32>,System.Guid>",	typeof(Dictionary<List<int>,Guid>).GetFriendlyName(true) },
				{ "Dictionary<Dictionary<Int32,String>[],List<Int32[]>>",												typeof(Dictionary<Dictionary<int,string>[],List<int[]>>).GetFriendlyName(false) },
				{ "System.Collections.Generic.Dictionary<System.Collections.Generic.Dictionary<System.Int32,System.String>[],System.Collections.Generic.List<System.Int32[]>>", typeof(Dictionary<Dictionary<int,string>[],List<int[]>>).GetFriendlyName(true) }
			};

			foreach (var k in tests.Keys)
				Assert.AreEqual<string>(k, tests[k]);
		}
	}
}