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
using System.Linq.Expressions;
using Flexpressions.Utilities;

namespace Flexpressions.Extensions
{
	/// <summary>
	/// Contains extensions for the <see cref="Expression"/> class.
	/// </summary>
	public static class ExpressionExtensions
	{
		/// <summary>
		/// Converts the <see cref="Expression"/> to equivalent C# code without modifying the provided expression.
		/// </summary>
		/// <param name="expression">The expression to convert, which will not be modified.</param>
		/// <param name="fullyQualifyTypes">If set to <c>true</c>, all types will be fully qualified.</param>
		/// <returns>The string representation of the <see cref="Expression"/> in C# code.</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="expression"/> is null, the exception is thrown.</exception>
		public static string ToCSharpString(this Expression expression, bool fullyQualifyTypes = false)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return new ExpressionConverter(fullyQualifyTypes).ToCSharpString(expression);
		}
		/// <summary>
		/// Rewrites the <see cref="Expression"/> using the variables in scope.
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to rewrite.</param>
		/// <param name="parameters">The collection of valid <see cref="ParameterExpression"/>s.</param>
		/// <param name="allowOuterVariables">Indicates whether or not outer variables (i.e. captured variables) are allowed.</param>
		/// <returns>The rewritten expression.</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="expression"/> or <paramref name="parameters"/> is null, the exception is thrown.</exception>
		public static Expression RewriteExpression(this Expression expression, IEnumerable<ParameterExpression> parameters, bool allowOuterVariables = false)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");
			if (parameters == null)
				throw new ArgumentNullException("parameters");

			return new ExpressionRewriter(parameters, allowOuterVariables).Visit(expression);
		}
	}
}