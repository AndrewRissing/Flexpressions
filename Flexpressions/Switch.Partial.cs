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
using System.Linq.Expressions;

namespace Flexpressions
{
	public sealed partial class Switch<TParent, R>
	{
		#region Case Methods

		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1>(Expression<Func<P1, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2>(Expression<Func<P1, P2, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3>(Expression<Func<P1, P2, P3, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4>(Expression<Func<P1, P2, P3, P4, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5>(Expression<Func<P1, P2, P3, P4, P5, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6>(Expression<Func<P1, P2, P3, P4, P5, P6, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}
		/// <summary>
		/// Creates a new case statement for the switch statement.
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
		/// <param name="caseValue">The value of the case.</param>
		/// <returns>The <see cref="SwitchCase&lt;TParent, R&gt;"/> to create the case.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="caseValue"/> is null, the exception is thrown.</exception>
		public SwitchCase<Switch<TParent, R>, R> Case<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>> caseValue)
		{
			if (caseValue == null)
				throw new ArgumentNullException("caseValue");

			return this.CaseSafe(caseValue.Body);
		}

		#endregion Case Methods
	}
}