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

namespace FlexpressionsTest
{
	/// <summary>
	/// The InputCase class is used to validate the inputs of a method.
	/// </summary>
	public sealed class InputCase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputCase" /> class.
		/// </summary>
		/// <param name="name">The name of the method.</param>
		/// <param name="expectedException">The type of the expected exception.</param>
		/// <param name="arguments">The arguments to supply to the method.</param>
		public InputCase(string name, Type expectedException, object[] arguments)
		{
			this.Name = name;
			this.ExpectedExceptionType = expectedException;
			this.Arguments = arguments ?? new object[0];
		}

		/// <summary>
		/// The name of the method.
		/// </summary>
		public readonly string Name;
		/// <summary>
		/// The type of the expected exception.
		/// </summary>
		public readonly Type ExpectedExceptionType;
		/// <summary>
		/// The arguments to supply to the method.
		/// </summary>
		public readonly object[] Arguments;
	}
}
