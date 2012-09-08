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
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Expressions.Moles;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Flexpressions.Utilities;
using Flexpressions.Utilities.Moles;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Moles.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBinder = Microsoft.CSharp.RuntimeBinder.Binder;

[assembly: MoledType(typeof(Expression))]
[assembly: MoledType(typeof(ExpressionConverter))]

namespace FlexpressionsTest.Utilities
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="ExpressionConverter"/>.
	/// </summary>
	[TestClass]
	public class ExpressionConverterTests
	{
		/// <summary>
		/// Ensures that the input is validated on the ToCSharpString method.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExpressionConverterToCSharpStringNullExpression()
		{
			new ExpressionConverter().ToCSharpString(null);
		}
		/// <summary>
		/// Ensures that an Extension <see cref="ExpressionType"/> throws a <see cref="NotImplementedException"/>.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void ExpressionConverterToCSharpStringExtensionExpression()
		{
			new ExpressionConverter().ToCSharpString(new ExtensionExpression());
		}
		/// <summary>
		/// Ensures that an unknown <see cref="CallSiteBinder"/> will throw a <see cref="NotImplementedException"/>.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void ExpressionConverterWriteBinderException()
		{
			TestExpressionConverter.TestWriteBinder(new MyCallSiteBinder());
		}
		/// <summary>
		/// Ensures that an unknown <see cref="CallSiteBinder"/> will throw a <see cref="NotImplementedException"/>.
		/// </summary>
		[TestMethod]
		[HostType("Moles")]
		public void ExpressionConverterWriteBinderSecurityException()
		{
			try
			{
				MExpressionConverter.AllInstances.WriteCallSiteBinderBinaryOperationBinder = TestMethods.ThrowSecurityException;
				var result = TestExpressionConverter.TestWriteBinder(RBinder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(ExpressionConverterTests), Enumerable.Empty<CSharpArgumentInfo>()));

				Assert.AreEqual<string>(@"// TODO: Correct reference to the CallSiteBinder - Type = CSharpBinaryOperationBinder.
null", result.Trim());
			}
			finally
			{
				MExpressionConverter.AllInstances.WriteCallSiteBinderBinaryOperationBinder = null;
			}
		}
		/// <summary>
		/// Ensures that the ToCSharpString method properly handles <see cref="SecurityException"/>s.
		/// </summary>
		[TestMethod]
		[HostType("Moles")]
		public void ExpressionConverterToCSharpStringSecurityException()
		{
			string result;

			try
			{
				MExpression.AllInstances.DebugViewGet = TestMethods.ThrowSecurityException;

				result = new ExpressionConverter().ToCSharpString(Expression.Constant(1, typeof(int)));

				Assert.AreEqual<string>(@"var t1 = typeof(Int32);

var expression = Expression.Constant(1, t1);", result);
			}
			finally
			{
				MExpression.AllInstances.DebugViewGet = null;
			}
		}
		/// <summary>
		/// Ensures that the ToCSharpString method will properly bubble up non-<see cref="SecurityException"/>s.
		/// </summary>
		[TestMethod]
		[HostType("Moles")]
		[ExpectedException(typeof(TargetInvocationException))]
		public void ExpressionConverterToCSharpStringNonSecurityException()
		{
			string result;

			try
			{
				MExpression.AllInstances.DebugViewGet = TestMethods.ThrowInvalidOperationException;

				result = new ExpressionConverter().ToCSharpString(Expression.Constant(1, typeof(int)));
			}
			finally
			{
				MExpression.AllInstances.DebugViewGet = null;
			}
		}
		/// <summary>
		/// Exercises the ExpressionConverter to ensure it can generate valid expressions from the provided expressions.
		/// </summary>
		[TestMethod]
		public void ExpressionConverterExercise()
		{
			var parameter1 = Expression.Variable(typeof(int));
			var parameter2 = Expression.Variable(typeof(string));
			var parameter3 = Expression.Variable(typeof(StringBuilder));

			foreach (var input in new ConverterTestInput[]
			{
				new ConverterTestInput()
				{
					Expression = (Expression)Expression.Lambda<Func<bool>>(Expression.TypeEqual(Expression.Constant(42, typeof(int)), typeof(int))),
					ExpectedResult = true
				},
				new ConverterTestInput()
				{
					Expression = (Expression)Expression.Lambda<Func<bool>>(Expression.TypeIs(Expression.Constant(42, typeof(int)), typeof(int))),
					ExpectedResult = true
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<int>>) (() => (new int[10]).Length),
					ExpectedResult = 10
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<bool>>(Expression.Block(Expression.DebugInfo(Expression.SymbolDocument("test.cs", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()), 1, 2, 3, 4), Expression.Constant(true, typeof(bool)))),
					ExpectedResult = true
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<string>>)(() => (string)null ?? ((Func<string>)(() => "a"))()),
					ExpectedResult = "a"
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<decimal>>)(() => int.Parse("123")),
					ExpectedResult = 123M
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<int>>)(() => new StringBuilder() { Capacity = 100 }.Capacity),
					ExpectedResult = 100
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<int>>)(() => new Dictionary<string, int>() { { "abc", 1 }, { "def", 42 } }["def"]),
					ExpectedResult = 42
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<int>>)(() => new List<int>() { 1, 2, 3, 4, 5 }.Count),
					ExpectedResult = 5
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<string>>)(() => new Node() { Data = new NodeData() { Name = "Wrong" }, Children = { new Node() { Data = { Name = "Wrong2" }, Children = { new Node() { Data = new NodeData() { Name = "Wrong3" } }, new Node() { Data = new NodeData() { Name = "Right" } } } } } }.Children[0].Children[1].Data.Name),
					ExpectedResult = "Right"
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Block
						(
							new ParameterExpression[] { parameter1, parameter2 },
							Expression.Assign(parameter1, Expression.Constant(12345)),
							Expression.Property(Expression.RuntimeVariables(parameter1, parameter2), "Count")
						)
					),
					ExpectedResult = 2
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Switch
						(
							Expression.Constant(1, typeof(int)),
							Expression.Constant(-1, typeof(int)),
							typeof(TestMethods).GetMethod("IntComparer"),
							Expression.SwitchCase
							(
								Expression.Constant(50, typeof(int)),
								Expression.Constant(0, typeof(int))
							),
							Expression.SwitchCase
							(
								Expression.Constant(543, typeof(int)),
								Expression.Constant(1, typeof(int))
							)
						)
					),
					ExpectedResult = 543
				},
				new ConverterTestInput()
				{
					Expression = (Expression<Func<dynamic>>)(() => (dynamic)5),
					ExpectedResult = 5
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.TryFault
						(
							Expression.Call
							(
								typeof(int).GetMethod("Parse", new Type[] { typeof(string) }),
								Expression.Constant("ab", typeof(string))
							),
							Expression.Constant(123, typeof(int))
						)
					),
					ExpectedResult = 123,
					ExpectedExceptionType = typeof(NotSupportedException)
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.ArrayAccess
						(
							Expression.NewArrayInit
							(
								typeof(int),
								Expression.Constant(1, typeof(int)),
								Expression.Constant(2, typeof(int)),
								Expression.Constant(3, typeof(int))
							),
							Expression.Constant(1, typeof(int))
						)
					),
					ExpectedResult = 2
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Property
						(
							Expression.New(typeof(NodeData)),
							typeof(NodeData).GetProperty("Item"),
							Expression.Constant(5001, typeof(int))
						)
					),
					ExpectedResult = 5001
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<string>>
					(
						Expression.Coalesce
						(
							Expression.Constant(null, typeof(string)),
							Expression.Constant("abcd", typeof(string)),
							Expression.Lambda<Func<string, string>>
							(
								Expression.Constant("Blah", typeof(string)),
								parameter2
							)
						)
					),
					ExpectedResult = "abcd"
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.TryCatch
						(
							Expression.Call
							(
								typeof(int).GetMethod("Parse", new Type[] { typeof(string) }),
								Expression.Constant("ab", typeof(string))
							),
							Expression.MakeCatchBlock(typeof(int), null, Expression.Constant(456, typeof(int)), Expression.Constant(true, typeof(bool)))
						)
					),
					ExpectedResult = 456,
					ExpectedExceptionType = typeof(NotSupportedException)
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Dynamic
						(
							RBinder.Convert
							(
								CSharpBinderFlags.ConvertExplicit | CSharpBinderFlags.CheckedContext,
								typeof(int),
								typeof(ExpressionConverterTests)
							),
							typeof(int),
							Expression.Dynamic
							(
								RBinder.BinaryOperation
								(
									CSharpBinderFlags.None,
									ExpressionType.Add,
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "right")
									}
								),
								typeof(object),
								Expression.Constant(1, typeof(int)),
								Expression.Constant(2, typeof(int))
							)
						)
					),
					ExpectedResult = 3
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Dynamic
						(
							RBinder.Convert
							(
								CSharpBinderFlags.None,
								typeof(int),
								typeof(ExpressionConverterTests)
							),
							typeof(int),
							Expression.Dynamic
							(
								RBinder.UnaryOperation
								(
									CSharpBinderFlags.CheckedContext,
									ExpressionType.Increment,
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}
								),
								typeof(object),
								Expression.Dynamic
								(
									RBinder.UnaryOperation
									(
										CSharpBinderFlags.None,
										ExpressionType.Increment,
										typeof(ExpressionConverterTests),
										new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}
									),
									typeof(object),
									Expression.Constant(1, typeof(int))
								)
							)
						)
					),
					ExpectedResult = 3
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<string>>
					(
						Expression.Property
						(
							Expression.New
							(
								typeof(NodeData).GetConstructor(new Type[] { typeof(string) }),
								new Expression[] { Expression.Constant("zdfe", typeof(string)) },
								new MemberInfo[] { typeof(NodeData).GetProperty("Name") }
							),
							"Name"
						)
					),
					ExpectedResult = "zdfe"
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Convert
						(
							Expression.Dynamic
							(
								RBinder.GetMember
								(
									CSharpBinderFlags.None,
									"Capacity",
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}
								),
								typeof(object),
								Expression.Dynamic
								(
									RBinder.InvokeConstructor
									(
										CSharpBinderFlags.None,
										typeof(ExpressionConverterTests),
										new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null)
										}
									),
									typeof(object),
									Expression.Constant(typeof(StringBuilder), typeof(Type))
								)
							),
							typeof(int)
						)
					),
					ExpectedResult = new StringBuilder().Capacity
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<bool>>
					(
						Expression.Dynamic
						(
							RBinder.IsEvent
							(
								CSharpBinderFlags.None,
								"Capacity",
								typeof(ExpressionConverterTests)
							),
							typeof(bool),
							Expression.New(typeof(StringBuilder))
						)
					),
					ExpectedResult = false
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Convert
						(
							Expression.Dynamic
							(
								RBinder.SetMember
								(
									CSharpBinderFlags.None,
									"Capacity",
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null),
									}
								),
								typeof(object),
								Expression.New(typeof(StringBuilder)),
								Expression.Constant(123456, typeof(int))
							),
							typeof(int)
						)
					),
					ExpectedResult = 123456
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Block
						(
							typeof(int),
							new ParameterExpression[] { parameter3 },
							Expression.Assign(parameter3, Expression.New(typeof(StringBuilder))),
							Expression.Convert
							(
								Expression.Dynamic
								(
									RBinder.SetMember
									(
										CSharpBinderFlags.ValueFromCompoundAssignment | CSharpBinderFlags.CheckedContext,
										"Capacity",
										typeof(ExpressionConverterTests),
										new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										}
									),
									typeof(object),
									parameter3,
									Expression.Dynamic
									(
										RBinder.BinaryOperation
										(
											CSharpBinderFlags.CheckedContext,
											ExpressionType.AddAssign,
											typeof(ExpressionConverterTests),
											new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}
										),
										typeof(object),
										Expression.Dynamic
										(
											RBinder.GetMember
											(
												CSharpBinderFlags.ResultIndexed,
												"Capacity",
												typeof(ExpressionConverterTests),
												new CSharpArgumentInfo[]
												{
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
												}
											),
											typeof(object),
											parameter3
										),
										Expression.Constant(1, typeof(int))
									)
								),
								typeof(int)
							)
						)
					),
					ExpectedResult = 17
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.ArrayIndex
						(
							Expression.NewArrayInit
							(
								typeof(int),
								Expression.Constant(1, typeof(int)),
								Expression.Constant(2, typeof(int)),
								Expression.Constant(3, typeof(int)),
								Expression.Constant(4, typeof(int)),
								Expression.Constant(5, typeof(int))
							),
							Expression.Dynamic
							(
								RBinder.Convert
								(
									CSharpBinderFlags.ConvertArrayIndex,
									typeof(int),
									typeof(ExpressionConverterTests)
								),
								typeof(int),
								Expression.Dynamic
								(
									RBinder.BinaryOperation
									(
										CSharpBinderFlags.None,
										ExpressionType.Add,
										typeof(ExpressionConverterTests),
										new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}
									),
									typeof(object),
									Expression.Constant(1, typeof(int)),
									Expression.Constant(2, typeof(int))
								)
							)
						)
					),
					ExpectedResult = 4
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<bool>>
					(
						Expression.Dynamic
						(
							RBinder.Convert
							(
								CSharpBinderFlags.None,
								typeof(bool),
								typeof(ExpressionConverterTests)
							),
							typeof(bool),
							Expression.Dynamic
							(
								RBinder.BinaryOperation
								(
									CSharpBinderFlags.BinaryOperationLogical,
									ExpressionType.ExclusiveOr,
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
									}
								),
								typeof(object),
								Expression.Constant(true, typeof(bool)),
								Expression.Constant(false, typeof(bool))
							)
						)
					),
					ExpectedResult = true
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<bool>>
					(
						Expression.Dynamic
						(
							RBinder.Convert
							(
								CSharpBinderFlags.None,
								typeof(bool),
								typeof(ExpressionConverterTests)
							),
							typeof(bool),
							Expression.Dynamic
							(
								RBinder.BinaryOperation
								(
									CSharpBinderFlags.BinaryOperationLogical | CSharpBinderFlags.CheckedContext,
									ExpressionType.ExclusiveOr,
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
									}
								),
								typeof(object),
								Expression.Constant(true, typeof(bool)),
								Expression.Constant(false, typeof(bool))
							)
						)
					),
					ExpectedResult = true
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Block
						(
							typeof(int),
							new ParameterExpression[] { parameter3 },
							Expression.Assign(parameter3, Expression.New(typeof(StringBuilder))),
							Expression.Dynamic
							(
								RBinder.InvokeMember
								(
									CSharpBinderFlags.ResultDiscarded,
									"Append",
									null,
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}
								),
								typeof(object),
								parameter3,
								Expression.Constant("Append", typeof(string))
							),
							Expression.MakeMemberAccess(parameter3, typeof(StringBuilder).GetProperty("Length"))
						)
					),
					ExpectedResult = 6
				},
				new ConverterTestInput()
				{
					Expression = Expression.Lambda<Func<int>>
					(
						Expression.Block
						(
							typeof(int),
							new ParameterExpression[] { parameter3 },
							Expression.Assign(parameter3, Expression.New(typeof(StringBuilder))),
							Expression.Dynamic
							(
								RBinder.InvokeMember
								(
									CSharpBinderFlags.InvokeSimpleName,
									"Append",
									null,
									typeof(ExpressionConverterTests),
									new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}
								),
								typeof(object),
								parameter3,
								Expression.Constant("Append", typeof(string))
							),
							Expression.MakeMemberAccess(parameter3, typeof(StringBuilder).GetProperty("Length"))
						)
					),
					ExpectedResult = 6
				}
			})
			{
				var typedMethodExpression = typeof(Utility).GetMethod("TestExpression").MakeGenericMethod(input.ExpectedResult.GetType());
				var exceptionExpected = (input.ExpectedExceptionType != null);
				var exceptionCaught = false;

				try
				{
					typedMethodExpression.Invoke(null, new object[] { input.Expression, input.ExpectedResult, new object[0] });
				}
				catch (TargetInvocationException e)
				{
					var innerException = e.GetBaseException();

					exceptionCaught = true;

					// Catch Filters and Fault blocks are not supported by the CLR for dynamic methods, but the
					// code can still ensure fidelity in producing the trees in case this ever changes.
					if (!exceptionExpected || (innerException.GetType() != input.ExpectedExceptionType))
						throw;
				}

				if (exceptionExpected && !exceptionCaught)
					Assert.Fail("An exception was expected, but none was thrown.");
			}
		}
		/// <summary>
		/// Exercises the ExpressionConverter to ensure it properly throws an error when invalid input is supplied.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void ExpressionConverterMemberInfoBadInput()
		{
			TestExpressionConverter.TestGetMemberInfoString(new FunkyMemberInfo());
		}
		/// <summary>
		/// Exercises the ExpressionConverter to ensure it can generate valid <see cref="MemberInfo"/> strings.
		/// </summary>
		[TestMethod]
		public void ExpressionConverterMemberInfoExercise()
		{
			foreach (var input in new[]
			{
				new
				{
					MemberInfo = (MemberInfo)typeof(TestMethods).GetConstructor(BindingFlags.NonPublic | BindingFlags.Static, null, Type.EmptyTypes, null),
					Item1 = (string)"constructor2",
					Item2 = (string)@"var t1 = typeof(TestMethods);
var constructor2 = t1.GetConstructor(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, new Type[] {  }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(int) }, null),
					Item1 = (string)"constructor2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var t3 = typeof(String);
var t4 = typeof(Int32);
var constructor2 = t1.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, new Type[] { t3, t4 }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetMethod("Method1", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(TestClass) }, null),
					Item1 = (string)"method2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var method2 = t1.GetMethod(""Method1"", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, new Type[] { t1 }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetMethod("Method2", BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null),
					Item1 = (string)"method2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var method2 = t1.GetMethod(""Method2"", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, new Type[] {  }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetField("Field1", BindingFlags.NonPublic | BindingFlags.Static),
					Item1 = (string)"field2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var field2 = t1.GetField(""Field1"", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetField("Field2", BindingFlags.Public | BindingFlags.Instance),
					Item1 = (string)"field2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var field2 = t1.GetField(""Field2"", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetEvent("Event1", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance),
					Item1 = (string)"event2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var event2 = t1.GetEvent(""Event1"", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetProperty("Property1", BindingFlags.Public | BindingFlags.Static, null, typeof(TestClass), Type.EmptyTypes, null),
					Item1 = (string)"property2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var property2 = t1.GetProperty(""Property1"", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy, null, t1, new Type[] {  }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetProperty("Property2", BindingFlags.Public | BindingFlags.Instance, null, typeof(int), Type.EmptyTypes, null),
					Item1 = (string)"property2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var t3 = typeof(Int32);
var property2 = t1.GetProperty(""Property2"", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, t3, new Type[] {  }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetProperty("Property3", BindingFlags.NonPublic | BindingFlags.Instance, null, typeof(string), Type.EmptyTypes, null),
					Item1 = (string)"property2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var t3 = typeof(String);
var property2 = t1.GetProperty(""Property3"", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, t3, new Type[] {  }, null);"
				},
				new
				{
					MemberInfo = (MemberInfo)typeof(TestClass).GetProperty("Item", BindingFlags.Public | BindingFlags.Instance, null, typeof(int), new Type[] { typeof(int), typeof(int) }, null),
					Item1 = (string)"property2",
					Item2 = (string)@"var t1 = typeof(TestClass);
var t3 = typeof(Int32);
var property2 = t1.GetProperty(""Item"", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, t3, new Type[] { t3, t3 }, null);"
				},
			})
			{
				var results = TestExpressionConverter.TestGetMemberInfoString(input.MemberInfo);

				Assert.AreEqual<string>(input.Item1, results.Item1);
				Assert.AreEqual<string>(input.Item2, results.Item2.Trim());
			}
		}
		/// <summary>
		/// Exercises the ExpressionConverter to ensure it can generate valid <see cref="CallSiteBinder"/> strings.
		/// </summary>
		[TestMethod]
		public void ExpressionConverterWriteBinderExercise()
		{
			foreach (var input in new[]
			{
				new
				{
					CallSiteBinder = RBinder.InvokeMember(CSharpBinderFlags.InvokeSpecialName, "Name", new Type[] { typeof(int), typeof(string) }, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "abcd") }),
					Result = (string)@"var t1 = typeof(Int32);
var t2 = typeof(String);
var t3 = typeof(ExpressionConverter);

Binder.InvokeMember
(
	CSharpBinderFlags.InvokeSpecialName,
	""Name"",
	new Type[]
	{
		t1,
		t2
	},
	t3,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			""abcd""
		)
	}
)"
				},
				new
				{
					CallSiteBinder = RBinder.InvokeMember(CSharpBinderFlags.None, "Name", new Type[] { typeof(int), typeof(string) }, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "abcd") }),
					Result = (string)@"var t1 = typeof(Int32);
var t2 = typeof(String);
var t3 = typeof(ExpressionConverter);

Binder.InvokeMember
(
	CSharpBinderFlags.None,
	""Name"",
	new Type[]
	{
		t1,
		t2
	},
	t3,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			""abcd""
		)
	}
)"
				},
				new
				{
					CallSiteBinder = RBinder.GetIndex(CSharpBinderFlags.None, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "defgh") }),
					Result = (string)@"var t1 = typeof(ExpressionConverter);

Binder.GetIndex
(
	CSharpBinderFlags.None,
	t1,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			""defgh""
		)
	}
)"
				},
				new
				{
					CallSiteBinder = RBinder.Invoke(CSharpBinderFlags.None, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "defgh") }),
					Result = (string)@"var t1 = typeof(ExpressionConverter);

Binder.Invoke
(
	CSharpBinderFlags.None,
	t1,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			""defgh""
		)
	}
)"
				},
				new
				{
					CallSiteBinder = RBinder.Invoke(CSharpBinderFlags.ResultDiscarded, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, "defgh") }),
					Result = (string)@"var t1 = typeof(ExpressionConverter);

Binder.Invoke
(
	CSharpBinderFlags.ResultDiscarded,
	t1,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			""defgh""
		)
	}
)"
				},
				new
				{
					CallSiteBinder = RBinder.SetIndex(CSharpBinderFlags.CheckedContext | CSharpBinderFlags.ValueFromCompoundAssignment, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }),
					Result = (string)@"var t1 = typeof(ExpressionConverter);

Binder.SetIndex
(
	CSharpBinderFlags.CheckedContext | CSharpBinderFlags.ValueFromCompoundAssignment,
	t1,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			null
		),
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			null
		)
	}
)"
				},
				new
				{
					CallSiteBinder = RBinder.SetIndex(CSharpBinderFlags.None, typeof(ExpressionConverter), new [] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }),
					Result = (string)@"var t1 = typeof(ExpressionConverter);

Binder.SetIndex
(
	CSharpBinderFlags.None,
	t1,
	new CSharpArgumentInfo[]
	{
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			null
		),
		CSharpArgumentInfo.Create
		(
			CSharpArgumentInfoFlags.None,
			null
		)
	}
)"
				}
			})
			{
				var result = TestExpressionConverter.TestWriteBinder(input.CallSiteBinder);

				Assert.AreEqual<string>(input.Result, result.Trim());
			}
		}

		#region Unit Test Classes

		/// <summary>
		/// The ConverterTestInput class is used to validate the <see cref="ExpressionConverter"/>.
		/// </summary>
		private class ConverterTestInput
		{
			/// <summary>
			/// The <see cref="Expression"/> to test.
			/// </summary>
			public Expression Expression;
			/// <summary>
			/// The expected result of the expression.
			/// </summary>
			public object ExpectedResult;
			/// <summary>
			/// The expected exception type.
			/// </summary>
			public Type ExpectedExceptionType;
		}

		/// <summary>
		/// The Node class is used for testing various expression constructs.
		/// </summary>
		public class Node
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="Node" /> class.
			/// </summary>
			public Node()
			{
				this.Children = new List<Node>();
				this.Data = new NodeData();
			}

			/// <summary>
			/// Gets or sets the <see cref="NodeData"/>.
			/// </summary>
			public NodeData Data { get; set; }
			/// <summary>
			/// Gets or sets the children.
			/// </summary>
			public List<Node> Children { get ; set; }
		}

		/// <summary>
		/// The NodeData class is used for testing various expression constructs.
		/// </summary>
		public class NodeData
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="NodeData" /> class.
			/// </summary>
			public NodeData()
			{
				/* Intentionally left blank. */
			}
			/// <summary>
			/// Initializes a new instance of the <see cref="NodeData" /> class.
			/// </summary>
			/// <param name="name">The name.</param>
			public NodeData(string name)
			{
				this.Name = name;
			}

			/// <summary>
			/// Gets or sets the name.
			/// </summary>
			public string Name { get; set; }
			/// <summary>
			/// Gets the <see cref="System.Int32" /> at the specified index.
			/// </summary>
			public int this[int index]
			{
				get
				{
					return index;
				}
			}
			/// <summary>
			/// The GenericMethod used to test Dynamic's InvokeMember.
			/// </summary>
			/// <typeparam name="T">The generic type of the method.</typeparam>
			/// <param name="value">The string value provided to the method.</param>
			/// <returns>The concatenation of the generic type's name and the value.</returns>
			public string GenericMethod<T>(string value)
			{
				return typeof(T).Name + value;
			}
		}

		/// <summary>
		/// The TestMethods is used for testing various expression constructs.
		/// </summary>
		public static class TestMethods
		{
			/// <summary>
			/// Initializes the <see cref="TestMethods" /> class.
			/// </summary>
			static TestMethods()
			{
				/* Intentionally left blank. */
			}

			/// <summary>
			/// Compares two integers.
			/// </summary>
			/// <param name="x">The x.</param>
			/// <param name="y">The y.</param>
			/// <returns>True - if the two integers are equal; False - otherwise.</returns>
			public static bool IntComparer(int x, int y)
			{
				return (x == y);
			}
			/// <summary>
			/// Used to override the DebugView property of Expressions, mimicing a SecurityException being thrown.
			/// </summary>
			/// <returns>Nothing, a SecurityException is always thrown.</returns>
			public static string ThrowSecurityException(Expression expression)
			{
				throw new SecurityException();
			}
			/// <summary>
			/// Used to override the DebugView property of Expressions, mimicing a InvalidOperationException being thrown.
			/// </summary>
			/// <returns>Nothing, a InvalidOperationException is always thrown.</returns>
			public static string ThrowInvalidOperationException(Expression expression)
			{
				throw new InvalidOperationException();
			}
			/// <summary>
			/// Used to force a SecurityException during the reflection process of the ExpressionConverter when dealing with <see cref="CallBindSite"/> instances.
			/// </summary>
			/// <param name="converter">The converter under test.</param>
			/// <param name="binder">The dummy binder used to invoke the Exception.</param>
			public static void ThrowSecurityException(ExpressionConverter converter, BinaryOperationBinder binder)
			{
				throw new SecurityException();
			}
		}

		/// <summary>
		/// The TestClass is used to test various operations within the <see cref="ExpressionConverter"/>.
		/// </summary>
		public class TestClass
		{
			/// <summary>
			/// Method1 is a test method for <see cref="TestExpressionConverter"/>.
			/// </summary>
			/// <param name="instance">The instance.</param>
			public static void Method1(TestClass instance)
			{
				++Field1;
			}

			/// <summary>
			/// Field1 is a test field for <see cref="TestExpressionConverter"/>.
			/// </summary>
			private static int Field1;

			/// <summary>
			/// Property1 is a test property for <see cref="TestExpressionConverter"/>.
			/// </summary>
			public static TestClass Property1 { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="TestClass" /> class.
			/// </summary>
			/// <param name="a">A string.</param>
			/// <param name="b">An integer.</param>
			public TestClass(string a, int b)
			{
				this.Field2 = new Guid(a);

				if (this.Event1 != null)
					this.Event1(null, EventArgs.Empty);
			}

			/// <summary>
			/// Event1 is a test event for <see cref="TestExpressionConverter"/>.
			/// </summary>
			public event EventHandler Event1;

			/// <summary>
			/// Field2 is a test field for <see cref="TestExpressionConverter"/>.
			/// </summary>
			public Guid Field2;

			/// <summary>
			/// Property2 is a test property for <see cref="TestExpressionConverter"/>.
			/// </summary>
			public int Property2 { get { return 42; } }

			/// <summary>
			/// Property3 is a test property for <see cref="TestExpressionConverter"/>.
			/// </summary>
			private string Property3 { set { this.Field2 = new Guid(value); } }

			/// <summary>
			/// This is a test property for <see cref="TestExpressionConverter"/>.
			/// </summary>
			public int this[int index1, int index2]
			{
				get
				{
					return index1 + index2;
				}
			}

			/// <summary>
			/// Method2 is a test method for <see cref="TestExpressionConverter"/>.
			/// </summary>
			private void Method2()
			{
				/* Intentionally left blank. */
			}
		}

		/// <summary>
		/// FunkyMemberInfo is a test <see cref="MemberInfo"/> for <see cref="TestExpressionConverter"/>.
		/// </summary>
		public class FunkyMemberInfo : MemberInfo
		{
			/// <summary>
			/// Gets the class that declares this member.
			/// </summary>
			/// <returns>The Type object for the class that declares this member.</returns>
			public override Type DeclaringType
			{
				get { return typeof(int); }
			}

			/// <summary>
			/// When overridden in a derived class, returns an array of custom attributes identified by <see cref="T:System.Type"></see>.
			/// </summary>
			/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
			/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
			/// <returns>
			/// An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.
			/// </returns>
			/// <exception cref="System.NotImplementedException">This method always throws this exception.</exception>
			public override object[] GetCustomAttributes(Type attributeType, bool inherit)
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// When overridden in a derived class, returns an array containing all the custom attributes.
			/// </summary>
			/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
			/// <returns>
			/// An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.
			/// </returns>
			/// <exception cref="System.NotImplementedException">This method always throws this exception.</exception>
			public override object[] GetCustomAttributes(bool inherit)
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// When overridden in a derived class, indicates whether one or more instance of attributeType is applied to this member.
			/// </summary>
			/// <param name="attributeType">The Type object to which the custom attributes are applied.</param>
			/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
			/// <returns>
			/// true if one or more instance of attributeType is applied to this member; otherwise false.
			/// </returns>
			/// <exception cref="System.NotImplementedException">This method always throws this exception.</exception>
			public override bool IsDefined(Type attributeType, bool inherit)
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// When overridden in a derived class, gets a <see cref="T:System.Reflection.MemberTypes"></see> value indicating the type of the member — method, constructor, event, and so on.
			/// </summary>
			/// <returns>A <see cref="T:System.Reflection.MemberTypes"></see> value indicating the type of member.</returns>
			/// <exception cref="System.NotImplementedException">This method always throws this exception.</exception>
			public override MemberTypes MemberType
			{
				get { throw new NotImplementedException(); }
			}

			/// <summary>
			/// Gets the name of the current member.
			/// </summary>
			/// <returns>A <see cref="T:System.String"></see> containing the name of this member.</returns>
			/// <exception cref="System.NotImplementedException">This method always throws this exception.</exception>
			public override string Name
			{
				get { throw new NotImplementedException(); }
			}

			/// <summary>
			/// Gets the class object that was used to obtain this instance of MemberInfo.
			/// </summary>
			/// <returns>The Type object through which this MemberInfo object was obtained.</returns>
			/// <exception cref="System.NotImplementedException">This method always throws this exception.</exception>
			public override Type ReflectedType
			{
				get { throw new NotImplementedException(); }
			}
		}

		/// <summary>
		/// The TestExpressionConverter exposes out methods to make unit testing the <see cref="ExpressionConverter"/> easier.
		/// </summary>
		public class TestExpressionConverter : ExpressionConverter
		{
			/// <summary>
			/// Gets the <see cref="MemberInfo"/> strings.
			/// </summary>
			/// <param name="memberInfo">The <see cref="MemberInfo"/> to get the strings for.</param>
			/// <returns>The <see cref="Tuple&gt;T1, T2&lt;"/> containing the variable name in Item1 and the declaration in Item2.</returns>
			public static Tuple<string, string> TestGetMemberInfoString(MemberInfo memberInfo)
			{
				var instance = new TestExpressionConverter();
				var name = instance.GetMemberInfoString(memberInfo);

				instance.InsertDeclarations();

				return Tuple.Create(name, instance.sb.ToString());
			}
			/// <summary>
			/// Writes the <see cref="CallSiteBinder"/> to write.
			/// </summary>
			/// <param name="callSiteBinder">The <see cref="CallSiteBinder"/> to write.</param>
			/// <returns>The string produced by reverse engineering the <see cref="CallSiteBinder"/>.</returns>
			public static string TestWriteBinder(CallSiteBinder callSiteBinder)
			{
				var instance = new TestExpressionConverter();
				instance.WriteBinder(callSiteBinder);

				instance.InsertDeclarations();

				return instance.sb.ToString();
			}
		}

		/// <summary>
		/// The ExtensionExpression is used to test that the Extension <see cref="ExpressionType"/> is properly handled.
		/// </summary>
		public class ExtensionExpression : Expression
		{
			/// <summary>
			/// Gets the node type of this <see cref="T:System.Linq.Expressions.Expression" />.
			/// </summary>
			/// <returns>One of the <see cref="T:System.Linq.Expressions.ExpressionType" /> values.</returns>
			public override ExpressionType NodeType
			{
				get
				{
					return ExpressionType.Extension;
				}
			}
		}

		/// <summary>
		/// The MyCallSiteBinder class is to ensure that the <see cref="ExpressionConverter"/> properly throws an exception on an unsupported <see cref="CallSiteBinder"/>.
		/// </summary>
		public class MyCallSiteBinder : DynamicMetaObjectBinder
		{
			/// <summary>
			/// When overridden in the derived class, performs the binding of the dynamic operation.
			/// </summary>
			/// <param name="target">The target of the dynamic operation.</param>
			/// <param name="args">An array of arguments of the dynamic operation.</param>
			/// <returns>
			/// The <see cref="T:System.Dynamic.DynamicMetaObject" /> representing the result of the binding.
			/// </returns>
			/// <exception cref="System.NotImplementedException"></exception>
			public override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
			{
				throw new NotImplementedException();
			}
		}

		#endregion Unit Test Classes
	}
}