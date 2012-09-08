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
	/// The <see cref="Try&lt;TParent&gt;"/> class encapsulates a try statement.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class Try<TParent> : FluentBase, IFlexpression where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Prevents a default instance of the <see cref="Try&lt;TParent&gt;" /> class from being created.
		/// </summary>
		/// <param name="parent">The parent to return to once the try statement has been ended.</param>
		private Try(TParent parent)
		{
			this.parent = parent;
			this.tryBlock = new Block<Try<TParent>>(this);
			this.lstCatchBlocks = new List<CatchBlockWrapper<Try<TParent>>>();
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
		/// Creates a catch block for the try block (no variable and catches all exceptions).
		/// </summary>
		/// <returns>The catch's <see cref="Block&lt;TParent&gt;"/> to begin inserting statements.</returns>
		public Block<Try<TParent>> Catch()
		{
			Block<Try<TParent>> catchBody;

			this.lstCatchBlocks.Add(CatchBlockWrapper<Try<TParent>>.Create(this, typeof(Exception), null, out catchBody));

			return catchBody;
		}
		/// <summary>
		/// Creates a catch block for the try block (no variable and catches all <typeparamref name="TException"/>s).
		/// </summary>
		/// <typeparam name="TException">The type of the <see cref="Exception"/> to catch.</typeparam>
		/// <returns>The catch's <see cref="Block&lt;TParent&gt;"/> to begin inserting statements.</returns>
		public Block<Try<TParent>> Catch<TException>() where TException : Exception
		{
			Block<Try<TParent>> catchBody;

			this.lstCatchBlocks.Add(CatchBlockWrapper<Try<TParent>>.Create(this, typeof(TException), null, out catchBody));

			return catchBody;
		}
		/// <summary>
		/// Creates a catch block for the try block (with a variable and catches all <typeparamref name="TException"/>s).
		/// </summary>
		/// <typeparam name="TException">The type of the <see cref="Exception"/> to catch.</typeparam>
		/// <param name="variableName">The variable name of the caught exception.</param>
		/// <returns>The catch's <see cref="Block&lt;TParent&gt;"/> to begin inserting statements.</returns>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When the <paramref name="variableName"/> is a name that is already declared, the exception is thrown.</para>
		///	</exception>
		public Block<Try<TParent>> Catch<TException>(string variableName) where TException : Exception
		{
			Block<Try<TParent>> catchBody;

			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");

			variableName = variableName.Trim();

			if (this.GetVariablesInScope().Any(x => x.Name == variableName))
				throw new ArgumentException(string.Format("Variable, {0}, is already declared.  Please assign a different name.", variableName));

			this.lstCatchBlocks.Add(CatchBlockWrapper<Try<TParent>>.Create(this, typeof(TException), variableName, out catchBody));

			return catchBody;
		}
		/// <summary>
		/// Converts the current instance into an <see cref="Expression"/>.
		/// </summary>
		/// <param name="trailingExpressions">The <see cref="Expression"/>s provided will throw an exception if non-null.</param>
		/// <returns>The <see cref="Expression"/> representing the current instance.</returns>
		/// <exception cref="ArgumentException">When <paramref name="trailingExpressions"/> is not null, the exception is thrown.</exception>
		/// <exception cref="InvalidOperationException">When the try block does not contain a finally block or at least one catch block, the exception is thrown.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null)
		{
			if (trailingExpressions != null)
			{
				throw new ArgumentException
				(
					string.Format("{0} does not process trailing expressions.", typeof(Try<TParent>).GetFriendlyName()),
					"trailingExpressions"
				);
			}

			if ((this.finallyBlock == null) && (this.lstCatchBlocks.Count < 1))
				throw new InvalidOperationException("A try block must contain either a catch block or a finally block.");

			var tryBlock = this.tryBlock.CreateExpression();

			return Expression.MakeTry
			(
				tryBlock.Type,
				tryBlock,
				(this.finallyBlock != null) ? this.finallyBlock.CreateExpression() : null,
				null,
				this.lstCatchBlocks.Select(x => x.CreateCatchBlock()).ToArray()
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
		/// Ends the current try block, returning to the parent.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		/// <exception cref="InvalidOperationException">When the try block does not contain a finally block or at least one catch block, the exception is thrown.</exception>
		public TParent EndTry()
		{
			// Since a try block will automatically end with a finally block, only catch blocks need to be checked for here.
			if (this.lstCatchBlocks.Count < 1)
				throw new InvalidOperationException("A try block must contain either a catch block or a finally block.");

			return this.parent;
		}
		/// <summary>
		/// Creates the finally clause for the try block.
		/// </summary>
		/// <returns>The finally block for the try block.</returns>
		public Block<TParent> Finally()
		{
			this.finallyBlock = new Block<TParent>(this.parent);

			return this.finallyBlock;
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

		#region Internal Code

		/// <summary>
		/// Creates a new <see cref="Try&lt;TParent&gt;" /> returning the <see cref="Block&lt;TParent&gt;"/> representing the try block.
		/// </summary>
		/// <param name="parent">The parent to return to once the try block has been ended.</param>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> is null, the exception is thrown.</exception>
		internal static Block<Try<TParent>> Create(TParent parent)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");

			return new Try<TParent>(parent).tryBlock;
		}

		#endregion Internal Code

		#region Private Code

		private readonly TParent parent;
		private readonly Block<Try<TParent>> tryBlock;
		private readonly List<CatchBlockWrapper<Try<TParent>>> lstCatchBlocks;
		private Block<TParent> finallyBlock;

		#endregion Private Code
	}
}