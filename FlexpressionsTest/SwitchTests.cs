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
	/// Contains all of the unit tests pertaining to the <see cref="Switch&lt;TParent, R&gt;"/>.
	/// </summary>
	[TestClass]
	public class SwitchTests
	{
		/// <summary>
		/// Exercises all versions of Case to ensure consistency.
		/// </summary>
		[TestMethod]
		public void SwitchCaseExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();

			Func<int[], int> interceptor = (int[] arguments) =>
			{
				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return 0;
			};

			var funcs = Utility.CreateFuncs<int, int>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			var switchBlock = block.Switch(() => 3);
			var switchType = switchBlock.GetType();

			foreach (var caseMethod in switchType.GetMethods().Where(x => x.Name == "Case"))
			{
				var methodToInvoke = caseMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					if (funcs.TryGetValue(parameterType, out inputAction))
					{
						var o = methodToInvoke.Invoke(switchBlock, new object[] { inputAction });
					}
					else if (parameterType != typeof(Expression))
					{
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
					}
				}
			}

			var lambda = switchBlock.EndSwitch().End().CreateLambda().ToCSharpString();

			var method = switchBlock.EndSwitch().End().Compile();
			method(inputs);
		}
		/// <summary>
		/// Exercises Case methods on Switch to ensure input is validated.
		/// </summary>
		[TestMethod]
		public void SwitchCaseInputValidation()
		{
			var switchBlock = Flexpression<Action<int[]>>.Create().Switch(() => 2);

			switchBlock.ValidateInputCase
			(
				new InputCase("Case", typeof(ArgumentNullException), new object[] { null })
			);
		}
		/// <summary>
		/// Creates a switch statement with two defaults to ensure the code throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SwitchWithTwoDefaults()
		{
			Flexpression<Func<int>>
				.Create()
					.Switch(() => 0)
						.Default()
						.Begin()
							.Return<int>(() => 5)
						.Default();
		}
		/// <summary>
		/// Creates a switch statement and attempts to assign two defaults.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SwitchAssignTwoDefaults()
		{
			Flexpression<Func<int>>
				.Create()
					.Switch(() => 0)
						.Case(() => 5)
						.Default()
						.Begin()
							.Return<int>(() => 5)
						.Case(() => 6)
						.Default();
		}
		/// <summary>
		/// Creates a switch statement and immediately ends it to ensure the code throws an exception.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SwitchWithNoContent()
		{
			Flexpression<Func<int>>
				.Create()
					.Switch(() => 0)
						.EndSwitch();
		}
		/// <summary>
		/// Creates a switch statement and immediately tries to add a null default body.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SwitchAssigningNullDefaultBody()
		{
			Flexpression<Func<int>>
				.Create()
					.Switch(() => 0)
						.AssignDefaultBody(null);
		}
		/// <summary>
		/// Creates a switch statement and calls CreateExpression with a non-null argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SwitchCreateExpressionWithNonNullArgument()
		{
			Flexpression<Func<int>>
				.Create()
					.Switch(() => 0)
						.CreateExpression(Enumerable.Empty<Expression>());
		}
		/// <summary>
		/// Creates a switch statement and calls CreateExpression with no content in the switch.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SwitchCreateExpressionWithNoContentInSwitch()
		{
			Flexpression<Func<int>>
				.Create()
					.Switch(() => 0)
						.CreateExpression(null);
		}
	}
}