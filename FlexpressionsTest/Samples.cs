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
using System.Linq;
using Flexpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// The Samples class contains general more real-world type tests to ensure the code behaves properly.
	/// </summary>
	[TestClass]
	public class Samples
	{
		/// <summary>
		/// Performs a simple summation to test general functionality.
		/// </summary>
		[TestMethod]
		public void Summation()
		{
			var input = Enumerable.Range(0, 10);

			Flexpression<Func<IEnumerable<int>, int>>
				.Create()
					.Set<int>("sum", () => 0)
					.Foreach<int, IEnumerable<int>, IEnumerable<int>>("x", p1 => p1)
						.Set<int, int, int>("sum", (x, sum) => x + sum)
					.End()
				.Return<int, int>(sum => sum)
				.CreateLambda()
				.TestExpression(input.Sum(), input);
		}
		/// <summary>
		/// Performs a simple filter to test general functionality.
		/// </summary>
		[TestMethod]
		public void Filter()
		{
			var input = Enumerable.Range(0, 10);

			Flexpression<Func<IEnumerable<int>, List<int>>>
				.Create()
					.Set<List<int>>("even", () => new List<int>())
					.Foreach<int, IEnumerable<int>, IEnumerable<int>>("x", p1 => p1)
						.If<int>(x => x % 2 == 0)
							.Act<List<int>, int>((even, x) => even.Add(x))
							.End()
						.EndIf()
					.End()
				.Return<List<int>, List<int>>(even => even)
				.CreateLambda()
				.TestExpression(input.Where(x => x % 2 == 0).ToList(), input);
		}
	}
}