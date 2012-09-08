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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Flexpressions;
using Flexpressions.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest.Utilities
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="ExpressionRewriter"/>.
	/// </summary>
	[TestClass]
	public class ExpressionRewriterTests
	{
		/// <summary>
		/// Uses outer variables when the ExpressionRewriter does not allow them.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ExpressionRewriterFailureWithOuterVariables()
		{
			int i = 42;

			Flexpression<Func<int>>.Create(false)
				.Return<int>(() => i);
		}
		/// <summary>
		/// Ensures that the ExpressionRewriter can succeed if outer variables are used and are present.
		/// </summary>
		[TestMethod]
		public void ExpressionRewriterSuccessWithOuterVariables()
		{
			int i = 42;

			var method = Flexpression<Func<int>>.Create(true)
				.Return<int>(() => i)
				.Compile();

			Assert.AreEqual<int>(i, method());
		}
		/// <summary>
		/// Ensures that the ExpressionRewriter can work with parameters successfully.
		/// </summary>
		[TestMethod]
		public void ExpressionRewriterSuccessWithParameters()
		{
			int i = 42;

			var method = Flexpression<Func<int, int>>.Create(true)
				.Return<int, int>(p1 => p1)
				.Compile();

			Assert.AreEqual<int>(i, method(i));
		}
		/// <summary>
		/// Ensures that the ExpressionRewriter enforces type compatibility.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ExpressionRewriterTypeMismatch()
		{
			int i = 42;

			var method = Flexpression<Func<int, int>>.Create(true)
				.Return<string, int>(p1 => p1.Length)
				.Compile();

			Assert.AreEqual<int>(i, method(i));
		}
		/// <summary>
		/// Ensures that the ExpressionRewriter allows for minor casting to ensure type compatibility.
		/// </summary>
		[TestMethod]
		public void ExpressionRewriterTypeCompatibility()
		{
			var method = Flexpression<Func<StringReader, int>>.Create(true)
				.Return<TextReader, int>(p1 => p1.Peek())
				.Compile();

			Assert.AreEqual<int>((int)'a', method(new StringReader("abc")));
		}
		/// <summary>
		/// Attempts to rewrite an Expression with a non-existant parameter.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ExpressionRewriterNonExistentParameter()
		{
			Flexpression<Func<int>>.Create(true)
				.Return<TextReader, int>(p1 => p1.Peek());
		}
		/// <summary>
		/// Ensures that the ExpressionRewriter will ignore parameters without a name.
		/// </summary>
		[TestMethod]
		public void ExpressionRewriterParameterWithNoName()
		{
			var rewriter = new ExpressionRewriter_Accessor(Enumerable.Empty<ParameterExpression>(), false);
			var parameter = Expression.Parameter(typeof(int));

			Assert.AreSame(parameter, rewriter.VisitParameter(parameter));
		}
		/// <summary>
		/// Ensures that the ExpressionRewriter will propertly handle constants.
		/// </summary>
		[TestMethod]
		public void ExpressionRewriterConstantExercise()
		{
			foreach (var input in new []
			{
				new { Exception = typeof(NotSupportedException), AllowOuterVariables = false, ConstantExpression = Expression.Constant(Guid.NewGuid(), typeof(Guid)) },
				new { Exception = (Type)null, AllowOuterVariables = true, ConstantExpression = Expression.Constant(null, typeof(StringComparer)) },
				new { Exception = (Type)null, AllowOuterVariables = false, ConstantExpression = Expression.Constant(null, typeof(StringComparer)) },
				new { Exception = (Type)null, AllowOuterVariables = true, ConstantExpression = Expression.Constant(Guid.NewGuid(), typeof(Guid)) },
				new { Exception = (Type)null, AllowOuterVariables = false, ConstantExpression = Expression.Constant("abcd", typeof(string)) },
				new { Exception = (Type)null, AllowOuterVariables = true, ConstantExpression = Expression.Constant("abcd", typeof(string)) },
				new { Exception = (Type)null, AllowOuterVariables = false, ConstantExpression = Expression.Constant(null, typeof(string)) },
				new { Exception = (Type)null, AllowOuterVariables = true, ConstantExpression = Expression.Constant(null, typeof(string)) },
				new { Exception = (Type)null, AllowOuterVariables = false, ConstantExpression = Expression.Constant(42, typeof(int)) },
				new { Exception = (Type)null, AllowOuterVariables = true, ConstantExpression = Expression.Constant(42, typeof(int)) },
			})
			{
				var rewriter = new ExpressionRewriter_Accessor(Enumerable.Empty<ParameterExpression>(), input.AllowOuterVariables);

				try
				{
					Assert.AreSame(input.ConstantExpression, rewriter.VisitConstant(input.ConstantExpression));
				}
				catch (Exception e)
				{
					Assert.IsTrue((input.Exception != null) && (input.Exception == e.GetType()));
				}
			}
		}
	}
}