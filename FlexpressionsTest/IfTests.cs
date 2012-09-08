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
using System.Linq;
using System.Linq.Expressions;
using Flexpressions;
using Flexpressions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="If&lt;TParent&gt;"/>.
	/// </summary>
	[TestClass]
	public class IfTests
	{
		/// <summary>
		/// Exercises all versions of ElseIf to ensure consistency.
		/// </summary>
		[TestMethod]
		public void IfElseIfExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();

			Func<int[], bool> interceptor = (int[] arguments) =>
			{
				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return false;
			};

			var funcs = Utility.CreateFuncs<int, bool>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var ifBlockType = typeof(If<Block<Flexpression<Action<int[]>>>>);

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			foreach (var caseMethod in ifBlockType.GetMethods().Where(x => x.Name == "ElseIf"))
			{
				var ifBlock = block.If(() => false).End();
				var methodToInvoke = caseMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					if (funcs.TryGetValue(parameterType, out inputAction))
						methodToInvoke.Invoke(ifBlock, new object[] { inputAction });
					else if (parameterType != typeof(Expression))
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
				}

				ifBlock.EndIf();
			}

			var method = block.End().Compile();
			method(inputs);
		}
		/// <summary>
		/// Exercises all versions of ElseIf properly handle null values.
		/// </summary>
		[TestMethod]
		public void IfInputValidation()
		{
			var ifBlock = Flexpression<Action<int[]>>.Create().If(() => false).End();

			ifBlock.ValidateInputCase
			(
				new InputCase("ElseIf", typeof(ArgumentNullException),	new object[] { null })
			);
		}
		/// <summary>
		/// Performs an ElseIf twice on an If block.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void IfWithTwoElseIfs()
		{
			var ifBlock = Flexpression<Action<int[]>>.Create().If(() => false).End();

			ifBlock.ElseIf(() => false);
			ifBlock.ElseIf(() => false);
		}
		/// <summary>
		/// Performs an Else twice on an If block.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void IfWithTwoElses()
		{
			var ifBlock = Flexpression<Action<int[]>>.Create().If(() => false).End();

			ifBlock.Else();
			ifBlock.Else();
		}
		/// <summary>
		/// Calls CreateExpression on an If block with non-null arguments.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void IfCreateExpressionWithNonNullArgument()
		{
			var ifBlock = Flexpression<Action<int[]>>.Create().If(() => false).End();

			ifBlock.CreateExpression(Enumerable.Empty<Expression>());
		}
		/// <summary>
		/// Performs an operation of If, where the else is exercised.
		/// </summary>
		[TestMethod]
		public void IfElseExecution()
		{
			Flexpression<Func<int>>
				.Create()
					.If(() => false)
						.Return<int>(() => 1)
					.Else()
						.Return<int>(() => 2)
					.EndIf()
				.End()
				.CreateLambda()
				.TestExpression(2);
		}
	}
}