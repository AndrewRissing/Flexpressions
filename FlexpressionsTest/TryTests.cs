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
using System.Linq.Expressions;
using Flexpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="Try&lt;TParent&gt;"/>.
	/// </summary>
	[TestClass]
	public class TryTests
	{
		/// <summary>
		/// Performs a simple summation to test general functionality.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TryWithNothingElse()
		{
			var e = Flexpression<Func<IEnumerable<int>, int>>
				.Create()
					.Try()
						.Act(() => Console.WriteLine("Fail!"))
					.End()
					.EndTry()
				.End()
				.Compile();
		}
		/// <summary>
		/// Tests to ensure that successive layers of try/catch blocks properly execute.
		/// </summary>
		[TestMethod]
		public void TryLayeredCatch()
		{
			Flexpression<Func<string>>
				.Create()
					.Set<string>("trace", () => string.Empty)
					.Try()
						.Try()
							.Try()
								.Set<string, string>("trace", trace => trace + "a")
								.Throw(() => new ArgumentNullException("b"))
							.Catch<ArgumentNullException>("ex")
								.Set<string, ArgumentNullException, string>("trace", (trace, ex) => trace + ex.ParamName)
								.Throw()
							.Finally()
								.Set<string, string>("trace", trace => trace + "c")
								.End()
							.End()
						.Catch<ArgumentException>("ex2")
							.Set<string, ArgumentException, string>("trace", (trace, ex2) => trace + ex2.ParamName)
							.Throw(() => new NotImplementedException())
						.EndTry()
						.End()
					.Catch()
						.Set<string, string>("trace", trace => trace + "d")
						.End()
					.EndTry()
					.Return<string, string>(trace => trace)
				.CreateLambda()
				.TestExpression("abcbd");
		}
		/// <summary>
		/// Tests to ensure that the argument-less throw is only allowed within a catch block.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TryWithRethrowNotInCatch()
		{
			var e = Flexpression<Func<IEnumerable<int>, int>>
				.Create()
					.Try()
						.Throw()
					.Catch()
						.End()
					.EndTry()
				.End()
				.Compile();
		}
		/// <summary>
		/// Tests a valid rethrow within a catch block.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(MulticastNotSupportedException))]
		public void TryWithRethrowInCatch()
		{
			var e = Flexpression<Action>
				.Create()
					.Try()
						.Throw(() => new MulticastNotSupportedException())
					.Catch()
						.Throw()
					.EndTry()
				.End()
				.Compile();

			e();
		}
		/// <summary>
		/// Tests a catch block with a specific exception.
		/// </summary>
		[TestMethod]
		public void TryWithCatchSpecificException()
		{
			Flexpression<Func<bool>>
				.Create()
					.Try()
						.Throw(() => new MulticastNotSupportedException())
					.Catch<MulticastNotSupportedException>()
						.Return<bool>(() => true)
					.EndTry()
				.Return<bool>(() => false)
				.CreateLambda()
				.TestExpression(true);
		}
		/// <summary>
		/// Tests a catch block that declares a variable name that already exists.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TryWithCatchDuplicateVariable()
		{
			Flexpression<Func<bool>>
				.Create()
					.Declare<Exception>("ex")
					.Try()
						.Act(() => Console.WriteLine("ERROR"))
						.End()
					.Catch<MulticastNotSupportedException>("ex")
						.Return<bool>(() => false)
					.EndTry()
				.Return<bool>(() => false)
				.Compile();
		}
		/// <summary>
		/// Tests a try block that performs a rethrow.
		/// </summary>
		[TestMethod]
		public void TryWithRethrow()
		{
			Flexpression<Func<bool>>
				.Create()
					.Try()
						.Throw(() => new Exception())
					.Catch()
						.Try()
							.Throw()
						.Catch()
							.End()
						.EndTry()
						.End()
					.EndTry()
				.Return<bool>(() => true)
				.CreateLambda()
				.TestExpression(true);
		}
		/// <summary>
		/// Tests a try block with just a catch block.
		/// </summary>
		[TestMethod]
		public void TryWithJustCatchBlock()
		{
			Flexpression<Func<int>>
				.Create()
					.Set<int>("flag", () => 1)
					.Try()
						.Set<int>("flag", () => 2)
						.End()
					.Catch()
						.Set<int>("flag", () => 3)
						.End()
					.EndTry()
				.Return<int, int>(flag => flag)
				.CreateLambda()
				.TestExpression(2);
		}
		/// <summary>
		/// Tests a try block with just a finally block.
		/// </summary>
		[TestMethod]
		public void TryWithJustFinallyBlock()
		{
			Flexpression<Func<int>>
				.Create()
					.Set<int>("flag", () => 1)
					.Try()
						.Set<int>("flag", () => 2)
						.End()
					.Finally()
						.Set<int>("flag", () => 3)
						.End()
				.Return<int, int>(flag => flag)
				.CreateLambda()
				.TestExpression(3);
		}
		/// <summary>
		/// Tests a try block's CreateExpression without a catch/finally block.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TryCreateExpressionWithoutCatchFinally()
		{
			var tryBlock = Flexpression<Func<int>>
				.Create()
					.Try()
						.End();

			tryBlock.CreateExpression(null);
		}
		/// <summary>
		/// Exercises various methods on Try to ensure input is validated.
		/// </summary>
		[TestMethod]
		public void TryInputValidation()
		{
			var tryBlock = Flexpression<Action<int[]>>.Create().Try().End();

			tryBlock.ValidateInputCase
			(
				new InputCase("Catch", typeof(ArgumentException), new object[] { null }),
				new InputCase("Catch", typeof(ArgumentException), new object[] { "" }),
				new InputCase("Catch", typeof(ArgumentException), new object[] { "     " }),
				new InputCase("CreateExpression", typeof(ArgumentException), new object[] { Enumerable.Empty<Expression>() }),
				new InputCase("DeclareLabelTarget", typeof(ArgumentNullException), new object[] { null })
			);
		}
	}
}