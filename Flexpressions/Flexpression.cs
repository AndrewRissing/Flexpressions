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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Flexpressions.Extensions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The Flexpression class wraps <see cref="Expression"/>s to simplify the code necessary to generate expressions.
	/// </summary>
	/// <typeparam name="S">The signature of the Lambda expression that will be produced.</typeparam>
	public sealed class Flexpression<S> : FluentBase, IFlexpression where S : class
	{
		#region Constructor

		/// <summary>
		/// Creates a new instance of the Flexpression class.
		/// </summary>
		/// <param name="lambdaType">The lambda's type to be created.</param>
		/// <param name="returnType">The return type of the Flexpression.</param>
		/// <param name="parameters">The parameters for the lambda expression.</param>
		/// <param name="allowOuterVariables">Indicates whether or not outer variables (i.e. captured variables) are allowed.</param>
		private Flexpression(Type lambdaType, Type returnType, IList<ParameterExpression> parameters, bool allowOuterVariables)
		{
			this.lambdaType = lambdaType;
			this.returnLabel = Expression.Label(Expression.Label(returnType), Expression.Default(returnType));
			this.Parameters = new ReadOnlyCollection<ParameterExpression>(parameters);
			this.allowOuterVariables = allowOuterVariables;
			this.dctLabelTargets = new Dictionary<string, LabelTarget>();
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Creates a new Flexpression of the type <typeparamref name="S"/> with automatically generated parameters (p1, p2, etc.) based on the signature.
		/// </summary>
		/// <param name="allowOuterVariables">Indicates whether or not outer variables (i.e. captured variables) are allowed.</param>
		/// <param name="parameterNames">The array of parameter names to assign to the inputs.   </param>
		/// <returns>A new <see cref="Flexpression&lt;S&gt;"/> based on the signature of <typeparamref name="S"/>.</returns>
		/// <exception cref="InvalidOperationException">If the <typeparamref name="S"/> is not an Action or Func, the exception will be thrown.</exception>
		/// <exception cref="ArgumentException">If the <paramref name="parameterNames"/> is provided and the count does not match the number of parameters, the exception will be thrown.</exception>
		public static Block<Flexpression<S>> Create(bool allowOuterVariables = false, params string[] parameterNames)
		{
			Type lambdaType = typeof(S);
			ParameterExpression[] parameters;

			if (!lambdaType.IsSubclassOf(typeof(Delegate)))
				throw new InvalidOperationException(string.Format("{0} must be a delegate (Action or Func).", lambdaType.GetFriendlyName()));

			var invokeMethod = lambdaType.GetMethod("Invoke");
			var parametersInfos = invokeMethod.GetParameters();

			if (parameterNames.Length > 0)
			{
				if (parametersInfos.Length != parameterNames.Length)
					throw new ArgumentException(
						string.Format("If parameter names are defined, the number must equal {0} to match the parameter(s) of {1} (currently {2} parameter(s) are supplied).",
							parametersInfos.Length,
							lambdaType.GetFriendlyName(),
							parameterNames.Length),
						"parameterNames");

				parameters = parametersInfos.Zip(parameterNames, (t, n) => Expression.Parameter(t.ParameterType, n)).ToArray();
			}
			else
			{
				parameters = parametersInfos
					.Select(x => Expression.Parameter(x.ParameterType, string.Format("p{0}", x.Position + 1)))
					.ToArray();
			}

			var flexpression = new Flexpression<S>(lambdaType, invokeMethod.ReturnType, parameters, allowOuterVariables);
			var block = flexpression.block = new Block<Flexpression<S>>(flexpression);

			return block;
		}

		/// <summary>
		/// Gets the parameters of the Flexpression.
		/// </summary>
		public ReadOnlyCollection<ParameterExpression> Parameters { get; private set; }

		/// <summary>
		/// Gets whether or not to allow outer variables in expressions.
		/// </summary>
		/// <returns>True - outer variables are allowed; False - outer variables will throw exceptions.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowOuterVariables()
		{
			return this.allowOuterVariables;
		}
		/// <summary>
		/// Gets whether or not to allow a rethrow of an exception.
		/// </summary>
		/// <returns>True - rethrows are allowed; False - rethrows are not allowed.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AllowRethrow()
		{
			return false;
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
					string.Format("{0} does not process trailing expressions.", typeof(Flexpression<S>).GetFriendlyName()),
					"trailingExpressions"
				);
			}

			return this.block.CreateExpression(new Expression[] { this.GetReturnLabel() });
		}
		/// <summary>
		/// Creates the lambda expression, ready for compiling (useful for debugging).
		/// </summary>
		/// <param name="name">The name of the lambda. Used for generating debugging info.</param>
		/// <param name="tailCall">A <see cref="bool"/> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <returns>The lambda expression, ready for compiling.</returns>
		public Expression<S> CreateLambda(string name = null, bool tailCall = false)
		{
			return Expression.Lambda<S>
			(
				this.CreateExpression(),
				name,
				tailCall,
				this.Parameters
			);
		}
		/// <summary>
		/// Compiles the Flexpression into the signature <typeparamref name="S"/>.
		/// </summary>
		/// <param name="name">The name of the lambda. Used for generating debugging info.</param>
		/// <param name="tailCall">A <see cref="bool"/> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="debugInfoGenerator">The <see cref="DebugInfoGenerator"/> to supply to the compiler.</param>
		/// <returns>The compiled method of the signature <typeparamref name="S"/>.</returns>
		public S Compile(string name = null, bool tailCall = false, DebugInfoGenerator debugInfoGenerator = null)
		{
			var lambda = this.CreateLambda(name, tailCall);

			if (debugInfoGenerator == null)
				return lambda.Compile();
			else
				return lambda.Compile(debugInfoGenerator);
		}
		/// <summary>
		/// Declares a new <see cref="LabelTarget"/> on the parent <see cref="Flexpression&lt;S&gt;"/> object.
		/// </summary>
		/// <param name="labelTarget">The <see cref="LabelTarget"/> to add to the parent <see cref="Flexpression&lt;S&gt;"/> object.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="labelTarget"/> is null, the exception is thrown.</exception>
		/// <exception cref="ArgumentException">
		///		<para>When the <paramref name="labelTarget"/> has a name that is null, empty, or whitespace, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When the name of the <paramref name="labelTarget"/> has already been declared, the exception is thrown.</para>
		///	</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DeclareLabelTarget(LabelTarget labelTarget)
		{
			if (labelTarget == null)
				throw new ArgumentNullException("labelTarget");
			if (string.IsNullOrWhiteSpace(labelTarget.Name))
				throw new ArgumentException("The label name cannot be null, empty, or whitespace.", "labelName");
			if (this.dctLabelTargets.ContainsKey(labelTarget.Name))
				throw new ArgumentException(string.Format("LabelTarget of the name, {0}, already exists.", labelTarget.Name), "labelTarget");

			this.dctLabelTargets[labelTarget.Name] = labelTarget;
		}
		/// <summary>
		/// Gets all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.
		/// </summary>
		/// <returns>A collection of all user-defined <see cref="LabelTarget"/>s defined in the Flexpression tree.</returns>
		public IEnumerable<LabelTarget> GetLabelTargets()
		{
			return this.dctLabelTargets.Values;
		}
		/// <summary>
		/// Any calls to this method will result in an <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <param name="startOfLoop">If set to <c>true</c>, the <see cref="LabelTarget"/> representing the start is returned; otherwise, the end is returned.</param>
		/// <returns>An <see cref="InvalidOperationException"/> is always thrown.</returns>
		/// <exception cref="InvalidOperationException">If a call to this method is made, the exception is thrown.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LabelTarget GetLoopLabel(bool startOfLoop)
		{
			throw new InvalidOperationException("Invalid call, no loop was detected.");
		}
		/// <summary>
		/// Gets the <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.
		/// </summary>
		/// <returns>The <see cref="LabelExpression"/> used for returns out of the parent <see cref="Flexpression&lt;S&gt;"/>.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LabelExpression GetReturnLabel()
		{
			return this.returnLabel;
		}
		/// <summary>
		/// Gets all variables (and parameters) up through the Flexpression tree.
		/// </summary>
		/// <returns>The collection of variables (and parameters) up through the Flexpression tree.</returns>
		public IEnumerable<ParameterExpression> GetVariablesInScope()
		{
			return this.Parameters;
		}

		#endregion Public Code

		#region Private Code

		private readonly Type lambdaType;
		private readonly LabelExpression returnLabel;
		private readonly bool allowOuterVariables;
		private readonly Dictionary<string, LabelTarget> dctLabelTargets;
		private Block<Flexpression<S>> block;

		#endregion Private Code
	}
}