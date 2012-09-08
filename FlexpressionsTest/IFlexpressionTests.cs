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
using Flexpressions.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="IFlexpression"/>.
	/// </summary>
	[TestClass]
	public class IFlexpressionTests
	{
		/// <summary>
		/// Performs general sweep of AllowRethrow IFlexpression methods with most elements outside of a Catch block.
		/// </summary>
		[TestMethod]
		public void IFlexpressionAllowRethrowNotWithinCatch()
		{
			var loopBlock = Flexpression<Action<int>>.Create().Do(() => true);
			var usingBlock = loopBlock.Using("iter", () => Enumerable.Empty<int>().GetEnumerator());
			var switchBlock = usingBlock.Switch(() => 5);
			var switchCaseBlock = switchBlock.Case(() => 5);
			var block = switchCaseBlock.Begin().Try().If(() => true);
			var ifBlock = block.Act(() => Console.WriteLine("Fail!")).End();
			var catchBlock = ifBlock.EndIf().End().Catch();
			var tryBlock = catchBlock.End();

			foreach (IFlexpression fe in new IFlexpression[]
			{
				loopBlock,
				usingBlock,
				switchBlock,
				switchCaseBlock,
				block,
				ifBlock,
				catchBlock,
				tryBlock
			})
			{
				if (fe == catchBlock)
					Assert.IsTrue(fe.AllowRethrow());
				else
					Assert.IsFalse(fe.AllowRethrow());
			}
		}
		/// <summary>
		/// Performs general sweep of AllowRethrow IFlexpression methods with all elements inside of a Catch block.
		/// </summary>
		[TestMethod]
		public void IFlexpressionAllowRethrowWithinCatch()
		{
			var loopBlock = Flexpression<Action<int>>.Create().Try().End().Catch().Do(() => true);
			var usingBlock = loopBlock.Using("iter", () => Enumerable.Empty<int>().GetEnumerator());
			var switchBlock = usingBlock.Switch(() => 5);
			var switchCaseBlock = switchBlock.Case(() => 5);
			var block = switchCaseBlock.Begin().Try().If(() => true);
			var ifBlock = block.Act(() => Console.WriteLine("Fail!")).End();
			var catchBlock = ifBlock.EndIf().End().Catch();
			var tryBlock = catchBlock.End();

			foreach (IFlexpression fe in new IFlexpression[]
			{
				loopBlock,
				usingBlock,
				switchBlock,
				switchCaseBlock,
				block,
				ifBlock,
				catchBlock,
				tryBlock
			})
			{
				Assert.IsTrue(fe.AllowRethrow());
			}
		}
		/// <summary>
		/// Performs general sweep of all IFlexpression operations, except for AllowRethrow.
		/// </summary>
		[TestMethod]
		public void IFlexpressionExercise()
		{
			foreach (var allowOuterVariables in new bool[] { false, true })
			{
				var counter = 0;
				var loopBlock = Flexpression<Action<int>>.Create(allowOuterVariables).Do(() => true);
				var usingBlock = loopBlock.Using("iter", () => Enumerable.Empty<int>().GetEnumerator());
				var switchBlock = usingBlock.Switch(() => 5);
				var switchCaseBlock = switchBlock.Case(() => 5);
				var block = switchCaseBlock.Begin().Try().If(() => true);
				var ifBlock = block.Act(() => Console.WriteLine("Fail!")).End();
				var catchBlock = ifBlock.EndIf().End().Catch();
				var tryBlock = catchBlock.End();
				var flexpression = tryBlock.EndTry().End().EndSwitch().End().End().End();

				foreach (IFlexpression fe in new IFlexpression[]
				{
					loopBlock,
					usingBlock,
					switchBlock,
					switchCaseBlock,
					block,
					ifBlock,
					catchBlock,
					tryBlock
				})
				{
					Assert.AreEqual<bool>(flexpression.AllowOuterVariables(), fe.AllowOuterVariables());
					Assert.AreSame(flexpression.GetReturnLabel(), fe.GetReturnLabel());
					Assert.AreSame(loopBlock.GetLoopLabel(false), fe.GetLoopLabel(false));
					Assert.AreSame(loopBlock.GetLoopLabel(true), fe.GetLoopLabel(true));

					var variablesInScope = fe.GetVariablesInScope().ToArray();
					Assert.AreEqual<int>(2, variablesInScope.Length);
					Assert.IsTrue(variablesInScope.Any(x => x.Name == "iter"));
					Assert.IsTrue(variablesInScope.Any(x => x.Name == "p1"));

					for (int i = 0; i < 5; ++i)
					{
						// Make sure both are at the same point at the start.
						var beforeLabelCountFlexpression = flexpression.GetLabelTargets().Count();
						var beforeLabelCountFe = fe.GetLabelTargets().Count();

						Assert.AreEqual<int>(beforeLabelCountFlexpression, beforeLabelCountFe);

						// Declare a new label.
						var labelTarget = Expression.Label(typeof(void), string.Format("label{0}", ++counter));
						fe.DeclareLabelTarget(labelTarget);

						// Make sure the label count is the same (and only incremented by one).
						var afterLabelCountFlexpression = flexpression.GetLabelTargets().Count();
						var afterLabelCountFe = fe.GetLabelTargets().Count();

						Assert.AreEqual<int>(afterLabelCountFlexpression, afterLabelCountFe);
						Assert.IsTrue(flexpression.GetLabelTargets().Contains(labelTarget));
						Assert.IsTrue(fe.GetLabelTargets().Contains(labelTarget));
					}
				}
			}
		}
	}
}