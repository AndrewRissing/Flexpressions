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
using Flexpressions.Extensions;

namespace Flexpressions.Utilities
{
	/// <summary>
	/// The ExpressionRewriter replaces all parameter references in an <see cref="Expression"/> tree to the provided list
	/// based upon the parameter names.
	/// </summary>
	internal sealed class ExpressionRewriter : ExpressionVisitor
	{
		#region Constructor

		/// <summary>
		/// Creates a new instance of the <see cref="ExpressionRewriter"/> class.
		/// </summary>
		/// <param name="sourceParameters">The collection of source parameters to map existing parameters to.</param>
		/// <param name="allowOuterVariables">Indicates whether or not outer variables (i.e. captured variables) are allowed.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="sourceParameters"/> is null, the exception is thrown.</exception>
		public ExpressionRewriter(IEnumerable<ParameterExpression> sourceParameters, bool allowOuterVariables = false)
		{
			Debug.Assert((sourceParameters != null), "The sourceParameters argument cannot be null.");

			this.dctParameters = sourceParameters
								.Where(x => !string.IsNullOrWhiteSpace(x.Name))
								.ToDictionary(x => x.Name);
			this.allowOuterVariables = allowOuterVariables;
		}

		#endregion Constructor

		#region Private Code

		private readonly Dictionary<string, ParameterExpression> dctParameters;
		private readonly bool allowOuterVariables;

		/// <summary>
		/// Visits the <see cref="ConstantExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>The original expression.</returns>
		/// <exception cref="NotSupportedException">If outer variables are not allowed and an outer variable is found, the exception is thrown.</exception>
		protected override Expression VisitConstant(ConstantExpression node)
		{
			if (!this.allowOuterVariables
			&& (node.Value != null)
			&& (node.Type.Name.StartsWith("<>") || ((node.Type != typeof(string)) && !node.Type.IsPrimitive)))
			{
				throw new NotSupportedException("The use of outer variables has been disabled, either set the parameter to allow them or remove the outer variable.");
			}

			return base.VisitConstant(node);
		}
		/// <summary>
		/// Visits the <see cref="ParameterExpression"/>.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>The modified expression, if the parameter needed to be replaced; otherwise, returns the original expression.</returns>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="node"/> type does not match the already declared parameter's type, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When the <paramref name="node"/> was not declared in the Flexpression tree, the exception is thrown.</para>
		///	</exception>
		protected override Expression VisitParameter(ParameterExpression node)
		{
			ParameterExpression newReference;

			if (!string.IsNullOrWhiteSpace(node.Name))
			{
				if (this.dctParameters.TryGetValue(node.Name, out newReference))
				{
					if (!node.Type.IsAssignableFrom(newReference.Type))
						throw new ArgumentException(string.Format("Parameter, {0}, had a type mismatch between {1} and {2}.",
							node.Name,
							node.Type.GetFriendlyName(),
							newReference.Type.GetFriendlyName()));

					return newReference;
				}
				else
				{
					throw new ArgumentException(string.Format("Parameter, {0}, was not declared in the current Flexpression tree.", node.Name));
				}
			}
			else
			{
				return base.VisitParameter(node);
			}
		}

		#endregion Private Code
	}
}