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
using System.Linq.Expressions;
using Flexpressions.Extensions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The <see cref="If&lt;TParent&gt;"/> class encapsulates an if statement.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed partial class If<TParent> : FluentBase, IFlexpression where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Prevents a default instance of the <see cref="If&lt;TParent&gt;" /> class from being created.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="test">The <see cref="Expression" /> representing the test.</param>
		private If(TParent parent, Expression test)
		{
			this.parent = parent;
			this.test = test.RewriteExpression(this.GetVariablesInScope(), this.AllowOuterVariables());
			this.expressionTrue = new Block<If<TParent>>(this);
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
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s provided will throw an exception if non-null.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <exception cref="ArgumentException">When <paramref name="trailingExpressions"/> is not null, the exception is thrown.</exception>
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

			return Expression.IfThenElse
			(
				this.test,
				this.expressionTrue.CreateExpression(),
				(this.expressionFalse == null) ? Expression.Empty() : this.expressionFalse.CreateExpression()
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
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DeclareLabelTarget(LabelTarget labelTarget)
		{
			this.parent.DeclareLabelTarget(labelTarget);
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
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf(Expression<Func<bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else clause for the if block.
		/// </summary>
		/// <returns>The else clause for the if block.</returns>
		/// <exception cref="InvalidOperationException">When an else/else if was already set for this block, the exception is thrown.</exception>
		public Block<If<TParent>> Else()
		{
			if (this.expressionFalse != null)
				throw new InvalidOperationException("An else statement already exists for the current context.");

			var elseBlock = new Block<If<TParent>>(this);
			this.expressionFalse = elseBlock;

			return elseBlock;
		}
		/// <summary>
		/// Ends the current if, returning to the parent.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		public TParent EndIf()
		{
			return this.parent;
		}

		#endregion Public Code

		#region Internal Code

		/// <summary>
		/// Creates a new <see cref="If&lt;TParent&gt;" /> returning the <see cref="Block&lt;TParent&gt;"/> representing the true case.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="test">The <see cref="Expression" /> representing the test.</param>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> or <paramref name="test"/> is null, the exception is thrown.</exception>
		internal static Block<If<TParent>> Create(TParent parent, Expression test)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");
			Debug.Assert((test != null), "The test argument cannot be null.");

			return new If<TParent>(parent, test).expressionTrue;
		}

		#endregion Internal Code

		#region Private Code

		private readonly TParent parent;
		private readonly Expression test;
		private readonly Block<If<TParent>> expressionTrue;
		private IFlexpression expressionFalse;

		/// <summary>
		/// Creates an else if block of the if block.
		/// </summary>
		/// <param name="test">The test to perform for the else if block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="InvalidOperationException">When an else/else if was already set for this block, the exception is thrown.</exception>
		private Block<If<TParent>> ElseIfSafe(Expression test)
		{
			if (this.expressionFalse != null)
				throw new InvalidOperationException("An else statement already exists for the current context.");

			var elseIf = new If<TParent>(this.parent, test);

			this.expressionFalse = elseIf;

			return elseIf.expressionTrue;
		}

		#endregion Private Code
	}
}