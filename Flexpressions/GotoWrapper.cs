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
using System.Linq;
using System.Linq.Expressions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The GotoWrapper class is used to delay the search of a <see cref="LabelTarget"/> until the point of creation of the expression.
	/// </summary>
	internal sealed class GotoWrapper : IExpressionWrapper
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="GotoWrapper" /> class.
		/// </summary>
		/// <param name="flexpression">The flexpression to retrieve the list of <see cref="LabelTarget"/>s from.</param>
		/// <param name="labelName">The name of the label.</param>
		/// <exception cref="ArgumentNullException">When the <paramref name="flexpression"/> is null, the exception is thrown.</exception>
		/// <exception cref="ArgumentException">When the <paramref name="labelName"/> is null, empty, or just whitespace, the exception is thrown.</exception>
		public GotoWrapper(IFlexpression flexpression, string labelName)
		{
			Debug.Assert((flexpression != null), "The flexpression argument cannot be null.");
			Debug.Assert(!string.IsNullOrWhiteSpace(labelName), "The labelName argument cannot be null, empty, or just whitespace.");

			this.flexpression = flexpression;
			this.labelName = labelName;
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s provided will throw an exception if non-null.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <exception cref="ArgumentException">
		///		<para>When <paramref name="trailingExpressions"/> is not null, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When a label cannot be found, the exception is thrown.</para>
		/// </exception>
		public Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null)
		{
			Debug.Assert((trailingExpressions == null), "The trailingExpressions argument must be null.");

			LabelTarget labelTarget = this.flexpression.GetLabelTargets().FirstOrDefault(x => x.Name == this.labelName);

			if (labelTarget == null)
				throw new ArgumentException(string.Format("Unable to find label target, {0}, referenced by Goto.", this.labelName));

			return Expression.Goto(labelTarget);
		}

		#endregion Public Code

		#region Private Code

		private readonly IFlexpression flexpression;
		private readonly string labelName;

		#endregion Private Code
	}
}