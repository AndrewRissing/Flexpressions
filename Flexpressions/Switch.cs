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
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Flexpressions.Extensions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The <see cref="Switch&lt;TParent, R&gt;"/> class encapsulates a switch statement.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	/// <typeparam name="R">The type of the switch value being evaluated.</typeparam>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed partial class Switch<TParent, R> : FluentBase, IFlexpression, ISwitch where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Switch&lt;TParent, R&gt;" /> class.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="switchValue">The <see cref="Expression"/> to produce the value to switch on.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="parent"/> or <paramref name="switchValue"/> is null, the exception is thrown.</exception>
		internal Switch(TParent parent, Expression switchValue)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");
			Debug.Assert((switchValue != null), "The switchValue argument cannot be null.");

			this.parent = parent;
			this.switchValue = switchValue.RewriteExpression(this.GetVariablesInScope(), this.AllowOuterVariables());
			this.lstSwitchCases = new List<SwitchCase<Switch<TParent, R>, R>>();
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Gets whether or not to allow outer variables in expressions.
		/// </summary>
		/// <returns>True - outer variables are allowed; False - outer variables will throw exceptions.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowOuterVariables()
		{
			return this.parent.AllowOuterVariables();
		}
		/// <summary>
		/// Gets whether or not to allow a rethrow of an exception.
		/// </summary>
		/// <returns>True - rethrows are allowed; False - rethrows are not allowed.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowRethrow()
		{
			return this.parent.AllowRethrow();
		}
		/// <summary>
		/// Assigns the default body.
		/// </summary>
		/// <param name="defaultBody">The body of the default case.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="defaultBody"/> is null, the exception is thrown.</exception>
		/// <exception cref="InvalidOperationException">When the switch statement has more than one default statement, the exception is thrown.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void AssignDefaultBody(IFlexpression defaultBody)
		{
			if (defaultBody == null)
				throw new ArgumentNullException("defaultBody");
			if (this.defaultBody != null)
				throw new InvalidOperationException("Only one default statement is allowed per switch statement.");

			this.defaultBody = defaultBody;
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case(Expression<Func<R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s provided will throw an exception if non-null.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <exception cref="ArgumentException">When <paramref name="trailingExpressions"/> is not null, the exception is thrown.</exception>
		/// <exception cref="InvalidOperationException">When <see cref="Switch&lt;TParent, R&gt;"/> does not contain a default case or any switch cases, the exception is thrown.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null)
		{
			if (trailingExpressions != null)
			{
				throw new ArgumentException
				(
					string.Format("{0} does not process trailing expressions.", typeof(If<TParent>).GetFriendlyName()),
					"trailingExpressions"
				);
			}

			var defaultBody = (this.defaultBody != null) ? this.defaultBody.CreateExpression() : null;
			var switchCases = this.lstSwitchCases.Select(x => x.CreateSwitchCase()).Where(x => x != null).ToArray();

			if (switchCases.Any())
			{
				return Expression.Switch
				(
					typeof(void),
					this.switchValue,
					defaultBody,
					null,
					switchCases
				);
			}
			else if (defaultBody != null)
			{
				// If you have just a default case, then optimize this logic.
				return defaultBody;
			}
			else
			{
				throw new InvalidOperationException("A switch statement must have at least a default body or switch case.");
			}
		}
		/// <summary>
		/// Creates a default case for the switch statement.
		/// </summary>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the default case.</returns>
		/// <exception cref="InvalidOperationException">When the switch statement has more than one default statement, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Default()
		{
			if (this.defaultBody != null)
				throw new InvalidOperationException("Only one default statement is allowed per switch statement.");

			return this.CaseSafe(null);
		}
		/// <summary>
		/// Declares a new <see cref="LabelTarget"/> on the parent <see cref="Flexpression&lt;S&gt;"/> object.
		/// </summary>
		/// <param name="labelTarget">The <see cref="LabelTarget"/> to add to the parent <see cref="Flexpression&lt;S&gt;"/> object.</param>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="labelTarget"/> has a name that is null, empty, or whitespace, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When the name of the <paramref name="labelTarget"/> has already been declared, the exception is thrown.</para>
		///	</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DeclareLabelTarget(LabelTarget labelTarget)
		{
			this.parent.DeclareLabelTarget(labelTarget);
		}
		/// <summary>
		/// Ends the current switch, returning to the parent.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		/// <exception cref="InvalidOperationException">When the switch statement doesn't contain either a case statement or a default case, the exception is thrown.</exception>
		public TParent EndSwitch()
		{
			if ((this.defaultBody == null) && (this.lstSwitchCases.Count < 1))
				throw new InvalidOperationException("A switch statement must contain either a case statement or a default case.");

			return this.parent;
		}
		/// <summary>
		/// Gets all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.
		/// </summary>
		/// <returns>A collection of all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IEnumerable<LabelTarget> GetLabelTargets()
		{
			return this.parent.GetLabelTargets();
		}
		/// <summary>
		/// Gets the <see cref="LabelTarget"/> of the innermost loop closest to the current instance.
		/// </summary>
		/// <param name="startOfLoop">If set to <c>true</c>, the <see cref="LabelTarget"/> representing the start is returned; otherwise, the end is returned.</param>
		/// <returns>A <see cref="LabelTarget"/> representing either the start or end of a loop.</returns>
		/// <exception cref="InvalidOperationException">If a call to this method is made when not within a loop, the exception is thrown.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LabelTarget GetLoopLabel(bool startOfLoop)
		{
			return this.parent.GetLoopLabel(startOfLoop);
		}
		/// <summary>
		/// Gets the <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.
		/// </summary>
		/// <returns>The <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LabelExpression GetReturnLabel()
		{
			return this.parent.GetReturnLabel();
		}
		/// <summary>
		/// Gets all variables (and parameters) up through the Flexpression tree.
		/// </summary>
		/// <returns>The collection of variables (and parameters) up through the Flexpression tree.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IEnumerable<ParameterExpression> GetVariablesInScope()
		{
			return this.parent.GetVariablesInScope();
		}

		#endregion Public Code

		#region Private Code

		private readonly TParent parent;
		private readonly Expression switchValue;
		private readonly List<SwitchCase<Switch<TParent, R>, R>> lstSwitchCases;
		private IFlexpression defaultBody;

		/// <summary>
		/// Creates a <see cref="SwitchCase&lt;TParent, R&gt;"/> for the current switch.
		/// </summary>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> of the new case statement.</returns>
		private SwitchCase<Switch<TParent, R>, R> CaseSafe(Expression caseValue)
		{
			var switchCase = new SwitchCase<Switch<TParent, R>, R>(this, caseValue);

			this.lstSwitchCases.Add(switchCase);

			return switchCase;
		}

		#endregion Private Code
	}
}