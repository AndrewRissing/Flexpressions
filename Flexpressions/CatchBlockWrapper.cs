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
using System.Diagnostics;
using System.Linq.Expressions;
using Flexpressions.Extensions;
using Flexpressions.Interfaces;

namespace Flexpressions
{
	/// <summary>
	/// The CatchBlockWrapper class wraps the necessary information for creating a <see cref="CatchBlock"/>.
	/// </summary>
	/// <typeparam name="TParent">The parent type from which to return to.</typeparam>
	internal sealed class CatchBlockWrapper<TParent> where TParent : IFlexpression
	{
		#region Constructor

		/// <summary>
		/// Prevents a default instance of the <see cref="CatchBlockWrapper&lt;TParent&gt;" /> class from being created.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="type">The type of the exception being caught.</param>
		/// <param name="variable">The variable (if requested) to set the caught exception into.</param>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> or <paramref name="type"/> is null, the exception is thrown.</exception>
		private CatchBlockWrapper(TParent parent, Type type, ParameterExpression variable)
		{
			ParameterExpression[] implicitVariables;

			this.parent = parent;
			this.type = type;
			this.variable = variable;

			if (variable != null)
				implicitVariables = new ParameterExpression[] { this.variable };
			else
				implicitVariables = null;

			this.catchBlock = new Block<TParent>(parent, parent, true, implicitVariables);
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Creates a new <see cref="CatchBlockWrapper&lt;TParent&gt;" /> returning the block representing the catch block.
		/// </summary>
		/// <param name="parent">The parent to return to once the block has been ended.</param>
		/// <param name="type">The type of the exception being caught.</param>
		/// <param name="variableName">The variable name of the caught exception.</param>
		/// <param name="catchBody">The body of the catch statement to hand back to the calling body to improve the fluent syntax.</param>
		/// <returns>The new <see cref="CatchBlockWrapper&lt;TParent&gt;" /> representing the catch statement.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="parent"/> or <paramref name="type"/> is null, the exception is thrown.</exception>
		/// <exception cref="ArgumentException">
		///		<para>When <paramref name="type"/> does not derive from <see cref="Exception"/>, the exception is thrown.</para>
		///		<para>- Or -</para>
		///		<para>When the <paramref name="variableName"/> is non-null and empty or whitespace, the exception is thrown.</para>
		/// </exception>
		public static CatchBlockWrapper<TParent> Create(TParent parent, Type type, string variableName, out Block<TParent> catchBody)
		{
			Debug.Assert((parent != null), "The parent argument cannot be null.");
			Debug.Assert((type != null), "The type argument cannot be null.");
			Debug.Assert(typeof(Exception).IsAssignableFrom(type), "The type argument must derive from Exception.");

			ParameterExpression variable;

			if (variableName != null)
			{
				variableName = variableName.Trim();

				Debug.Assert((variableName.Length > 0), "The variableName argument cannot be empty or whitespace.");

				variable = Expression.Variable(type, variableName);
			}
			else
			{
				variable = null;
			}

			var catchBlockWrapper = new CatchBlockWrapper<TParent>(parent, type, variable);
			catchBody = catchBlockWrapper.catchBlock;

			return catchBlockWrapper;
		}

		/// <summary>
		/// Creates the catch block from the <see cref="CatchBlockWrapper&lt;TParent&gt;"/>.
		/// </summary>
		/// <returns>The catch block from the <see cref="CatchBlockWrapper&lt;TParent&gt;"/>.</returns>
		public CatchBlock CreateCatchBlock()
		{
			return Expression.MakeCatchBlock(this.type, this.variable, this.catchBlock.CreateExpression(), null);
		}

		#endregion Public Code

		#region Private Code

		private readonly TParent parent;
		private readonly Type type;
		private readonly ParameterExpression variable;
		private readonly Block<TParent> catchBlock;

		#endregion Private Code
	}
}