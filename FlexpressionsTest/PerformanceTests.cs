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
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Flexpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// The PerformanceTests class contains performance benchmarks.
	/// </summary>
	[TestClass]
	public class PerformanceTests
	{
		/// <summary>
		/// Performs a simple summation to test general functionality.
		/// </summary>
		[TestMethod]
		public void Benchmark()
		{
			const int ITERATIONS = 10000;
			long flexpressionTime, linqExpressionTime;

			Stopwatch sw = Stopwatch.StartNew();

			for (int i = 0; i < ITERATIONS; ++i)
				FlexpressionSummation();

			sw.Stop();
			flexpressionTime = sw.ElapsedMilliseconds;

			sw = Stopwatch.StartNew();

			for (int i = 0; i < ITERATIONS; ++i)
				LinqExpressionSummation();

			sw.Stop();
			linqExpressionTime = sw.ElapsedMilliseconds;

			Trace.WriteLine(string.Format("Flexpression Time: {0} ms, Linq.Expression Time: {1} ms",
				flexpressionTime,
				linqExpressionTime));
		}

		/// <summary>
		/// The Flexpressions version of a summation function.
		/// </summary>
		/// <returns>The <see cref="LambdaExpression"/> representing the summation function.</returns>
		private LambdaExpression FlexpressionSummation()
		{
			return Flexpression<Func<IEnumerable<int>, int>>
				.Create()
					.Set<int>("sum", () => 0)
					.Foreach<int, IEnumerable<int>, IEnumerable<int>>("x", p1 => p1)
						.Set<int, int, int>("sum", (x, sum) => x + sum)
					.End()
				.Return<int, int>(sum => sum)
				.CreateLambda();
		}
		/// <summary>
		/// The Linq.Expression version of a summation function.
		/// </summary>
		/// <returns>The <see cref="LambdaExpression"/> representing the summation function.</returns>
		private LambdaExpression LinqExpressionSummation()
		{
			var t1 = typeof(Int32);
			var v2 = Expression.Variable(t1, "sum");
			var t4 = typeof(IEnumerator<Int32>);
			var v3 = Expression.Variable(t4, "v3");
			var v5 = Expression.Variable(t1, "x");
			var t7 = typeof(IEnumerable<Int32>);
			var v6 = Expression.Variable(t7, "p1");
			var method8 = t7.GetMethod("GetEnumerator", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, new Type[] { }, null);
			var t9 = typeof(void);
			var t10 = typeof(IEnumerator);
			var method11 = t10.GetMethod("MoveNext", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, new Type[] { }, null);
			var property12 = t4.GetProperty("Current", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, t1, new Type[] { }, null);
			var lt13 = Expression.Label(t9, "lt13");
			var lt14 = Expression.Label(t9, "lt14");
			var t15 = typeof(IDisposable);
			var method16 = t15.GetMethod("Dispose", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy, null, new Type[] { }, null);
			var lt17 = Expression.Label(t1, "lt17");

			return Expression.Lambda<Func<IEnumerable<Int32>, Int32>>
			(
				Expression.Block
				(
					t1,
					new ParameterExpression[] { v2, v3, v5 },
					new Expression[]
					{
						Expression.MakeBinary(ExpressionType.Assign, v2, Expression.Constant(0, t1), false, null, null),
						Expression.MakeBinary(ExpressionType.Assign, v3, Expression.Call(v6, method8), false, null, null),
						Expression.MakeTry
						(
							t9,
							Expression.Loop
							(
								Expression.Condition
								(
									Expression.Call(v3, method11),
									Expression.Block
									(
										t1,
										new Expression[]
										{
											Expression.MakeBinary
											(
												ExpressionType.Assign,
												v5,
												Expression.MakeMemberAccess(v3, property12),
												false,
												null,
												null
											),
											Expression.MakeBinary
											(
												ExpressionType.Assign,
												v2,
												Expression.MakeBinary(ExpressionType.Add, v5, v2, false, null, null),
												false,
												null,
												null
											)
										}
									),
									Expression.Goto(lt13, t9),
									t9
								),
								lt13,
								lt14
							),
							Expression.Condition
							(
								Expression.MakeBinary(ExpressionType.NotEqual, v3, Expression.Constant(null, t4), false, null, null),
								Expression.Call(Expression.MakeUnary(ExpressionType.Convert, v3, t15, null), method16),
								Expression.Default(t9),
								t9
							),
							null,
							null
						),
						Expression.Goto(lt17, v2, t1),
						Expression.Label(lt17, Expression.Default(t1))
					}
				),
				"",
				false,
				new ParameterExpression[] { v6 }
			);
		}
	}
}