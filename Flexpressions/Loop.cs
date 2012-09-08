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
using Flexpressions.Extensions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The <see cref="Loop&lt;TParent&gt;"/> class encapsulates a loop statement.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	internal sealed class Loop<TParent> : FluentBase, IFlexpression where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Prevents a default instance of the <see cref="Loop&lt;TParent&gt;" /> class from being created.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <param name="checkConditionAtStart">Indicates that the loop will check the condition at the start of the loop.</param>
		private Loop(TParent parent, Expression condition, bool checkConditionAtStart)
		{
			this.parent = parent;
			this.condition = condition.RewriteExpression(this.GetVariablesInScope(), this.AllowOuterVariables());
			this.checkConditionAtStart = checkConditionAtStart;
			this.loopStart = Expression.Label();
			this.loopEnd = Expression.Label();
			this.loopBodyLabel = Expression.Label();
			this.loopBody = new Block<TParent>(this.parent, this);
			
			// Add a label to the start of the body, so that we can bypass the first check, if creating a do loop.
			if (!this.checkConditionAtStart)
				loopBody.Act(Expression.Label(this.loopBodyLabel));
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Creates a new <see cref="Loop&lt;TParent&gt;" /> representing the loop.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <param name="checkConditionAtStart">Indicates that the loop will check the condition at the start of the loop.</param>
		/// <param name="loopBody">The body of the loop to hand back to the calling body to improve the fluent syntax.</param>
		/// <param name="prependToLoop">An <see cref="Expression"/> to add just prior to the loop.</param>
		/// <returns>The new <see cref="Loop&lt;TParent&gt;" /> representing the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> or <paramref name="condition"/> is null, the exception is thrown.</exception>
		public static Loop<TParent> Create(TParent parent, Expression condition, bool checkConditionAtStart, out Block<TParent> loopBody, out Expression prependToLoop)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");
			Debug.Assert((condition != null), "The condition argument cannot be null.");

			var loop = new Loop<TParent>(parent, condition, checkConditionAtStart);

			loopBody = loop.loopBody;

			// Add a Goto to bypass the first conditional check in the case of a do loop.
			prependToLoop = (checkConditionAtStart) ? null : Expression.Goto(loop.loopBodyLabel);

			return loop;
		}

		/// <summary>
		/// Gets whether or not to allow outer variables in expressions.
		/// </summary>
		/// <returns>True - outer variables are allowed; False - outer variables will throw exceptions.</returns>
		public bool AllowOuterVariables()
		{
			return this.parent.AllowOuterVariables();
		}
		/// <summary>
		/// Gets whether or not to allow a rethrow of an exception.
		/// </summary>
		/// <returns>True - rethrows are allowed; False - rethrows are not allowed.</returns>
		public bool AllowRethrow()
		{
			return this.parent.AllowRethrow();
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

			return Expression.Loop
			(
				Expression.IfThenElse
				(
					this.condition,
					this.loopBody.CreateExpression(),
					Expression.Goto(this.loopEnd)
				),
				this.loopEnd,
				this.loopStart
			);
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
		public void DeclareLabelTarget(LabelTarget labelTarget)
		{
			this.parent.DeclareLabelTarget(labelTarget);
		}
		/// <summary>
		/// Gets all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.
		/// </summary>
		/// <returns>A collection of all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.</returns>
		public IEnumerable<LabelTarget> GetLabelTargets()
		{
			return this.parent.GetLabelTargets();
		}
		/// <summary>
		/// Gets the <see cref="LabelTarget"/> of the innermost loop closest to the current instance.
		/// </summary>
		/// <param name="startOfLoop">If set to <c>true</c>, the <see cref="LabelTarget"/> representing the start is returned; otherwise, the end is returned.</param>
		/// <returns>A <see cref="LabelTarget"/> representing either the start or end of a loop.</returns>
		public LabelTarget GetLoopLabel(bool startOfLoop)
		{
			return (startOfLoop) ? this.loopStart : this.loopEnd;
		}
		/// <summary>
		/// Gets the <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.
		/// </summary>
		/// <returns>The <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.</returns>
		public LabelExpression GetReturnLabel()
		{
			return this.parent.GetReturnLabel();
		}
		/// <summary>
		/// Gets all variables (and parameters) up through the Flexpression tree.
		/// </summary>
		/// <returns>The collection of variables (and parameters) up through the Flexpression tree.</returns>
		public IEnumerable<ParameterExpression> GetVariablesInScope()
		{
			return this.parent.GetVariablesInScope();
		}

		#endregion Internal Code

		#region Private Code

		private readonly TParent parent;
		private readonly Expression condition;
		private readonly bool checkConditionAtStart;
		private readonly LabelTarget loopStart;
		private readonly LabelTarget loopEnd;
		private readonly LabelTarget loopBodyLabel;
		private readonly Block<TParent> loopBody;

		#endregion Private Code
	}
}