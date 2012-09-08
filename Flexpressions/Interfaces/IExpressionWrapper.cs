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

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Flexpressions.Interfaces
{
	/// <summary>
	/// The IExpressionWrapper exposes the ability to create <see cref="Expression"/>s.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IExpressionWrapper
	{
		/// <summary>
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s to add to the end of the newly created <see cref="Expression"/>.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <remarks>
		///		<para>When <paramref name="trailingExpressions"/> is null, the generated expression is unchanged.</para>
		///		<para>- Or -</para>
		///		<para>The <paramref name="trailingExpressions"/> is only provided to the immediate expression and not passed to subsequent children.</para>
		///	</remarks>
		Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null);
	}
}