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
using System.ComponentModel;
using System.Linq.Expressions;

namespace Flexpressions.Interfaces
{
	/// <summary>
	/// The IFlexpression exposes non-generic functionality to all levels of the Flexpression tree.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IFlexpression : IExpressionWrapper
	{
		/// <summary>
		/// Gets whether or not to allow outer variables in expressions.
		/// </summary>
		/// <returns>True - outer variables are allowed; False - outer variables will throw exceptions.</returns>
		bool AllowOuterVariables();
		/// <summary>
		/// Gets whether or not to allow a rethrow of an exception.
		/// </summary>
		/// <returns>True - rethrows are allowed; False - rethrows are not allowed.</returns>
		bool AllowRethrow();
		/// <summary>
		/// Declares a new <see cref="LabelTarget"/> on the parent <see cref="Flexpression&lt;S&gt;"/> object.
		/// </summary>
		/// <param name="labelTarget">The <see cref="LabelTarget"/> to add to the parent <see cref="Flexpression&lt;S&gt;"/> object.</param>
		/// <exception cref="ArgumentException">When the name of the <paramref name="labelTarget"/> has already been declared, the exception is thrown.</exception>
		void DeclareLabelTarget(LabelTarget labelTarget);
		/// <summary>
		/// Gets all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.
		/// </summary>
		/// <returns>A collection of all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.</returns>
		IEnumerable<LabelTarget> GetLabelTargets();
		/// <summary>
		/// Gets the <see cref="LabelTarget"/> of the innermost loop closest to the current instance.
		/// </summary>
		/// <param name="startOfLoop">If set to <c>true</c>, the <see cref="LabelTarget"/> representing the start is returned; otherwise, the end is returned.</param>
		/// <returns>A <see cref="LabelTarget"/> representing either the start or end of a loop.</returns>
		/// <exception cref="InvalidOperationException">If a call to this method is made when not within a loop, the exception is thrown.</exception>
		LabelTarget GetLoopLabel(bool startOfLoop);
		/// <summary>
		/// Gets the <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.
		/// </summary>
		/// <returns>The <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.</returns>
		LabelExpression GetReturnLabel();
		/// <summary>
		/// Gets all variables (and parameters) up through the Flexpression tree.
		/// </summary>
		/// <returns>The collection of variables (and parameters) up through the Flexpression tree.</returns>
		IEnumerable<ParameterExpression> GetVariablesInScope();
	}
}