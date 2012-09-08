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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Flexpressions.Extensions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The Block contains the expressions to execute.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed partial class Block<TParent> : FluentBase, IFlexpression where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Creates a new instance of the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="lookupParent">The lookup parent is used to have the <see cref="Block&lt;TParent&gt;"/> return to <paramref name="parent"/>, but pull its lookups from another (used to improve fluent syntax).</param>
		/// <param name="allowRethrow">Indicates whether or not this block allows rethrow.</param>
		/// <param name="implicitVariables">A collection of implicit variables to be used as valid, when looking for variables.</param>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> is null, the exception is thrown.</exception>
		internal Block(TParent parent, IFlexpression lookupParent = null, bool allowRethrow = false, IEnumerable<ParameterExpression> implicitVariables = null)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");

			this.parent = parent;
			this.lookupParent = lookupParent ?? parent;
			this.allowRethrow = allowRethrow;
			this.implicitVariables = implicitVariables;
			this.lstExpressions = new List<IExpressionWrapper>();
			this.variables = new List<ParameterExpression>();
			this.Variables = variables.AsReadOnly();
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Gets the variables of the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		public ReadOnlyCollection<ParameterExpression> Variables { get; private set; }

		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act(Expression act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			this.AddExpression(act);
			return this;
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act(Expression<Action> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Gets whether or not to allow outer variables in expressions.
		/// </summary>
		/// <returns>True - outer variables are allowed; False - outer variables will throw exceptions.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowOuterVariables()
		{
			return this.lookupParent.AllowOuterVariables();
		}
		/// <summary>
		/// Gets whether or not to allow a rethrow of an exception.
		/// </summary>
		/// <returns>True - rethrows are allowed; False - rethrows are not allowed.</returns>
		/// <exception cref="InvalidOperationException">If a call to this method is made when not within a catch block, the exception is thrown.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowRethrow()
		{
			// Blocks are the only construct that will be able to set this otherwise, but check up the chain
			// to see if any other parents might have it set (e.g. nested if blocks within a catch block).
			if (this.allowRethrow)
				return true;
			else
				return this.lookupParent.AllowRethrow();
		}
		/// <summary>
		/// Breaks out of the innermost loop.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		public TParent Break()
		{
			this.AddExpression(Expression.Break(this.GetLoopLabel(false)));

			return this.parent;
		}
		/// <summary>
		/// Continues back at the start of the innermost loop.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		public TParent Continue()
		{
			this.AddExpression(Expression.Continue(this.GetLoopLabel(true)));

			return this.parent;
		}
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
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Expression CreateExpression(IEnumerable<Expression> trailingExpressions = null)
		{
			var expressions =
				this.lstExpressions
				.Select(x => x.CreateExpression())
				.Concat(trailingExpressions ?? Enumerable.Empty<Expression>())
				.ToArray();

			if ((this.variables.Count == 0) && !expressions.Any())
				return Expression.Empty();
			else if ((this.variables.Count < 1) && (expressions.Count() == 1))
				return expressions.First();
			else
				return Expression.Block(this.variables, expressions);
		}
		/// <summary>
		/// Declares a new variable for the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="V">The type of the variable.</typeparam>
		/// <param name="variableName">The name of the variable.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When <paramref name="variableName"/> is already declared, the exception is thrown.</para>
		/// </exception>
		public Block<TParent> Declare<V>(string variableName)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");

			var variable = Expression.Variable(typeof(V), variableName);

			if (this.TryDeclareVariable(variable))
				return this;
			else
				throw new ArgumentException(string.Format("Variable already exists with the name, {0}.", variableName), "variableName");
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
			this.lookupParent.DeclareLabelTarget(labelTarget);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">If the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do(Expression<Func<bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Ends the current block, returning to the parent.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		public TParent End()
		{
			return this.parent;
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, R>(string variableName, Expression<Func<R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Gets all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.
		/// </summary>
		/// <returns>A collection of all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.</returns>
		public IEnumerable<LabelTarget> GetLabelTargets()
		{
			return this.lookupParent.GetLabelTargets();
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
			return this.lookupParent.GetLoopLabel(startOfLoop);
		}
		/// <summary>
		/// Gets the <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.
		/// </summary>
		/// <returns>The <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LabelExpression GetReturnLabel()
		{
			return this.lookupParent.GetReturnLabel();
		}
		/// <summary>
		/// Gets all variables (and parameters) up through the Flexpression tree.
		/// </summary>
		/// <returns>The collection of variables (and parameters) up through the Flexpression tree.</returns>
		public IEnumerable<ParameterExpression> GetVariablesInScope()
		{
			if (this.implicitVariables != null)
			{
				foreach (var v in this.implicitVariables)
					yield return v;
			}

			foreach (var v in this.variables)
				yield return v;

			foreach (var v in this.lookupParent.GetVariablesInScope())
				yield return v;
		}
		/// <summary>
		/// Goes to the specified target.
		/// </summary>
		/// <param name="labelName">Name of the label.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="labelName"/> is null, empty, or just whitespace, the exception is thrown.</exception>
		public TParent Goto(string labelName)
		{
			if (string.IsNullOrWhiteSpace(labelName))
				throw new ArgumentException("The label name cannot be null, empty, or whitespace.", "labelName");

			this.lstExpressions.Add(new GotoWrapper(this, labelName));

			return this.parent;
		}
		/// <summary>
		/// Goes to the specified target.
		/// </summary>
		/// <param name="target">The <see cref="LabelTarget"/> to goto.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		/// <exception cref="ArgumentNullException">If the <paramref name="target"/> is null, the exception is thrown.</exception>
		public TParent Goto(LabelTarget target)
		{
			if (target == null)
				throw new ArgumentNullException("target");

			this.AddExpression(Expression.Goto(target));

			return this.parent;
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If(Expression<Func<bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Inserts a user-defined <see cref="LabelExpression"/> into the expression tree.
		/// </summary>
		/// <param name="labelName">Name of the label.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="labelName"/> is null, empty, or just whitespace, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When a label exists with the of the same name as <paramref name="labelName"/>, the exception is thrown.</para>
		///	</exception>
		public Block<TParent> InsertLabel(string labelName)
		{
			LabelTarget labelTarget;

			return this.InsertLabel(labelName, out labelTarget);
		}
		/// <summary>
		/// Inserts a user-defined <see cref="LabelExpression"/> into the expression tree.
		/// </summary>
		/// <param name="labelName">Name of the label.</param>
		/// <param name="labelTarget">The <see cref="LabelTarget"/> that was created.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="labelName"/> is null, empty, or just whitespace, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When a label exists with the of the same name as <paramref name="labelName"/>, the exception is thrown.</para>
		///	</exception>
		public Block<TParent> InsertLabel(string labelName, out LabelTarget labelTarget)
		{
			if (string.IsNullOrWhiteSpace(labelName))
				throw new ArgumentException("The label name cannot be null, empty, or whitespace.", "labelName");
			if (this.GetLabelTargets().Any(x => x.Name == labelName))
				throw new ArgumentException(string.Format("A label of the name, {0}, already exists.", labelName), "labelName");

			labelTarget = Expression.Label(labelName);

			this.AddExpression(Expression.Label(labelTarget));
			this.DeclareLabelTarget(labelTarget);

			return this;
		}
		/// <summary>
		/// Returns from the <see cref="Block&lt;TParent&gt;"/> without a value.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the expression.</returns>
		public TParent Return()
		{
			return this.ReturnSafe(typeof(void), null);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<R>(Expression<Func<R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<R>(string variableName, Expression<Func<R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<R>(Expression<Func<R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Throws an already caught exception.  This operation is only valid within a catch block.
		/// </summary>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="InvalidOperationException">If a call to this method is made and a rethrow is not valid, the exception is thrown.</exception>
		public TParent Throw()
		{
			if (!this.AllowRethrow())
				throw new InvalidOperationException("Invalid call, no catch block was detected.");

			this.AddExpression(Expression.Rethrow());

			return this.parent;
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw(Expression<Func<Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Creates a <see cref="Flexpressions.Try&lt;TParent&gt;"/>, returning its try <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <returns>The try <see cref="Block&lt;TParent&gt;"/> of the <see cref="Flexpressions.Try&lt;TParent&gt;"/> statement.</returns>
		public Block<Try<Block<TParent>>> Try()
		{
			var tryBlock = Try<Block<TParent>>.Create(this);

			this.lstExpressions.Add(tryBlock.parent);

			return tryBlock;
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<R>(string variableName, Expression<Func<R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While(Expression<Func<bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}

		#endregion Public Code

		#region Private Code

		private readonly TParent parent;
		private readonly IFlexpression lookupParent;
		private readonly bool allowRethrow;
		private readonly IEnumerable<ParameterExpression> implicitVariables;
		private readonly List<IExpressionWrapper> lstExpressions;
		private readonly List<ParameterExpression> variables;

		/// <summary>
		/// Encapsulates the addition of a new statement to the <see cref="Block&lt;TParent&gt;"/> to ensure all variables/parameters are properly referenced.
		/// </summary>
		/// <param name="expression">The new expression to add.</param>
		private void AddExpression(Expression expression)
		{
			this.lstExpressions.Add(new ExpressionWrapper(expression.RewriteExpression(this.GetVariablesInScope(), this.AllowOuterVariables())));
		}
		/// <summary>
		/// Creates a foreach loop with the provided inputs.
		/// </summary>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="variableType">The type of the objects being iterated over.</param>
		/// <param name="collection">The expression to return the collection to iterate over.</param>
		/// <param name="collectionType">The type of the collection enumerator.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentException">When <paramref name="variableName"/> is already declared, the exception is thrown.</exception>
		private Block<Block<TParent>> ForeachSafe(string variableName, Type variableType, Expression collection, Type collectionType)
		{
			//////////////////////////////////////////////////////////////////
			//  var enumerator = collection.GetEnumerator();
			//////////////////////////////////////////////////////////////////

			var getEnumeratorMethodInfo = collectionType.GetMethod("GetEnumerator");
			var enumeratorType = getEnumeratorMethodInfo.ReturnType;
			var enumeratorVariable = Expression.Variable(enumeratorType);

			this.TryDeclareVariable(enumeratorVariable);
			this.AddExpression(Expression.Assign(enumeratorVariable, Expression.Call(collection, getEnumeratorMethodInfo)));

			//////////////////////////////////////////////////////////////////
			//  while (enumerator.MoveNext())
			//  {
			//       // Loop Body ...
			//  }
			//////////////////////////////////////////////////////////////////

			var moveNextMethodInfo = typeof(IEnumerator).GetMethod("MoveNext");
			var loopCondition = Expression.Call(enumeratorVariable, moveNextMethodInfo);

			Block<Block<TParent>> loopBody;
			Expression prependToLoop;
			var loop = Loop<Block<TParent>>.Create(this, loopCondition, true, out loopBody, out prependToLoop);

			Debug.Assert((prependToLoop == null), "The prependToLoop should be null, since a while loop is created.");

			//////////////////////////////////////////////////////////////////
			//  (In loop body)
			//  var variable = enumerator.Current;
			//////////////////////////////////////////////////////////////////

			var loopVariable = Expression.Variable(variableType, variableName);

			if (!this.TryDeclareVariable(loopVariable))
				throw new ArgumentException(string.Format("Variable already exists with the name, {0}.", variableName), "variableName");

			var currentGetPropertyInfo = enumeratorType.GetProperty("Current").GetGetMethod();
			Expression currentValueExpression = Expression.Property(enumeratorVariable, currentGetPropertyInfo);

			// Apply a type conversion if the types don't match.
			if (variableType != currentGetPropertyInfo.ReturnType)
				currentValueExpression = Expression.Convert(currentValueExpression, variableType);

			loopBody.Act(Expression.Assign(loopVariable, currentValueExpression));

			// Wrap the whole statement in a using block, if the enumerator is generic (the generic form has a dispose method).
			if (collectionType.IsGenericType)
			{
				Block<Block<TParent>> usingBody;

				this.lstExpressions.Add(Flexpressions.Using<Block<TParent>>.Create(this, enumeratorVariable, out usingBody));
				usingBody.lstExpressions.Add(loop);
			}
			else
			{
				this.lstExpressions.Add(loop);
			}

			return loopBody;
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		private Block<If<Block<TParent>>> IfSafe(Expression test)
		{
			var trueBlock = Flexpressions.If<Block<TParent>>.Create(this, test);

			this.lstExpressions.Add(trueBlock.parent);

			return trueBlock;
		}
		/// <summary>
		/// Creates a loop with the provided inputs.
		/// </summary>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <param name="checkConditionAtStart">Indicates that the loop will check the condition at the start of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the <see cref="T:Flexpressions.Loop&lt;TParent&gt;"/>.</returns>
		private Block<Block<TParent>> LoopSafe(Expression condition, bool checkConditionAtStart)
		{
			Block<Block<TParent>> loopBody;
			Expression prependToLoop;

			var loop = Loop<Block<TParent>>.Create(this, condition, checkConditionAtStart, out loopBody, out prependToLoop);

			if (prependToLoop != null)
				this.Act(prependToLoop);

			this.lstExpressions.Add(loop);

			return loopBody;
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="type">The return type of the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <param name="expression">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The calling body to return to.</returns>
		/// <exception cref="ArgumentException">If the <paramref name="type"/> does not match the expected return type, the exception is thrown.</exception>
		private TParent ReturnSafe(Type type, Expression expression)
		{
			LabelExpression returnLabel = this.GetReturnLabel();

			if (returnLabel.Type != type)
				throw new ArgumentException(string.Format("Type mismatch in the return statement, {0}, differs from expected, {1}.",
					type.GetFriendlyName(),
					returnLabel.Type.GetFriendlyName()));

			if (expression != null)
				this.AddExpression(Expression.Return(returnLabel.Target, expression, type));
			else
				this.AddExpression(Expression.Return(returnLabel.Target));

			return this.parent;
		}
		/// <summary>
		/// Sets the variable with the provided expression.
		/// </summary>
		/// <param name="variableType">The type of the variable to set.</param>
		/// <param name="variableName">The name of the variable to set.</param>
		/// <param name="set">The <see cref="Expression"/> to set the variable's value.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		private Block<TParent> SetSafe(Type variableType, string variableName, Expression set)
		{
			var variable = Expression.Variable(variableType, variableName);

			// Auto-declare the variable if it isn't already in the system.
			this.TryDeclareVariable(variable);
			this.AddExpression(Expression.Assign(variable, set));

			return this;
		}
		/// <summary>
		/// Constructs a <see cref="Switch&lt;TParent, R&gt;"/> and returns it.
		/// </summary>
		/// <typeparam name="R">The type of the switch value statement.</typeparam>
		/// <param name="switchValue">The <see cref="Expression"/> to produce the value to switch on.</param>
		/// <returns>The constructed <see cref="Switch&lt;TParent, R&gt;"/>.</returns>
		private Switch<Block<TParent>, R> SwitchSafe<R>(Expression switchValue)
		{
			var s = new Switch<Block<TParent>, R>(this, switchValue);

			this.lstExpressions.Add(s);

			return s;
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		private TParent ThrowSafe(Expression expression)
		{
			this.AddExpression(Expression.Throw(expression));

			return this.parent;
		}
		/// <summary>
		/// Tries to declare the provided variable.
		/// </summary>
		/// <param name="variable">The <see cref="ParameterExpression"/> to attempt to declare.</param>
		/// <returns>True - if the variable was declared, False - otherwise.</returns>
		private bool TryDeclareVariable(ParameterExpression variable)
		{
			if ((variable.Name == null) || !this.GetVariablesInScope().Any(x => x.Name == variable.Name))
			{
				this.variables.Add(variable);
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <param name="variableType">The type of the variable to hold the using's object.</param>
		/// <param name="variableName">The name of the variable to hold the using's object.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		private Block<Block<TParent>> UsingSafe(Type variableType, string variableName, Expression set)
		{
			Block<Block<TParent>> usingBody;

			var variable = Expression.Variable(variableType, variableName);

			this.TryDeclareVariable(variable);
			this.AddExpression(Expression.Assign(variable, set));
			this.lstExpressions.Add(Flexpressions.Using<Block<TParent>>.Create(this, variable, out usingBody));

			return usingBody;
		}

		#endregion Private Code
	}
}