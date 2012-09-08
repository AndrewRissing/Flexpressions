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
using System.Text;

namespace Flexpressions.Extensions
{
	/// <summary>
	/// Contains extensions for the <see cref="Type"/> class.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Gets the friendly name of a type (including generic types).
		/// </summary>
		/// <param name="type">The type to get the friendly name for.</param>
		/// <param name="fullyQualifyName">If set to <c>true</c>, all types will be fully qualified.</param>
		/// <returns>The friendly string form of the type's name.</returns>
		/// <exception cref="ArgumentNullException">If the <paramref name="type"/> is null, the exception is thrown.</exception>
		public static string GetFriendlyName(this Type type, bool fullyQualifyName = false)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			StringBuilder sb = new StringBuilder();

			TypeExtensions.GetFriendlyName(type, fullyQualifyName, sb);

			// Replace all + symbols (used for subclasses) with periods for a proper reference to the type.
			return sb.Replace('+', '.').ToString();
		}

		/// <summary>
		/// Gets the friendly name of a type (including generic types).
		/// </summary>
		/// <param name="type">The type to get the friendly name for.</param>
		/// <param name="fullyQualifyName">If set to <c>true</c>, all types will be fully qualified.</param>
		/// <param name="sb">The <see cref="StringBuilder" /> to fill with the friendly type name.</param>
		private static void GetFriendlyName(Type type, bool fullyQualifyName, StringBuilder sb)
		{
			bool isArray;

			if (type.IsArray)
			{
				isArray = true;
				type = type.GetElementType();
			}
			else
			{
				isArray = false;
			}

			if (type.IsGenericParameter)
			{
				sb.Append(type.Name);
			}
			else if (!type.IsGenericType)
			{
				sb.Append((fullyQualifyName) ? type.FullName : type.Name);
			}
			else
			{
				string strName;
				bool isFirst = true;

				strName = (fullyQualifyName) ? type.FullName : type.Name;

				sb.Append(strName.Substring(0, strName.IndexOf("`")));
				sb.Append('<');

				foreach (Type tArgument in type.GetGenericArguments())
				{
					if (!isFirst)
						sb.Append(',');

					TypeExtensions.GetFriendlyName(tArgument, fullyQualifyName, sb);
					isFirst = false;
				}

				sb.Append('>');
			}

			if (isArray)
				sb.Append("[]");
		}
	}
}