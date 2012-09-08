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
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Flexpressions.Extensions;
using Flexpressions.Interfaces;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// The Utility class contains core unit test functionality for all of the unit tests.
	/// </summary>
	public static class Utility
	{
		/// <summary>
		/// Populates a dictionary with all possible variants of an <see cref="Action"/> with <typeparamref name="T"/> arguments, supplying
		/// these values to a interceptor method that will collapse all of these values into an array of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of the parameters to use for the action.</typeparam>
		/// <param name="interceptor">The action that will combine all the parameters into a single input.</param>
		/// <returns>A dictionary with all possible variants of an action.</returns>
		public static Dictionary<Type, Expression> CreateActions<T>(Action<T[]> interceptor)
		{
			return new Dictionary<Type, Expression>()
			{
				{ typeof(Expression<Action>), ((Expression<Action>)(() => interceptor(new T[] {  }))) },
				{ typeof(Expression<Action<T>>), ((Expression<Action<T>>)((p1) => interceptor(new T[] { p1 }))) },
				{ typeof(Expression<Action<T, T>>), ((Expression<Action<T, T>>)((p1, p2) => interceptor(new T[] { p1, p2 }))) },
				{ typeof(Expression<Action<T, T, T>>), ((Expression<Action<T, T, T>>)((p1, p2, p3) => interceptor(new T[] { p1, p2, p3 }))) },
				{ typeof(Expression<Action<T, T, T, T>>), ((Expression<Action<T, T, T, T>>)((p1, p2, p3, p4) => interceptor(new T[] { p1, p2, p3, p4 }))) },
				{ typeof(Expression<Action<T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T>>)((p1, p2, p3, p4, p5) => interceptor(new T[] { p1, p2, p3, p4, p5 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6) => interceptor(new T[] { p1, p2, p3, p4, p5, p6 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15 }))) },
				{ typeof(Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>>), ((Expression<Action<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16 }))) },
			};
		}
		/// <summary>
		/// Populates a dictionary with all possible variants of an Func with <typeparamref name="T"/> arguments and a return of <typeparamref name="R"/>, supplying
		/// these values to a interceptor method that will collapse all of these values into an array of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of the parameters to use for the action.</typeparam>
		/// <typeparam name="R">The type of the return value from the Func.</typeparam>
		/// <param name="interceptor">The action that will combine all the parameters into a single input.</param>
		/// <returns>A dictionary with all possible variants of a func.</returns>
		public static Dictionary<Type, Expression> CreateFuncs<T, R>(Func<T[], R> interceptor)
		{
			return new Dictionary<Type, Expression>()
			{
				{ typeof(Expression<Func<R>>), ((Expression<Func<R>>)(() => interceptor(new T[] {  }))) },
				{ typeof(Expression<Func<T, R>>), ((Expression<Func<T, R>>)((p1) => interceptor(new T[] { p1 }))) },
				{ typeof(Expression<Func<T, T, R>>), ((Expression<Func<T, T, R>>)((p1, p2) => interceptor(new T[] { p1, p2 }))) },
				{ typeof(Expression<Func<T, T, T, R>>), ((Expression<Func<T, T, T, R>>)((p1, p2, p3) => interceptor(new T[] { p1, p2, p3 }))) },
				{ typeof(Expression<Func<T, T, T, T, R>>), ((Expression<Func<T, T, T, T, R>>)((p1, p2, p3, p4) => interceptor(new T[] { p1, p2, p3, p4 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, R>>)((p1, p2, p3, p4, p5) => interceptor(new T[] { p1, p2, p3, p4, p5 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6) => interceptor(new T[] { p1, p2, p3, p4, p5, p6 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15 }))) },
				{ typeof(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, R>>), ((Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, R>>)((p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16) => interceptor(new T[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16 }))) },
			};
		}
		/// <summary>
		/// Makes the method concrete (if it is generic).
		/// </summary>
		/// <typeparam name="T">The type to substitute into each generic parameter of the method.</typeparam>
		/// <typeparam name="TLast">The type of the last type in the generic method (used for the generic return type of Funcs).</typeparam>
		/// <param name="methodInfo">The <see cref="MethodInfo"/> to make concrete.</param>
		/// <returns>The concrete <see cref="MethodInfo"/> ready for execution.</returns>
		public static MethodInfo MakeMethodConcrete<T, TLast>(this MethodInfo methodInfo)
		{
			if (methodInfo.IsGenericMethod)
			{
				var replacementType = typeof(T);
				var lastReplacementType = typeof(TLast);
				var genericArguments = methodInfo.GetGenericArguments();

				Type[] typeArray = new Type[genericArguments.Length];

				for (int i = 0; i < typeArray.Length; ++i)
				{
					var typeConstraints = genericArguments[i].GetGenericParameterConstraints();

					if (typeConstraints.Length > 0)
						typeArray[i] = typeConstraints[0];
					else if (i == (typeArray.Length - 1))
						typeArray[i] = lastReplacementType;
					else
						typeArray[i] = replacementType;
				}

				methodInfo = methodInfo.MakeGenericMethod(typeArray);
			}

			return methodInfo;
		}
		/// <summary>
		/// Tests the provided expression to ensure the reverse engineered expression using the <see cref="ExpressionConverter"/>
		/// is equivalent and valid.
		/// </summary>
		/// <param name="input"></param>
		public static void TestConverter(this Expression input)
		{
			// Compile the Expression from CSharp and retrieve it back.
			var reverseEngineered = Utility.CompileCSharpStringFromExpression(input);

			Utility.AssertCSharpStringsAreEqual(input, reverseEngineered);
		}
		/// <summary>
		/// Tests the provided expression to ensure the compiled expression produces the expected results from the provided inputs
		/// and the reverse engineered expression using the <see cref="ExpressionConverter"/> is equivalent and valid.
		/// </summary>
		/// <typeparam name="T">The type of the expected result.</typeparam>
		/// <param name="input">The input <see cref="Expression"/>.</param>
		/// <param name="expectedResult">The expected result.</param>
		/// <param name="args">The arguments for the expression.</param>
		public static void TestExpression<T>(this Expression input, T expectedResult, params object[] args)
		{
			var type = typeof(T);

			// Compile the Expression from CSharp and retrieve it back.
			var reverseEngineered = Utility.CompileCSharpStringFromExpression(input);

			Utility.AssertCSharpStringsAreEqual(input, reverseEngineered);

			var inputCompiled = ((LambdaExpression)input).Compile();
			var reverseEngineeredCompiled = ((LambdaExpression)reverseEngineered).Compile();

			var inputResult = (T)inputCompiled.DynamicInvoke(args);
			var reverseEngineeredResult = (T)reverseEngineeredCompiled.DynamicInvoke(args);

			if (typeof(ICollection).IsAssignableFrom(type))
			{
				CollectionAssert.AreEqual((ICollection)expectedResult, (ICollection)inputResult);
				CollectionAssert.AreEqual((ICollection)expectedResult, (ICollection)reverseEngineeredResult);
			}
			else if (type.IsArray)
			{
				var baseType = type.GetElementType();

				var arrayExpected = (Array)(object)expectedResult;
				var arrayInput = (Array)(object)inputResult;
				var arrayReverseEngineered = (Array)(object)reverseEngineeredResult;

				Assert.AreEqual<int>(arrayExpected.Length, arrayInput.Length);
				Assert.AreEqual<int>(arrayExpected.Length, arrayReverseEngineered.Length);

				for (int i = 0; i < arrayExpected.Length; ++i)
				{
					Assert.AreEqual(arrayExpected.GetValue(i), arrayInput.GetValue(i));
					Assert.AreEqual(arrayExpected.GetValue(i), arrayReverseEngineered.GetValue(i));
				}
			}
			else
			{
				Assert.AreEqual<T>(expectedResult, inputResult);
				Assert.AreEqual<T>(expectedResult, reverseEngineeredResult);
			}
		}
		/// <summary>
		/// Validates the <see cref="InputCase"/>s for the particular object by using reflection to call all of its methods.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> of the <see cref="IFlexpression"/> object.</typeparam>
		/// <param name="flexpression">The flexpression instance.</param>
		/// <param name="inputs">The collection of <see cref="InputCase"/>s.</param>
		public static void ValidateInputCase<T>(this T flexpression, params InputCase[] inputs) where T : IFlexpression
		{
			var flexpressionType = typeof(T);
			var lookup = inputs.ToLookup(x => x.Name);

			foreach (var method in flexpressionType.GetMethods())
			{
				foreach (var inputCase in lookup[method.Name])
				{
					if (inputCase.Arguments.Length == method.GetParameters().Length)
					{
						var methodToInvoke = method.MakeMethodConcrete<int, int>();
						var failureTriggered = false;
						var methodToInvokeParameters = methodToInvoke.GetParameters();
						var argumentsToUse = new object[methodToInvokeParameters.Length];

						// Verify all the argument types match up.
						for (int i = 0; i < methodToInvokeParameters.Length; ++i)
						{
							if ((inputCase.Arguments[i] != null)
								&& !methodToInvokeParameters[i].ParameterType.IsAssignableFrom(inputCase.Arguments[i].GetType())
								&& typeof(Expression).IsAssignableFrom(methodToInvokeParameters[i].ParameterType))
							{
								var delegateType = methodToInvokeParameters[i].ParameterType.GetGenericArguments()[0];
								var genericArguments = delegateType.GetGenericArguments();
								var numberOfParameters = (delegateType.GetMethod("Invoke").ReturnType == typeof(void)) ? genericArguments.Length : genericArguments.Length - 1;

								// If there is an argument type mismatch for expressions, build a valid expression from the provided argument.
								argumentsToUse[i] = Expression.Lambda
								(
									delegateType,
									((LambdaExpression)inputCase.Arguments[i]).Body,
									genericArguments.Take(numberOfParameters).Select(x => Expression.Parameter(x))
								);
							}
							else
							{
								argumentsToUse[i] = inputCase.Arguments[i];
							}
						}

						try
						{
							methodToInvoke.Invoke(flexpression, argumentsToUse);
						}
						catch (TargetInvocationException e)
						{
							failureTriggered = (e.GetBaseException().GetType() == inputCase.ExpectedExceptionType);
						}
						finally
						{
							Assert.IsTrue(failureTriggered, string.Format("InputCase failed (Name: {0}, Exception: {1})", inputCase.Name, inputCase.ExpectedExceptionType.GetFriendlyName()));
						}
					}
				}
			}
		}

		/// <summary>
		/// Asserts that the strings produced by the two provided expressions are equal.
		/// </summary>
		/// <param name="left">The left <see cref="Expression"/> to compare.</param>
		/// <param name="right">The right <see cref="Expression"/> to compare.</param>
		private static void AssertCSharpStringsAreEqual(Expression left, Expression right)
		{
			var inputCSharp = Utility.StripCommentsOffBeginning(left.ToCSharpString(true));
			var reverseEngineeredCSharp = Utility.StripCommentsOffBeginning(right.ToCSharpString(true));

			Assert.AreEqual<string>(inputCSharp, reverseEngineeredCSharp);
		}
		/// <summary>
		/// Takes the provided expression, constructs the equivalent C# string, and then converts it back to an <see cref="Expression"/>.
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to work on.</param>
		/// <returns>The <see cref="Expression"/> returned from the compiled C# code.</returns>
		private static Expression CompileCSharpStringFromExpression(Expression expression)
		{
			string originalCSharp = expression.ToCSharpString(true);
			string code = string.Format(@"
public static class __CompiledCode__
{{
	public static System.Linq.Expressions.Expression Run()
	{{
{0}

		return expression;
	}}
}}",
			originalCSharp);

			var parameters = new CompilerParameters();
			parameters.ReferencedAssemblies.Add("System.dll");
			parameters.ReferencedAssemblies.Add("System.Core.dll");
			parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
			parameters.ReferencedAssemblies.Add("FlexpressionsTest.dll");
			parameters.GenerateInMemory = true;

			var csProvider = new CSharpCodeProvider();
			var results = csProvider.CompileAssemblyFromSource(parameters, code);
			var assembly = results.CompiledAssembly;
			var compiledType = assembly.GetType("__CompiledCode__");

			var resultingExpression = compiledType.GetMethod("Run").Invoke(null, null) as Expression;

			return resultingExpression;
		}
		/// <summary>
		/// Strips the comments off beginning of the code snippet.
		/// </summary>
		/// <param name="code">The C# code to operate on.</param>
		/// <returns>The cleaned up C# code.</returns>
		private static string StripCommentsOffBeginning(string code)
		{
			using (var sr = new StringReader(code))
			{
				while (sr.Peek() == '/')
					sr.ReadLine();

				return sr.ReadToEnd();
			}
		}
	}
}