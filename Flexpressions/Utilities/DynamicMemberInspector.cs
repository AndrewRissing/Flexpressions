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
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Flexpressions.Utilities
{
	/// <summary>
	/// The DynamicMemberInspector provides a clean way of accessing an object's members that are otherwise private.
	/// </summary>
	internal sealed class DynamicMemberInspector : DynamicObject
	{
		#region Constructor

		/// <summary>
		/// Prevents a default instance of the <see cref="DynamicMemberInspector" /> class from being created.
		/// </summary>
		/// <param name="obj">The object to inspect.</param>
		private DynamicMemberInspector(object obj)
		{
			this.obj = obj;
			this.dctMemberInfos = this.GetFields(obj.GetType());
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Wraps the provided object and returns it as dynamic.
		/// </summary>
		/// <param name="obj">The object to wrap.</param>
		/// <returns>The dynamic object to inspect.</returns>
		/// <exception cref="ArgumentException">When <paramref name="obj"/> is null, the exception is thrown.</exception>
		public static dynamic Wrap(object obj)
		{
			Debug.Assert((obj != null), "The obj argument cannot be null.");

			return new DynamicMemberInspector(obj);
		}

		/// <summary>
		/// Provides the implementation for operations that get member values. Classes derived from the <see cref="DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
		/// </summary>
		/// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
		/// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
		/// <returns>
		/// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
		/// </returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			FieldInfo field;

			if (this.dctMemberInfos.TryGetValue(binder.Name, out field))
			{
				result = field.GetValue(this.obj);
				return true;
			}
			else
			{
				result = null;
				return false;
			}
		}

		#endregion Public Code

		#region Private Code

		private readonly object obj;
		private readonly Dictionary<string, FieldInfo> dctMemberInfos;

		/// <summary>
		/// Gets all of the fields on the provided type, recursively through all of its base types.
		/// </summary>
		/// <param name="type">The type to inspect.</param>
		/// <returns>The <see cref="Dictionary&lt;TKey, TValue&gt;"/> containing the lookup of field name to <see cref="FieldInfo"/>.</returns>
		private Dictionary<string, FieldInfo> GetFields(Type type)
		{
			Dictionary<string, FieldInfo> dctMemberInfos = new Dictionary<string, FieldInfo>();

			while (type != typeof(object))
			{
				foreach (var fi in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
				{
					if (!dctMemberInfos.ContainsKey(fi.Name))
						dctMemberInfos[fi.Name] = fi;
				}

				type = type.BaseType;
			}

			return dctMemberInfos;
		}

		#endregion Private Code
	}
}