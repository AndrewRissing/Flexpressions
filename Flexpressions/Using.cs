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
	/// The <see cref="Using&lt;TParent&gt;"/> class encapsulates a using statement.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	internal sealed class Using<TParent> : FluentBase, IExpressionWrapper where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Prevents a default instance of the <see cref="Using&lt;TParent&gt;" /> class from being created.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="variable">The <see cref="ParameterExpression"/> of the <see cref="IDisposable"/> object.</param>
		private Using(TParent parent, ParameterExpression variable)
		{
			this.parent = parent;
			this.variable = variable;
			this.usingBody = new Block<TParent>(this.parent);
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Creates a new <see cref="Using&lt;TParent&gt;" /> representing the using.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="variable">The <see cref="ParameterExpression"/> of the <see cref="IDisposable"/> object.</param>
		/// <param name="usingBody">The body of the using statement to hand back to the calling body to improve the fluent syntax.</param>
		/// <returns>The new <see cref="Using&lt;TParent&gt;" /> representing the using statement.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> or <paramref name="variable"/> is null, the exception is thrown.</exception>
		public static Using<TParent> Create(TParent parent, ParameterExpression variable, out Block<TParent> usingBody)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");
			Debug.Assert((variable != null), "The variable argument cannot be null.");

			var instance = new Using<TParent>(parent, variable);

			usingBody = instance.usingBody;

			return instance;
		}

		/// <summary>
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s provided will throw an exception if non-null.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <exception cref="ArgumentException">When <paramref name="trailingExpressions"/> is not null, the exception is thrown.</exception>
		public Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null)
		{
			Expression finallyBlock, callDispose;

			Debug.Assert((trailingExpressions == null), "The trailingExpressions argument must be null.");

			// The block here performs the following: ((IDisposable)v).Dispose();
			callDispose = Expression.Call
			(
				Expression.Convert(this.variable, typeof(IDisposable)),
				typeof(IDisposable).GetMethod("Dispose", Type.EmptyTypes)
			);

			if (this.variable.Type.IsValueType)
			{
				finallyBlock = callDispose;
			}
			else
			{
				// The block here performs the following:
				//
				// if (v != null)
				//	((IDisposable)v).Dispose();
				//
				finallyBlock = Expression.IfThen
				(
					Expression.NotEqual(this.variable, Expression.Constant(null, this.variable.Type)),
					callDispose
				);
			}

			return Expression.TryFinally(this.usingBody.CreateExpression(), finallyBlock);
		}

		#endregion Public Code

		#region Private Code

		private readonly TParent parent;
		private readonly ParameterExpression variable;
		private readonly Block<TParent> usingBody;

		#endregion Private Code
	}
}