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
using System.Diagnostics;
using System.Linq.Expressions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// Wraps an <see cref="Expression"/> to unify how Flexpression objects are treated with <see cref="Expression"/>s.
	/// </summary>
	internal sealed class ExpressionWrapper : IExpressionWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExpressionWrapper" /> class.
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to wrap.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null, the exception is thrown.</exception>
		public ExpressionWrapper(Expression expression)
		{
			Debug.Assert((expression != null), "The expression argument cannot be null.");

			this.expression = expression;
		}

		/// <summary>
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s provided will throw an exception if non-null.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <exception cref="ArgumentException">When <paramref name="trailingExpressions"/> is not null, the exception is thrown.</exception>
		public Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null)
		{
			Debug.Assert((trailingExpressions == null), "The trailingExpressions argument must be null.");

			return this.expression;
		}

		private readonly Expression expression;
	}
}