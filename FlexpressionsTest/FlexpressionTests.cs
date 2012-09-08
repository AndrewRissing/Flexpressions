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
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Flexpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="Flexpression&lt;S&gt;"/>.
	/// </summary>
	[TestClass]
	public class FlexpressionTests
	{
		/// <summary>
		/// Calls CreateExpression on an Flexpression block with non-null arguments.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionCreateExpressionWithNonNullArgument()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.CreateExpression(Enumerable.Empty<Expression>());
		}
		/// <summary>
		/// Calls GetLoopLabel on an Flexpression block with a false argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void FlexpressionGetLoopLabelWithFalseArgument()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.GetLoopLabel(false);
		}
		/// <summary>
		/// Calls GetLoopLabel on an Flexpression block with a true argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void FlexpressionGetLoopLabelWithTrueArgument()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.GetLoopLabel(true);
		}
		/// <summary>
		/// Calls DeclareLabelTarget on an Flexpression block with a null name.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionDeclareLabelTargetWithLabelTargetNullName()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.DeclareLabelTarget(Expression.Label(typeof(void), null));
		}
		/// <summary>
		/// Calls DeclareLabelTarget on an Flexpression block with an empty name.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionDeclareLabelTargetWithLabelTargetEmptyName()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.DeclareLabelTarget(Expression.Label(typeof(void), string.Empty));
		}
		/// <summary>
		/// Calls DeclareLabelTarget on an Flexpression block with an whitespace name.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionDeclareLabelTargetWithLabelTargetWhitespaceName()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.DeclareLabelTarget(Expression.Label(typeof(void), "          "));
		}
		/// <summary>
		/// Calls DeclareLabelTarget on an Flexpression block with a duplicate name.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionDeclareLabelTargetWithDuplicateName()
		{
			var flexpression = Flexpression<Action<int[]>>.Create().End();

			flexpression.DeclareLabelTarget(Expression.Label(typeof(void), "a"));
			flexpression.DeclareLabelTarget(Expression.Label(typeof(void), "a"));
		}
		/// <summary>
		/// Constructs a Flexpression from an invalid type.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void FlexpressionWithInvalidType()
		{
			Flexpression<string>.Create();
		}
		/// <summary>
		/// Constructs a Flexpression with too few parameter names.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionCreateWithTooFewParameterNames()
		{
			Flexpression<Action<int, int, int>>.Create(false, "a", "b");
		}
		/// <summary>
		/// Constructs a Flexpression with too few parameter names.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FlexpressionCreateWithTooManyParameterNames()
		{
			Flexpression<Action<int, int, int>>.Create(false, "a", "b", "c", "d");
		}
		/// <summary>
		/// Compiles a Flexpression with a DebugInfoGenerator.
		/// </summary>
		[TestMethod]
		public void FlexpressionCompileWithDebugInfoGenerator()
		{
			var method = Flexpression<Func<int>>
				.Create()
					.Return<int>(() => 234)
				.Compile(null, false, DebugInfoGenerator.CreatePdbGenerator());

			Assert.AreEqual<int>(234, method());
		}
	}
}