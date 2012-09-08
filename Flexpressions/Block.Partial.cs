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
using System.Linq.Expressions;

namespace Flexpressions
{
	public sealed partial class Block<TParent>
	{
		#region Act Methods

		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1>(Expression<Action<P1>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2>(Expression<Action<P1, P2>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3>(Expression<Action<P1, P2, P3>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4>(Expression<Action<P1, P2, P3, P4>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5>(Expression<Action<P1, P2, P3, P4, P5>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6>(Expression<Action<P1, P2, P3, P4, P5, P6>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7>(Expression<Action<P1, P2, P3, P4, P5, P6, P7>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}
		/// <summary>
		/// Adds the provided <see cref="Expression"/> to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="act">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="act"/> is null, the exception is thrown.</exception>
		public Block<TParent> Act<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Action<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>> act)
		{
			if (act == null)
				throw new ArgumentNullException("act");

			return this.Act(act.Body);
		}

		#endregion Act Methods

		#region Do Methods

		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1>(Expression<Func<P1, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2>(Expression<Func<P1, P2, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3>(Expression<Func<P1, P2, P3, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4>(Expression<Func<P1, P2, P3, P4, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5>(Expression<Func<P1, P2, P3, P4, P5, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6>(Expression<Func<P1, P2, P3, P4, P5, P6, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}
		/// <summary>
		/// Creates a do loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Do<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, false);
		}

		#endregion Do Methods

		#region Foreach Methods

		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, R>(string variableName, Expression<Func<P1, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, R>(string variableName, Expression<Func<P1, P2, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, R>(string variableName, Expression<Func<P1, P2, P3, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, R>(string variableName, Expression<Func<P1, P2, P3, P4, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="P9">The type of the 9th argument.</typeparam>
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}
		/// <summary>
		/// Creates a foreach loop using the provided inputs.
		/// </summary>
		/// <typeparam name="V">The type of the loop variable in the foreach loop.</typeparam>
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
		/// <typeparam name="R">The type of the objects being iterated over.</typeparam>
		/// <param name="variableName">The variable name of the foreach loop.</param>
		/// <param name="collection">The collection to iterate over.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		///	<exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="collection"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Foreach<V, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>> collection) where R : IEnumerable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (collection == null)
				throw new ArgumentNullException("collection");

			return this.ForeachSafe(variableName, typeof(V), collection.Body, typeof(R));
		}

		#endregion Foreach Methods

		#region If Methods

		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1>(Expression<Func<P1, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2>(Expression<Func<P1, P2, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3>(Expression<Func<P1, P2, P3, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4>(Expression<Func<P1, P2, P3, P4, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5>(Expression<Func<P1, P2, P3, P4, P5, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6>(Expression<Func<P1, P2, P3, P4, P5, P6, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}
		/// <summary>
		/// Creates an <see cref="If&lt;TParent&gt;"/>, returning its true <see cref="Block&lt;TParent&gt;"/>.
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
		/// <param name="test">The test to perform for the <see cref="If&lt;TParent&gt;"/> block.</param>
		/// <returns>The true <see cref="Block&lt;TParent&gt;"/> of the <see cref="If&lt;TParent&gt;"/> block.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="test"/> is null, the exception is thrown.</exception>
		public Block<If<Block<TParent>>> If<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, bool>> test)
		{
			if (test == null)
				throw new ArgumentNullException("test");

			return this.IfSafe(test.Body);
		}

		#endregion If Methods

		#region Return Methods

		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, R>(Expression<Func<P1, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, R>(Expression<Func<P1, P2, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, R>(Expression<Func<P1, P2, P3, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, R>(Expression<Func<P1, P2, P3, P4, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, R>(Expression<Func<P1, P2, P3, P4, P5, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, R>(Expression<Func<P1, P2, P3, P4, P5, P6, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}
		/// <summary>
		/// Returns the provided value from the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R&gt;"/>.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public TParent Return<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.ReturnSafe(value.ReturnType, value.Body);
		}

		#endregion Return Methods

		#region Set Methods

		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, R>(string variableName, Expression<Func<P1, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, R>(string variableName, Expression<Func<P1, P2, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, R>(string variableName, Expression<Func<P1, P2, P3, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, R>(string variableName, Expression<Func<P1, P2, P3, P4, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds the provided expression to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The return type of the <see cref="Func&lt;P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R&gt;"/>.</typeparam>
		/// <param name="variableName">The name of the variable to set a value into.</param>
		/// <param name="set">The <see cref="Expression"/> containing the action to add to the <see cref="Block&lt;TParent&gt;"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		/// <remarks>If the variable provided has yet to be declared, it will automatically be declared.</remarks>
		public Block<TParent> Set<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>> set)
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.SetSafe(typeof(R), variableName, set.Body);
		}

		#endregion Set Methods

		#region Switch Methods

		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, R>(Expression<Func<P1, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, R>(Expression<Func<P1, P2, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, R>(Expression<Func<P1, P2, P3, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, R>(Expression<Func<P1, P2, P3, P4, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, R>(Expression<Func<P1, P2, P3, P4, P5, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, R>(Expression<Func<P1, P2, P3, P4, P5, P6, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}
		/// <summary>
		/// Inserts a new <see cref="Switch&lt;TParent, R&gt;"/> statement.
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
		/// <typeparam name="R">The type of the value to switch on.</typeparam>
		/// <param name="value">The <see cref="Expression"/> to return the value.</param>
		/// <returns>The <see cref="Switch&lt;TParent, R&gt;"/> to continue constructing the switch expression.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="value"/> is null, the exception is thrown.</exception>
		public Switch<Block<TParent>, R> Switch<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			return this.SwitchSafe<R>(value.Body);
		}

		#endregion Switch Methods

		#region Throw Methods

		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1>(Expression<Func<P1, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2>(Expression<Func<P1, P2, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3>(Expression<Func<P1, P2, P3, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4>(Expression<Func<P1, P2, P3, P4, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5>(Expression<Func<P1, P2, P3, P4, P5, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6>(Expression<Func<P1, P2, P3, P4, P5, P6, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}
		/// <summary>
		/// Throws an exception, using the provided expression.
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
		/// <param name="expression">The expression which will construct the exception to throw.</param>
		/// <returns>The <typeparamref name="TParent"/> parent to continue creating the <see cref="Expression"/>.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="expression"/> is null, the exception is thrown.</exception>
		public TParent Throw<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, Exception>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return this.ThrowSafe(expression.Body);
		}

		#endregion Throw Methods

		#region Using Methods

		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, R>(string variableName, Expression<Func<P1, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, R>(string variableName, Expression<Func<P1, P2, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, R>(string variableName, Expression<Func<P1, P2, P3, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, R>(string variableName, Expression<Func<P1, P2, P3, P4, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}
		/// <summary>
		/// Adds a using statement to the <see cref="Block&lt;TParent&gt;"/>.
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
		/// <typeparam name="R">The type of the object to dispose.</typeparam>
		/// <param name="variableName">The name for the variable to hold the <see cref="IDisposable"/> value.</param>
		/// <param name="set">The <see cref="Expression"/> to initialize the value of <paramref name="variableName"/>.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;"/> of the using statement to continue adding statements.</returns>
		/// <exception cref="ArgumentException">When the <paramref name="variableName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		/// <exception cref="ArgumentNullException">When the <paramref name="set"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> Using<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>(string variableName, Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, R>> set) where R : IDisposable
		{
			if (string.IsNullOrWhiteSpace(variableName))
				throw new ArgumentException("The variable name cannot be null, empty, or whitespace.", "variableName");
			if (set == null)
				throw new ArgumentNullException("set");

			return this.UsingSafe(typeof(R), variableName, set.Body);
		}

		#endregion Using Methods

		#region While Methods

		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1>(Expression<Func<P1, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2>(Expression<Func<P1, P2, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3>(Expression<Func<P1, P2, P3, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4>(Expression<Func<P1, P2, P3, P4, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5>(Expression<Func<P1, P2, P3, P4, P5, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6>(Expression<Func<P1, P2, P3, P4, P5, P6, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
		/// </summary>
		/// <typeparam name="P1">The type of the 1st argument.</typeparam>
		/// <typeparam name="P2">The type of the 2nd argument.</typeparam>
		/// <typeparam name="P3">The type of the 3rd argument.</typeparam>
		/// <typeparam name="P4">The type of the 4th argument.</typeparam>
		/// <typeparam name="P5">The type of the 5th argument.</typeparam>
		/// <typeparam name="P6">The type of the 6th argument.</typeparam>
		/// <typeparam name="P7">The type of the 7th argument.</typeparam>
		/// <typeparam name="P8">The type of the 8th argument.</typeparam>
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}
		/// <summary>
		/// Creates a while loop using the provided inputs.
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
		/// <param name="condition">The condition, if false, will cause the exit of the loop.</param>
		/// <returns>The <see cref="Block&lt;TParent&gt;" /> of the loop.</returns>
		/// <exception cref="ArgumentNullException">When the <paramref name="condition"/> is null, the exception is thrown.</exception>
		public Block<Block<TParent>> While<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16>(Expression<Func<P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15, P16, bool>> condition)
		{
			if (condition == null)
				throw new ArgumentNullException("condition");

			return this.LoopSafe(condition.Body, true);
		}

		#endregion While Methods
	}
}