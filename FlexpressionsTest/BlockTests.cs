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
using System.Linq;
using System.Linq.Expressions;
using Flexpressions;
using Flexpressions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="Block&lt;TParent&gt;"/>.
	/// </summary>
	[TestClass]
	public class BlockTests
	{
		/// <summary>
		/// Exercises all versions of Act to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockActExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();
			var callCount = 0;
			var expectedCallCount = 0;

			Action<int[]> interceptor = (int[] arguments) =>
			{
				++callCount;
				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);
			};

			var actions = Utility.CreateActions<int>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var blockType = block.GetType();

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			expectedCallCount = 0;

			foreach (var genericMethod in blockType.GetMethods().Where(x => x.Name == "Act"))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (actions.TryGetValue(parameterType, out inputAction))
				{
					++expectedCallCount;

					if (actions.TryGetValue(parameterType, out inputAction))
						methodToInvoke.Invoke(block, new object[] { inputAction });
					else if (parameterType != typeof(Expression))
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
				}
			}

			var method = block.End().Compile();
			method(inputs);

			Assert.AreEqual<int>(17, callCount);
		}
		/// <summary>
		/// Exercises all versions of Do, While, and If to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockDoWhileAndIfExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();
			var callCount = 0;
			var expectedCallCount = 0;

			Func<int[], bool> interceptor = (int[] arguments) =>
			{
				++callCount;

				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return false;
			};

			var funcs = Utility.CreateFuncs<int, bool>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var blockType = block.GetType();

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Do") || (x.Name == "While") || (x.Name == "If")))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					++expectedCallCount;

					if (funcs.TryGetValue(parameterType, out inputAction))
						methodToInvoke.Invoke(block, new object[] { inputAction });
					else if (parameterType != typeof(Expression))
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
				}
			}

			var method = block.End().Compile();
			method(inputs);

			Assert.AreEqual<int>(expectedCallCount, callCount);
		}
		/// <summary>
		/// Exercises all versions of Set to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockSetExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();
			var callCount = 0;
			var expectedCallCount = 0;

			Func<int[], int> interceptor = (int[] arguments) =>
			{
				++callCount;

				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return 42;
			};

			var funcs = Utility.CreateFuncs<int, int>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var blockType = block.GetType();

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			block.Set<int>("value", () => 42);

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Set")))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[1].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					++expectedCallCount;

					if (funcs.TryGetValue(parameterType, out inputAction))
						methodToInvoke.Invoke(block, new object[] { "value", inputAction });
					else if (parameterType != typeof(Expression))
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
				}
			}

			var method = block.End().Compile();
			method(inputs);

			Assert.AreEqual<int>(expectedCallCount, callCount);
		}
		/// <summary>
		/// Exercises all versions of Foreach to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockForeachExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();
			var callCount = 0;
			var expectedCallCount = 0;

			Func<int[], IEnumerable> interceptor = (int[] arguments) =>
			{
				++callCount;

				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return new int[0];
			};

			var funcs = Utility.CreateFuncs<int, IEnumerable>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var blockType = block.GetType();

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Foreach")))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int[]>();
				var parameterType = methodToInvoke.GetParameters()[1].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					++expectedCallCount;

					if (funcs.TryGetValue(parameterType, out inputAction))
						methodToInvoke.Invoke(block, new object[] { string.Format("value{0}", expectedCallCount), inputAction });
					else if (parameterType != typeof(Expression))
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
				}
			}

			var method = block.End().Compile();
			method(inputs);

			Assert.AreEqual<int>(expectedCallCount, callCount);
		}
		/// <summary>
		/// Exercises all versions of Return to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockReturnExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();

			Func<int[], int> interceptor = (int[] arguments) =>
			{
				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return 42;
			};

			var funcs = Utility.CreateFuncs<int, int>(interceptor);
			var blockType = typeof(Block<Flexpression<Func<int[], int>>>);

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Return") && (x.GetParameters().Length > 0)))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					if (funcs.TryGetValue(parameterType, out inputAction))
					{
						var block = Flexpression<Func<int[], int>>.Create(true, "i");

						for (int j = 0; j < inputs.Length; ++j)
						{
							int k = j;
							block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
						}

						methodToInvoke.Invoke(block, new object[] { inputAction });

						Assert.AreEqual<int>(42, block.End().Compile()(inputs));
					}
					else if (parameterType != typeof(Expression))
					{
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
					}
				}
			}
		}
		/// <summary>
		/// Exercises all versions of Switch to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockSwitchExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();
			var callCount = 0;
			var expectedCallCount = 0;

			Func<int[], int> interceptor = (int[] arguments) =>
			{
				++callCount;

				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return 42;
			};

			var funcs = Utility.CreateFuncs<int, int>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var blockType = block.GetType();

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Switch")))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					++expectedCallCount;

					if (funcs.TryGetValue(parameterType, out inputAction))
					{
						var switchBlock = (Switch<Block<Flexpression<Action<int[]>>>, int>)methodToInvoke.Invoke(block, new object[] { inputAction });

						switchBlock
							.Case(() => 0)
							.Begin()
							.End()
						.EndSwitch();
					}
					else if (parameterType != typeof(Expression))
					{
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
					}
				}
			}

			var method = block.End().Compile();
			method(inputs);

			Assert.AreEqual<int>(expectedCallCount, callCount);
		}
		/// <summary>
		/// Exercises all versions of Throw to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockThrowExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();

			Func<int[], Exception> interceptor = (int[] arguments) =>
			{
				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return new NotImplementedException();
			};

			var funcs = Utility.CreateFuncs<int, Exception>(interceptor);
			var blockType = typeof(Block<Flexpression<Action<int[]>>>);

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Throw") && (x.GetParameters().Length > 0)))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int>();
				var parameterType = methodToInvoke.GetParameters()[0].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					if (funcs.TryGetValue(parameterType, out inputAction))
					{
						var block = Flexpression<Action<int[]>>.Create(true, "i");

						for (int j = 0; j < inputs.Length; ++j)
						{
							int k = j;
							block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
						}

						methodToInvoke.Invoke(block, new object[] { inputAction });

						try
						{
							block.End().Compile()(inputs);
							Assert.Fail("NotImplementedException expected.");
						}
						catch (NotImplementedException)
						{
							/* Intentionally left blank. */
						}
					}
					else if (parameterType != typeof(Expression))
					{
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
					}
				}
			}
		}
		/// <summary>
		/// Exercises all versions of Using to ensure consistency.
		/// </summary>
		[TestMethod]
		public void BlockUsingExercise()
		{
			Expression inputAction;
			var inputs = Enumerable.Range(0, 17).ToArray();
			var callCount = 0;
			var expectedCallCount = 0;

			Func<int[], IDisposable> interceptor = (int[] arguments) =>
			{
				++callCount;

				Assert.IsTrue(arguments.Length <= inputs.Length);
				CollectionAssert.AreEqual(inputs.Take(arguments.Length).ToArray(), arguments);

				return null;
			};

			var funcs = Utility.CreateFuncs<int, IDisposable>(interceptor);
			var block = Flexpression<Action<int[]>>.Create(true, "i");
			var blockType = block.GetType();

			for (int j = 0; j < inputs.Length; ++j)
			{
				int k = j;
				block.Set<int[], int>(string.Format("p{0}", j + 1), i => i[k]);
			}

			foreach (var genericMethod in blockType.GetMethods().Where(x => (x.Name == "Using")))
			{
				var methodToInvoke = genericMethod.MakeMethodConcrete<int, int[]>();
				var parameterType = methodToInvoke.GetParameters()[1].ParameterType;

				if (funcs.TryGetValue(parameterType, out inputAction))
				{
					++expectedCallCount;

					if (funcs.TryGetValue(parameterType, out inputAction))
						methodToInvoke.Invoke(block, new object[] { string.Format("value{0}", expectedCallCount), inputAction });
					else if (parameterType != typeof(Expression))
						Assert.Fail("Unable to find matching action for {0}.", parameterType.GetFriendlyName());
				}
			}

			var method = block.End().Compile();
			method(inputs);

			Assert.AreEqual<int>(expectedCallCount, callCount);
		}
		/// <summary>
		/// Exercises various methods on Block to ensure input is validated.
		/// </summary>
		[TestMethod]
		public void BlockInputValidation()
		{
			var block = Flexpression<Action<int[]>>.Create();
			
			block.ValidateInputCase
			(
				new InputCase("Act",				typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("Declare",			typeof(ArgumentException),		new object[] { null }),
				new InputCase("Declare",			typeof(ArgumentException),		new object[] { "" }),
				new InputCase("Declare",			typeof(ArgumentException),		new object[] { "     " }),
				new InputCase("DeclareLabelTarget", typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("Do",					typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("Foreach",			typeof(ArgumentException),		new object[] { null, (Expression<Func<IEnumerable>>)(() => new int[0]) }),
				new InputCase("Foreach",			typeof(ArgumentException),		new object[] { "", (Expression<Func<IEnumerable>>)(() => new int[0]) }),
				new InputCase("Foreach",			typeof(ArgumentException),		new object[] { "     ", (Expression<Func<IEnumerable>>)(() => new int[0]) }),
				new InputCase("Foreach",			typeof(ArgumentNullException),	new object[] { "a", null }),
				new InputCase("If",					typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("InsertLabel",		typeof(ArgumentException),		new object[] { null, null }),
				new InputCase("Return",				typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("Set",				typeof(ArgumentNullException),	new object[] { "a", null }),
				new InputCase("Set",				typeof(ArgumentException),		new object[] { null, null }),
				new InputCase("Set",				typeof(ArgumentException),		new object[] { "", null }),
				new InputCase("Set",				typeof(ArgumentException),		new object[] { "     ", null }),
				new InputCase("Switch",				typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("Throw",				typeof(ArgumentNullException),	new object[] { null }),
				new InputCase("Using",				typeof(ArgumentNullException),	new object[] { "a", null }),
				new InputCase("Using",				typeof(ArgumentException),		new object[] { null, null }),
				new InputCase("Using",				typeof(ArgumentException),		new object[] { "", null }),
				new InputCase("Using",				typeof(ArgumentException),		new object[] { "     ", null }),
				new InputCase("While",				typeof(ArgumentNullException),	new object[] { null })
			);
		}
		/// <summary>
		/// Calls Block's Declare method with a variable name that has already been declared.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockDeclareWithDuplicateVariableName()
		{
			Flexpression<Action>.Create()
				.Declare<int>("a")
				.Declare<int>("a");
		}
		/// <summary>
		/// Calls Block's InsertLabel method with a label name that has already been declared.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockInsertLabelWithDuplicateLabelName()
		{
			LabelTarget labelTarget;

			Flexpression<Action>.Create()
				.InsertLabel("a", out labelTarget)
				.InsertLabel("a", out labelTarget);
		}
		/// <summary>
		/// Calls Block's Foreach method with a variable name that has already been declared.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockForeachWithDuplicateVariableName()
		{
			Flexpression<Action>.Create()
				.Declare<int>("a")
				.Foreach<int, int[]>("a", () => new int[10]);
		}
		/// <summary>
		/// Calls Block's Foreach method with a required convert.
		/// </summary>
		[TestMethod]
		public void BlockForeachWithIEnumerable()
		{
			Flexpression<Func<int>>
				.Create()
					.Declare<int>("sum")
					.Foreach<int, int[]>("a", () => new int[3] { 1, 2, 3 })
						.Set<int, int, int>("sum", (sum, a) => sum + a)
					.End()
				.Return<int, int>(sum => sum)
				.CreateLambda()
				.TestExpression(6);
		}
		/// <summary>
		/// Calls Block's Return method with a type mismatch.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockReturnTypeMismatch()
		{
			Flexpression<Func<int>>.Create().Return<long>(() => 132L);
		}
		/// <summary>
		/// Calls Block's Return method to ensure it is properly handled.
		/// </summary>
		[TestMethod]
		public void BlockReturn()
		{
			Action failure = () => Assert.Fail();

			var method = Flexpression<Action>
				.Create(true)
					.If(() => true)
						.Return()
					.EndIf()
					.Act(() => failure())
				.Return()
				.Compile();

			method();
		}
		/// <summary>
		/// Calls Block's Break method to ensure it is properly handled.
		/// </summary>
		[TestMethod]
		public void BlockBreak()
		{
			Flexpression<Func<bool>>
				.Create()
					.Set<int>("counter", () => 0)
					.While<int>(counter => counter < 5)
						.Set<int, int>("counter", counter => counter + 1)
						.Break()
				.Return<int, bool>(counter => counter == 1)
				.CreateLambda()
				.TestExpression(true);
		}
		/// <summary>
		/// Calls Block's Break method to ensure it is properly handled.
		/// </summary>
		[TestMethod]
		public void BlockContinue()
		{
			Flexpression<Func<bool>>
				.Create(true)
					.Set<int>("counter", () => 0)
					.Set<bool>("flag", () => true)
					.While<int>(counter => counter < 5)
						.Set<int, int>("counter", counter => counter + 1)
						.If(() => true)
							.Continue()
						.EndIf()
						.Set<bool>("flag", () => false)
					.End()
					.Return<bool, bool>(flag => flag)
				.CreateLambda()
				.TestExpression(true);
		}
		/// <summary>
		/// Calls Block's Goto method to ensure it is properly handled.
		/// </summary>
		[TestMethod]
		public void BlockGotoWithString()
		{
			var block = Flexpression<Func<bool>>.Create(true);

			block
				.Set<bool>("flag", () => true)
				.Goto("exit");

			block
				.Set<bool>("flag", () => false)
				.InsertLabel("exit")
				.Return<bool, bool>(flag => flag)
				.CreateLambda()
				.TestExpression(true);
		}
		/// <summary>
		/// Calls Block's Goto method with a LabelTarget parameter to ensure it is properly handled.
		/// </summary>
		[TestMethod]
		public void BlockGotoWithLabelTargetOutParameter()
		{
			LabelTarget labelTarget;

			var block = Flexpression<Func<bool>>
				.Create(true)
					.If(() => false)
						.InsertLabel("pass", out labelTarget)
						.Return<bool>(() => true)
					.EndIf();

			block.Goto(labelTarget);

			block
				.Return<bool>(() => false)
				.CreateLambda()
				.TestExpression(true);
		}
		/// <summary>
		/// Calls Block's Goto method to an undefined label without compiling.
		/// </summary>
		[TestMethod]
		public void BlockGotoToUndefinedLabelNoCompiling()
		{
			Flexpression<Func<bool>>.Create(true).Goto("exit");
		}
		/// <summary>
		/// Calls Block's Goto method to an undefined label with compiling.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockGotoToUndefinedLabelWithCompiling()
		{
			var block = Flexpression<Func<bool>>.Create(true);

			block
				.Set<bool>("flag", () => true)
				.Goto("exit");

			block
				.Set<bool>("flag", () => false)
				.Return<bool, bool>(flag => flag)
				.CreateLambda();
		}
		/// <summary>
		/// Calls the Block's Goto method with a null labelTarget argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void BlockGotoWithNullLabelTargetArgument()
		{
			Flexpression<Action>
					.Create()
						.Goto((LabelTarget)null);
		}
		/// <summary>
		/// Calls the Block's Goto method with a null label name argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockGotoWithNullLabelNameArgument()
		{
			Flexpression<Action>
					.Create()
						.Goto((string)null);
		}
		/// <summary>
		/// Calls the Block's Goto method with a empty label name argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockGotoWithEmptyLabelNameArgument()
		{
			Flexpression<Action>
					.Create()
						.Goto(string.Empty);
		}
		/// <summary>
		/// Calls the Block's Goto method with a empty label name argument.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BlockGotoWithWhiteSpaceLabelNameArgument()
		{
			Flexpression<Action>
					.Create()
						.Goto("      ");
		}
		/// <summary>
		/// Calls the Block's Set method on a Nullable type.
		/// </summary>
		[TestMethod]
		public void BlockSetWithNullableType()
		{
			Flexpression<Func<Nullable<int>, int>>
				.Create()
					.Set<Nullable<int>, int>("newValue", p1 => p1.Value)
					.Return<int, int>(newValue => newValue)
				.CreateLambda()
				.TestExpression(42, new Nullable<int>(42));
		}
	}
}