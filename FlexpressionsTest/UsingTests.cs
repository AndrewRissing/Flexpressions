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
using Flexpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexpressionsTest
{
	/// <summary>
	/// Contains all of the unit tests pertaining to the <see cref="Using&lt;TParent&gt;"/>.
	/// </summary>
	[TestClass]
	public class UsingTests
	{
		/// <summary>
		/// Constructs a using statement using a referenec type and a value type to ensure that each is disposed.
		/// </summary>
		[TestMethod]
		public void UsingCreateExpressionTest()
		{
			var classDisposed = false;
			Action onClassDispose = () => classDisposed = true;

			var structDisposed = false;
			Action onStructDispose = () => structDisposed = true;

			var expression = Flexpression<Action>
				.Create(true)
					.Using("class", () => new ClassDisposable(onClassDispose))
						.Using("struct", () => new StructDisposable(onStructDispose))
							.Act(() => Console.WriteLine("No whammies!"))
							.End()
					.End()
				.End()
				.Compile();

			expression();

			Assert.IsTrue(classDisposed);
			Assert.IsTrue(structDisposed);
		}

		#region ClassDisposable class

		/// <summary>
		/// The ClassDisposable class is used to test the using statement with a class.
		/// </summary>
		private class ClassDisposable : IDisposable
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="ClassDisposable" /> class.
			/// </summary>
			public ClassDisposable(Action onDispose)
			{
				this.OnDispose = onDispose;
			}

			/// <summary>
			/// The action to call when dispose is executed.
			/// </summary>
			public Action OnDispose;

			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			public void Dispose()
			{
				this.OnDispose();
			}
		}

		#endregion ClassDisposable struct

		#region StructDisposable struct

		/// <summary>
		/// The StructDisposable class is used to test the using statement with a struct.
		/// </summary>
		private struct StructDisposable : IDisposable
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="StructDisposable" /> struct.
			/// </summary>
			public StructDisposable(Action onDispose)
			{
				this.OnDispose = onDispose;
			}

			/// <summary>
			/// The action to call when dispose is executed.
			/// </summary>
			public Action OnDispose;

			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			public void Dispose()
			{
				this.OnDispose();
			}
		}

		#endregion StructDisposable struct
	}
}