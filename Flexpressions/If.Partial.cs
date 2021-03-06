﻿//  Flexpressions
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
using System.Linq.Expressions;

namespace Flexpressions
{
	public sealed partial class If<TParent>
	{
		#region ElseIf Methods

		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1>(Expression<Func<P1, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2>(Expression<Func<P1, P2, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3>(Expression<Func<P1, P2, P3, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4>(Expression<Func<P1, P2, P3, P4, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5>(Expression<Func<P1, P2, P3, P4, P5, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6>(Expression<Func<P1, P2, P3, P4, P5, P6, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <typeparam name="P11">The type of the 11th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <typeparam name="P11">The type of the 11th argument.</typeparam>
		/// <typeparam name="P12">The type of the 12th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <typeparam name="P11">The type of the 11th argument.</typeparam>
		/// <typeparam name="P12">The type of the 12th argument.</typeparam>
		/// <typeparam name="P13">The type of the 13th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <typeparam name="P11">The type of the 11th argument.</typeparam>
		/// <typeparam name="P12">The type of the 12th argument.</typeparam>
		/// <typeparam name="P13">The type of the 13th argument.</typeparam>
		/// <typeparam name="P14">The type of the 14th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <typeparam name="P11">The type of the 11th argument.</typeparam>
		/// <typeparam name="P12">The type of the 12th argument.</typeparam>
		/// <typeparam name="P13">The type of the 13th argument.</typeparam>
		/// <typeparam name="P14">The type of the 14th argument.</typeparam>
		/// <typeparam name="P15">The type of the 15th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}
		/// <summary>
		/// Creates the else if block of the if block.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="P10">The type of the 10th argument.</typeparam>
		/// <typeparam name="P11">The type of the 11th argument.</typeparam>
		/// <typeparam name="P12">The type of the 12th argument.</typeparam>
		/// <typeparam name="P13">The type of the 13th argument.</typeparam>
		/// <typeparam name="P14">The type of the 14th argument.</typeparam>
		/// <typeparam name="P15">The type of the 15th argument.</typeparam>
		/// <typeparam name="P16">The type of the 16th argument.</typeparam>
		/// <param name="test">The test to perform for the ElseIf block.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the new else if block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<TParent>> ElseIf<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.ElseIfSafe(test.Body);
		}

		#endregion ElseIf Methods
	}
}