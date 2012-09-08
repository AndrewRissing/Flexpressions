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
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Flexpressions.Extensions;
using Microsoft.CSharp.RuntimeBinder;
using RBinder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace Flexpressions.Utilities
{
	/// <summary>
	/// The ExpressionConverter class converts an in-memory <see cref="Expression" /> tree
	/// to the equivalent C# code that would have been written to produce it.  It does not modify
	/// the underlying tree in doing so.
	/// </summary>
	public class ExpressionConverter : ExpressionVisitor
	{
		#region Constants

		/// <summary>
		/// The character used to indent each new level.
		/// </summary>
		protected const char INDENT_CHARACTER = '\t';

		#endregion Constants

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="ExpressionConverter" /> class.
		/// </summary>
		/// <param name="fullyQualifyTypes">If set to <c>true</c>, all types will be fully qualified.</param>
		public ExpressionConverter(bool fullyQualifyTypes = false)
		{
			this.fullyQualifyTypes = fullyQualifyTypes;

			this.sb = new StringBuilder();
			this.expressionTypeString = typeof(Expression).GetFriendlyName(fullyQualifyTypes);
			this.parameters = new Dictionary<ParameterExpression, string>();
			this.variables = new Dictionary<ParameterExpression, string>();
			this.labelTargets = new Dictionary<LabelTarget, string>();
			this.types = new Dictionary<Type, string>();
			this.memberInfos = new Dictionary<MemberInfo, string>();
			this.outerVariables = new Dictionary<object, string>();
			this.declarations = new List<string>();
			this.itemCount = 0;
		}

		#endregion Constructor

		#region Public Code

		/// <summary>
		/// Converts the <see cref="Expression"/> to the equivalent C# code without modifying the provided expression.
		/// </summary>
		/// <param name="expression">The expression to convert, which will not be modified.</param>
		/// <returns>The string representation of the <see cref="Expression"/> in C# code.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null, the exception is thrown.</exception>
		public virtual string ToCSharpString(Expression expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			this.sb.Length = 0;

			this.Visit(expression);
			this.InsertInitialVariable();
			this.InsertDeclarations();
			this.InsertComment(expression);

			return this.sb.ToString();
		}

		#endregion Public Code

		#region Protected Code

		/// <summary>
		/// Indicates whether or not types are fully qualified in the generated string.
		/// </summary>
		protected readonly bool fullyQualifyTypes;
		/// <summary>
		/// The <see cref="StringBuilder"/> containing the generated string.
		/// </summary>
		protected readonly StringBuilder sb;

		/// <summary>
		/// Adds a string to be inserted at the start of the code, which could not have been inserted
		/// due to how <see cref="Expression"/>s are processed.
		/// </summary>
		/// <param name="value">The value to add to the declarations.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="value"/> is null, the exception is thrown.</exception>
		protected void AddDeclaration(string value)
		{
			Debug.Assert((value != null), "The value argument cannot be null.");

			this.declarations.Add(value);
		}
		/// <summary>
		/// Converts to string.
		/// </summary>
		/// <param name="value">Converts the value converted into "true" and "false".</param>
		/// <returns>The value converted into "true" and "false".</returns>
		protected string ConvertToString(bool value)
		{
			return (value) ? "true" : "false";
		}
		/// <summary>
		/// Gets the <see cref="LabelTarget"/> reference string.
		/// </summary>
		/// <param name="labelTarget">The <see cref="LabelTarget"/> to retrieve the string for.</param>
		/// <returns>The <see cref="LabelTarget"/> reference string.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="labelTarget"/> is null, the exception is thrown.</exception>
		protected string GetLabelTargetString(LabelTarget labelTarget)
		{
			Debug.Assert(labelTarget != null, "The labelTarget argument cannot be null.");

			string name;

			if (!this.labelTargets.TryGetValue(labelTarget, out name))
			{
				this.labelTargets[labelTarget] = name = string.Format("lt{0}", ++this.itemCount);

				this.AddDeclaration(string.Format("var {0} = {1}.Label({2}, \"{3}\");",
					name,
					this.expressionTypeString,
					this.GetTypeString(labelTarget.Type),
					(string.IsNullOrWhiteSpace(labelTarget.Name)) ? name : labelTarget.Name));
			}

			return name;
		}
		/// <summary>
		/// Gets the <see cref="MemberInfo"/> reference string.
		/// </summary>
		/// <param name="memberInfo">The <see cref="MemberInfo"/> to retrieve the string for.</param>
		/// <returns>The <see cref="MemberInfo"/> reference string.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="memberInfo"/> is null, the exception is thrown.</exception>
		/// <exception cref="NotImplementedException">When the <paramref name="memberInfo"/> is an unknown derived class, the exception is thrown.</exception>
		protected string GetMemberInfoString(MemberInfo memberInfo)
		{
			Debug.Assert((memberInfo != null), "The memberInfo argument cannot be null.");

			string name, declaringType, bindingFlagsType;
			ConstructorInfo constructorInfo;
			MethodInfo methodInfo;
			FieldInfo fieldInfo;
			EventInfo eventInfo;
			PropertyInfo propertyInfo;

			if (!this.memberInfos.TryGetValue(memberInfo, out name))
			{
				declaringType = this.GetTypeString(memberInfo.DeclaringType);
				bindingFlagsType = typeof(BindingFlags).GetFriendlyName(this.fullyQualifyTypes);

				if ((constructorInfo = (memberInfo as ConstructorInfo)) != null)
				{
					this.memberInfos[constructorInfo] = name = string.Format("constructor{0}", ++this.itemCount);
					this.AddDeclaration(
						string.Format("var {0} = {1}.GetConstructor({2}.{3} | {2}.{4} | {2}.FlattenHierarchy, null, new {5}[] {{ {6} }}, null);",
							name,
							declaringType,
							bindingFlagsType,
							(constructorInfo.IsPublic) ? "Public" : "NonPublic",
							(constructorInfo.IsStatic) ? "Static" : "Instance",
							typeof(Type).GetFriendlyName(this.fullyQualifyTypes),
							string.Join(", ", constructorInfo.GetParameters().Select(x => this.GetTypeString(x.ParameterType)))));
				}
				else if ((methodInfo = (memberInfo as MethodInfo)) != null)
				{
					this.memberInfos[methodInfo] = name = string.Format("method{0}", ++this.itemCount);
					this.AddDeclaration(
						string.Format("var {0} = {1}.GetMethod(\"{2}\", {3}.{4} | {3}.{5} | {3}.FlattenHierarchy, null, new {6}[] {{ {7} }}, null);",
							name,
							declaringType,
							methodInfo.Name,
							bindingFlagsType,
							(methodInfo.IsPublic) ? "Public" : "NonPublic",
							(methodInfo.IsStatic) ? "Static" : "Instance",
							typeof(Type).GetFriendlyName(this.fullyQualifyTypes),
							string.Join(", ", methodInfo.GetParameters().Select(x => this.GetTypeString(x.ParameterType)))));
				}
				else if ((fieldInfo = (memberInfo as FieldInfo)) != null)
				{
					this.memberInfos[fieldInfo] = name = string.Format("field{0}", ++this.itemCount);
					this.AddDeclaration(
						string.Format("var {0} = {1}.GetField(\"{2}\", {3}.{4} | {3}.{5} | {3}.FlattenHierarchy);",
							name,
							declaringType,
							fieldInfo.Name,
							bindingFlagsType,
							(fieldInfo.IsPublic) ? "Public" : "NonPublic",
							(fieldInfo.IsStatic) ? "Static" : "Instance"));
				}
				else if ((eventInfo = (memberInfo as EventInfo)) != null)
				{
					this.memberInfos[eventInfo] = name = string.Format("event{0}", ++this.itemCount);
					this.AddDeclaration(
						string.Format("var {0} = {1}.GetEvent(\"{2}\", {3}.NonPublic | {3}.Public | {3}.Static | {3}.FlattenHierarchy);",
							name,
							declaringType,
							eventInfo.Name,
							bindingFlagsType));
				}
				else if ((propertyInfo = (memberInfo as PropertyInfo)) != null)
				{
					var method = (propertyInfo.CanRead) ? propertyInfo.GetGetMethod(true) : propertyInfo.GetSetMethod(true);

					this.memberInfos[propertyInfo] = name = string.Format("property{0}", ++this.itemCount);
					this.AddDeclaration(
						string.Format("var {0} = {1}.GetProperty(\"{2}\", {3}.{4} | {3}.{5} | {3}.FlattenHierarchy, null, {6}, new {7}[] {{ {8} }}, null);",
							name,
							declaringType,
							propertyInfo.Name,
							bindingFlagsType,
							(method.IsPublic) ? "Public" : "NonPublic",
							(method.IsStatic) ? "Static" : "Instance",
							this.GetTypeString(propertyInfo.PropertyType),
							typeof(Type).GetFriendlyName(this.fullyQualifyTypes),
							string.Join(", ", propertyInfo.GetIndexParameters().Select(x => this.GetTypeString(x.ParameterType)))));
				}
				else
				{
					throw new NotImplementedException(string.Format("Unknown MemberInfo type, {0}.", memberInfo.GetType().GetFriendlyName(this.fullyQualifyTypes)));
				}
			}

			return name;
		}
		/// <summary>
		/// Gets the outer variable string.
		/// </summary>
		/// <param name="value">The <see cref="ConstantExpression"/> representing the outer variable (ie. captured variable).</param>
		/// <returns>The string representation of the <see cref="ConstantExpression"/>'s value.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="value"/> is null, the exception is thrown.</exception>
		protected string GetOuterVariableString(object value)
		{
			Debug.Assert((value != null), "The value argument cannot be null.");

			string name;

			if (!this.outerVariables.TryGetValue(value, out name))
			{
				this.outerVariables[value] = name = string.Format("ov{0}", ++this.itemCount);
				this.AddDeclaration(string.Format("var {0} = null; // TODO: Outer variable detected.", name));
			}

			return name;
		}
		/// <summary>
		/// Gets the <see cref="ParameterExpression"/> reference string.
		/// </summary>
		/// <param name="variable">The variable to retrieve the parameter name.</param>
		/// <returns>The <see cref="ParameterExpression"/> reference string.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="variable"/> is null, the exception is thrown.</exception>
		protected string GetParameterName(ParameterExpression variable)
		{
			Debug.Assert((variable != null), "The variable argument cannot be null.");

			string name;

			if (!this.parameters.TryGetValue(variable, out name))
			{
				if (!this.variables.TryGetValue(variable, out name))
				{
					this.variables[variable] = name = string.Format("v{0}", ++this.itemCount);

					this.AddDeclaration(string.Format("var {0} = {1}.Variable({2}, \"{3}\");",
						name,
						this.expressionTypeString,
						this.GetTypeString(variable.Type),
						(string.IsNullOrWhiteSpace(variable.Name)) ? name : variable.Name));
				}
			}

			return name;
		}
		/// <summary>
		/// Gets the <see cref="Type" /> reference string.
		/// </summary>
		/// <param name="type">The <see cref="Type" /> to get the string representation of.</param>
		/// <returns>The <see cref="Type"/> reference string.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="type"/> is null, the exception is thrown.</exception>
		protected string GetTypeString(Type type)
		{
			Debug.Assert((type != null), "The type argument cannot be null.");

			string name, typename;

			if (!this.types.TryGetValue(type, out name))
			{
				this.types[type] = name = string.Format("t{0}", ++this.itemCount);

				// Void needs to be handled special.
				if (type == typeof(void))
					typename = "void";
				else
					typename = type.GetFriendlyName(this.fullyQualifyTypes);

				this.AddDeclaration(string.Format("var {0} = typeof({1});", name, typename));
			}

			return name;
		}
		/// <summary>
		/// Indents the subsequent lines one level.
		/// </summary>
		protected void Indent()
		{
			++this.indent;
		}
		/// <summary>
		/// Inserts a comment at the start of the code to summarize the logic contained within.
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to write the comment for.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null, the exception is thrown.</exception>
		protected void InsertComment(Expression expression)
		{
			Debug.Assert((expression != null), "The expression argument cannot be null.");

			try
			{
				var property = typeof(Expression).GetProperty("DebugView", BindingFlags.NonPublic | BindingFlags.Instance);
				var debugView = property.GetValue(expression, null) as string;

				if (!string.IsNullOrWhiteSpace(debugView))
				{
					var comment = new StringBuilder(debugView)
						.Replace(Environment.NewLine, Environment.NewLine + "// ")
						.Insert(0, "// ")
						.Insert(0, Environment.NewLine)
						.Insert(0, "// ")
						.Insert(0, Environment.NewLine)
						.Insert(0, "// The subsequent expression code equates to:")
						.AppendLine()
						.AppendLine()
						.ToString();

					this.sb.Insert(0, comment);
				}
			}
			catch (TargetInvocationException e)
			{
				// Ignore the exception and continue on, if it is a SecurityException.
				if (!(e.GetBaseException() is SecurityException))
					throw;
			}
		}
		/// <summary>
		/// Inserts the declarations at the start of the code.
		/// </summary>
		protected void InsertDeclarations()
		{
			// Pad the beginning and then insert the declarations.
			this.sb.Insert(0, Environment.NewLine, 2);
			this.sb.Insert(0, string.Join(Environment.NewLine, this.declarations));
		}
		/// <summary>
		/// Inserts the initial variable to capture the generated expression.
		/// </summary>
		protected void InsertInitialVariable()
		{
			// Add the initial statement to capture the expression.
			this.sb.Insert(0, "var expression = ");

			// Remove the trailing newline and add a semicolon.
			this.sb.Length = this.sb.Length - Environment.NewLine.Length;
			this.sb.Append(";");
		}
		/// <summary>
		/// Undents the subsequent lines one level.
		/// </summary>
		protected void Undent()
		{
			--this.indent;
		}
		/// <summary>
		/// Writes the specified string followed by a new line.
		/// </summary>
		/// <param name="value">The string value to write.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="value"/> is null, the exception is thrown.</exception>
		protected void WriteLine(string value)
		{
			Debug.Assert((value != null), "The value argument cannot be null.");

			this.sb.Append(ExpressionConverter.INDENT_CHARACTER, this.indent);
			this.sb.AppendLine(value);
		}
		/// <summary>
		/// Writes the provided node.
		/// </summary>
		/// <typeparam name="T">The type of the node to write.</typeparam>
		/// <param name="node">The node to write.</param>
		/// <param name="methodName">Name of the method used to create the node.</param>
		/// <param name="writePerArgument">The action used to write each argument.</param>
		/// <returns>The node provided to write out.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="node"/> or <paramref name="writePerArgument"/> is null, the exception is thrown.</exception>
		/// <exception cref="ArgumentException">When the <paramref name="methodName"/> is a name that is null, empty, or whitespace, the exception is thrown.</exception>
		protected T WriteNode<T>(T node, string methodName, IEnumerable<Action<ExpressionConverter, T>> writePerArgument)
		{
			Debug.Assert((node != null), "The node argument cannot be null.");
			Debug.Assert(!string.IsNullOrWhiteSpace(methodName), "The methodName argument cannot be null, empty, or just whitespace.");
			Debug.Assert((writePerArgument != null), "The writePerArgument argument cannot be null.");

			bool first = true;

			this.WriteLine(methodName);
			this.WriteLine("(");
			this.Indent();

			foreach (var write in writePerArgument)
			{
				if (!first)
					this.sb.Insert(this.sb.Length - Environment.NewLine.Length, ',');

				write(this, node);
				first = false;
			}

			this.Undent();
			this.WriteLine(")");

			return node;
		}
		/// <summary>
		/// Writes an array of items.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> of the items to write.</typeparam>
		/// <param name="items">The items to write.</param>
		/// <param name="writePerItem">The action used to write each item.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="items"/> or <paramref name="writePerItem"/> is null, the exception is thrown.</exception>
		protected void WriteArray<T>(IEnumerable<T> items, Action<ExpressionConverter, T> writePerItem)
		{
			Debug.Assert((items != null), "The items argument cannot be null.");
			Debug.Assert((writePerItem != null), "The writePerItem argument cannot be null.");

			bool isNotEmpty = items.Any();

			this.WriteLine(string.Format("new {0}[]{1}",
				typeof(T).GetFriendlyName(this.fullyQualifyTypes),
				(isNotEmpty) ? string.Empty : " { }"));

			if (isNotEmpty)
			{
				bool first = true;

				this.WriteLine("{");
				this.Indent();

				foreach (var item in items)
				{
					if (!first)
						this.sb.Insert(this.sb.Length - Environment.NewLine.Length, ',');

					writePerItem(this, item);
					first = false;
				}

				this.Undent();
				this.WriteLine("}");
			}
		}
		/// <summary>
		/// Writes the <see cref="SymbolDocumentInfo"/>.
		/// </summary>
		/// <param name="node">The <see cref="SymbolDocumentInfo"/> to write.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="node"/> is null, the exception is thrown.</exception>
		protected void WriteSymbolDocumentInfo(SymbolDocumentInfo node)
		{
			Debug.Assert((node != null), "The node argument cannot be null.");

			this.WriteNode(
				node,
				string.Format("{0}.SymbolDocument", this.expressionTypeString),
				new Action<ExpressionConverter, SymbolDocumentInfo>[]
				{
					(that, x) => that.WriteLine(string.Format("\"{0}\"", x.FileName)),
					(that, x) => that.WriteLine(string.Format("new {0}(\"{1}\")", typeof(Guid).GetFriendlyName(that.fullyQualifyTypes), x.Language.ToString())),
					(that, x) => that.WriteLine(string.Format("new {0}(\"{1}\")", typeof(Guid).GetFriendlyName(that.fullyQualifyTypes), x.LanguageVendor.ToString())),
					(that, x) => that.WriteLine(string.Format("new {0}(\"{1}\")", typeof(Guid).GetFriendlyName(that.fullyQualifyTypes), x.DocumentType.ToString()))
				});
		}
		/// <summary>
		/// Writes the <see cref="CallSiteBinder"/> to write.
		/// </summary>
		/// <param name="callSiteBinder">The <see cref="CallSiteBinder"/> to write.</param>
		/// <exception cref="NotImplementedException">When the <paramref name="callSiteBinder"/> is an unknown derived class, the exception is thrown.</exception>
		protected virtual void WriteBinder(CallSiteBinder callSiteBinder)
		{
			Debug.Assert((callSiteBinder != null), "The callSiteBinder argument cannot be null.");

			BinaryOperationBinder binaryOperationBinder;
			ConvertBinder convertBinder;
			GetMemberBinder getMemberBinder;
			GetIndexBinder getIndexBinder;
			InvokeBinder invokeBinder;
			InvokeMemberBinder invokeMemberBinder;
			SetIndexBinder setIndexBinder;
			SetMemberBinder setMemberBinder;
			UnaryOperationBinder unaryOperationBinder;
			DynamicMetaObjectBinder dynamicMetaObjectBinder;
			bool handled = false;

			try
			{
				if ((binaryOperationBinder = (callSiteBinder as BinaryOperationBinder)) != null)
				{
					this.WriteCallSiteBinder(binaryOperationBinder);
					handled = true;
				}
				else if ((convertBinder = (callSiteBinder as ConvertBinder)) != null)
				{
					this.WriteCallSiteBinder(convertBinder);
					handled = true;
				}
				else if ((getMemberBinder = (callSiteBinder as GetMemberBinder)) != null)
				{
					this.WriteCallSiteBinder(getMemberBinder);
					handled = true;
				}
				else if ((getIndexBinder = (callSiteBinder as GetIndexBinder)) != null)
				{
					this.WriteCallSiteBinder(getIndexBinder);
					handled = true;
				}
				else if ((invokeBinder = (callSiteBinder as InvokeBinder)) != null)
				{
					this.WriteCallSiteBinder(invokeBinder);
					handled = true;
				}
				else if ((invokeMemberBinder = (callSiteBinder as InvokeMemberBinder)) != null)
				{
					this.WriteCallSiteBinder(invokeMemberBinder);
					handled = true;
				}
				else if ((setIndexBinder = (callSiteBinder as SetIndexBinder)) != null)
				{
					this.WriteCallSiteBinder(setIndexBinder);
					handled = true;
				}
				else if ((setMemberBinder = (callSiteBinder as SetMemberBinder)) != null)
				{
					this.WriteCallSiteBinder(setMemberBinder);
					handled = true;
				}
				else if ((unaryOperationBinder = (callSiteBinder as UnaryOperationBinder)) != null)
				{
					this.WriteCallSiteBinder(unaryOperationBinder);
					handled = true;
				}
				else if ((dynamicMetaObjectBinder = (callSiteBinder as DynamicMetaObjectBinder)) != null)
				{
					var type = dynamicMetaObjectBinder.GetType();

					if (type.Name == "CSharpIsEventBinder")
					{
						this.WriteCallSiteBinderIsEventBinder(dynamicMetaObjectBinder);
						handled = true;
					}
					else if (type.Name == "CSharpInvokeConstructorBinder")
					{
						this.WriteCallSiteBinderInvokeConstructor(dynamicMetaObjectBinder);
						handled = true;
					}
				}

				if (!handled)
					throw new NotImplementedException(string.Format("The CallSiteBinder of type, {0}, is currently not supported.", callSiteBinder.GetType().GetFriendlyName(this.fullyQualifyTypes)));
			}
			catch (SecurityException)
			{
				this.WriteLine(string.Format("// TODO: Correct reference to the CallSiteBinder - Type = {0}.", callSiteBinder.GetType().GetFriendlyName(this.fullyQualifyTypes)));
				this.WriteLine("null");
			}
		}
		/// <summary>
		/// Writes the <see cref="CSharpArgumentInfo"/>.
		/// </summary>
		/// <param name="argumentInfo">The <see cref="CSharpArgumentInfo"/> to write.</param>
		protected void WriteCSharpArgumentInfo(CSharpArgumentInfo argumentInfo)
		{
			Debug.Assert((argumentInfo != null), "The argumentInfo argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(argumentInfo);

			this.WriteNode
			(
				binder,
				string.Format("{0}.Create", typeof(CSharpArgumentInfo).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteCSharpArgumentInfoFlags((CSharpArgumentInfoFlags)x.m_flags),
					(that, x) => that.WriteLine((x.m_name != null) ? string.Format("\"{0}\"", (string)x.m_name) : "null")
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="CSharpArgumentInfoFlags"/>.
		/// </summary>
		/// <param name="flags">The flags to write.</param>
		protected void WriteCSharpArgumentInfoFlags(CSharpArgumentInfoFlags flags)
		{
			Type type = typeof(CSharpArgumentInfoFlags);
			string typeReference = type.GetFriendlyName(this.fullyQualifyTypes);

			if (flags == CSharpArgumentInfoFlags.None)
			{
				this.WriteLine(string.Format("{0}.None", typeReference));
			}
			else
			{
				var values = new List<string>();

				foreach (CSharpArgumentInfoFlags v in Enum.GetValues(type))
					if ((v != CSharpArgumentInfoFlags.None) && flags.HasFlag(v))
						values.Add(string.Format("{0}.{1}", typeReference, v.ToString()));

				this.WriteLine(string.Join(" | ", values));
			}
		}
		/// <summary>
		/// Writes the <see cref="CSharpBinderFlags"/>.
		/// </summary>
		/// <param name="flags">The flags to write.</param>
		protected void WriteCSharpBinderFlags(CSharpBinderFlags flags)
		{
			Type type = typeof(CSharpBinderFlags);
			string typeReference = type.GetFriendlyName(this.fullyQualifyTypes);

			if (flags == CSharpBinderFlags.None)
			{
				this.WriteLine(string.Format("{0}.None", typeReference));
			}
			else
			{
				var values = new List<string>();

				foreach (CSharpBinderFlags v in Enum.GetValues(type))
					if ((v != CSharpBinderFlags.None) && flags.HasFlag(v))
						values.Add(string.Format("{0}.{1}", typeReference, v.ToString()));

				this.WriteLine(string.Join(" | ", values));
			}
		}
		/// <summary>
		/// Writes the BinaryOperation's <see cref="CSharpBinderFlags"/>.
		/// </summary>
		/// <param name="binaryOperationFlags">The binary operation flags.</param>
		/// <param name="isChecked">If set to <c>true</c>, the binary operation is checked.</param>
		protected void WriteBinaryOperationFlags(int binaryOperationFlags, bool isChecked)
		{
			CSharpBinderFlags flag;

			if ((binaryOperationFlags & 2) != 0)
				flag = CSharpBinderFlags.BinaryOperationLogical;
			else
				flag = CSharpBinderFlags.None;

			if (isChecked)
				flag = CSharpBinderFlags.CheckedContext;
			else
				flag = CSharpBinderFlags.None;

			this.WriteCSharpBinderFlags(flag);
		}
		/// <summary>
		/// Writes the Convert's <see cref="CSharpBinderFlags"/>.
		/// </summary>
		/// <param name="conversionKind">The integer value of the conversion kind.</param>
		/// <param name="isChecked">If set to <c>true</c>, the conversion is checked.</param>
		protected void WriteConvertCSharpBinderFlags(int conversionKind, bool isChecked)
		{
			CSharpBinderFlags flag;

			if ((conversionKind & 1) != 0)
				flag = CSharpBinderFlags.ConvertExplicit;
			else if ((conversionKind & 2) != 0)
				flag = CSharpBinderFlags.ConvertArrayIndex;
			else
				flag = CSharpBinderFlags.None;

			if (isChecked)
				flag |= CSharpBinderFlags.CheckedContext;

			this.WriteCSharpBinderFlags(flag);
		}
		/// <summary>
		/// Writes the SetIndex and SetMember's <see cref="CSharpBinderFlags"/>.
		/// </summary>
		/// <param name="isCompoundAssignment">If set to <c>true</c>, the conversion is a compound assignment.</param>
		/// <param name="isChecked">If set to <c>true</c>, the conversion is checked.</param>
		protected void WriteSetCSharpBinderFlags(bool isCompoundAssignment, bool isChecked)
		{
			CSharpBinderFlags flag;

			if (isCompoundAssignment)
				flag = CSharpBinderFlags.ValueFromCompoundAssignment;
			else
				flag = CSharpBinderFlags.None;

			if (isChecked)
				flag |= CSharpBinderFlags.CheckedContext;

			this.WriteCSharpBinderFlags(flag);
		}
		/// <summary>
		/// Writes the InvokeMember's <see cref="CSharpBinderFlags"/>.
		/// </summary>
		/// <param name="flags">The integer value of the InvokeMember's flags.</param>
		protected void WriteInvokeMemberCSharpBinderFlags(int flags)
		{
			CSharpBinderFlags flag;

			if ((flags & 1) == 1)
				flag = CSharpBinderFlags.InvokeSimpleName;
			else if ((flags & 2) == 2)
				flag = CSharpBinderFlags.InvokeSpecialName;
			else if ((flags & 4) == 4)
				flag = CSharpBinderFlags.ResultDiscarded;
			else
				flag = CSharpBinderFlags.None;

			this.WriteCSharpBinderFlags(flag);
		}
		/// <summary>
		/// Writes the <see cref="BinaryOperationBinder"/>.
		/// </summary>
		/// <param name="binaryOperationBinder">The <see cref="BinaryOperationBinder"/> to write.</param>
		protected void WriteCallSiteBinder(BinaryOperationBinder binaryOperationBinder)
		{
			Debug.Assert((binaryOperationBinder != null), "The binaryOperationBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(binaryOperationBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.BinaryOperation", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteBinaryOperationFlags(Convert.ToInt32((Enum)x.m_binopFlags), (bool)x.m_isChecked),
					(that, x) => that.WriteLine(string.Format("{0}.{1}",
						typeof(ExpressionType).GetFriendlyName(that.fullyQualifyTypes),
						((ExpressionType)x._operation).ToString())),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="ConvertBinder"/>.
		/// </summary>
		/// <param name="convertBinder">The <see cref="ConvertBinder"/> to write.</param>
		protected void WriteCallSiteBinder(ConvertBinder convertBinder)
		{
			Debug.Assert((convertBinder != null), "The convertBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(convertBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.Convert", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteConvertCSharpBinderFlags(Convert.ToInt32((Enum)x.m_conversionKind), (bool)x.m_isChecked),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x._type)),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="DynamicMetaObjectBinder"/> for the InvokeConstructor binder.
		/// </summary>
		/// <param name="dynamicMetaObjectBinder">The <see cref="DynamicMetaObjectBinder"/> to write.</param>
		protected void WriteCallSiteBinderInvokeConstructor(DynamicMetaObjectBinder dynamicMetaObjectBinder)
		{
			Debug.Assert((dynamicMetaObjectBinder != null), "The dynamicMetaObjectBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(dynamicMetaObjectBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.InvokeConstructor", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
					{
						(that, x) => that.WriteLine(string.Format("{0}.{1}",
							typeof(CSharpBinderFlags).GetFriendlyName(that.fullyQualifyTypes),
							CSharpBinderFlags.None.ToString())),
						(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
						(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
					}
			);
		}
		/// <summary>
		/// Writes the <see cref="GetIndexBinder"/>.
		/// </summary>
		/// <param name="getIndexBinder">The <see cref="GetIndexBinder"/> to write.</param>
		protected void WriteCallSiteBinder(GetIndexBinder getIndexBinder)
		{
			Debug.Assert((getIndexBinder != null), "The getIndexBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(getIndexBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.GetIndex", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteLine(string.Format("{0}.{1}",
						typeof(CSharpBinderFlags).GetFriendlyName(that.fullyQualifyTypes),
						CSharpBinderFlags.None.ToString())),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="GetMemberBinder"/>.
		/// </summary>
		/// <param name="getMemberBinder">The <see cref="GetMemberBinder"/> to write.</param>
		protected void WriteCallSiteBinder(GetMemberBinder getMemberBinder)
		{
			Debug.Assert((getMemberBinder != null), "The getMemberBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(getMemberBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.GetMember", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteLine(string.Format("{0}.{1}",
						typeof(CSharpBinderFlags).GetFriendlyName(that.fullyQualifyTypes),
						((bool)x.m_bResultIndexed) ? CSharpBinderFlags.ResultIndexed.ToString() : CSharpBinderFlags.None.ToString())),
					(that, x) => that.WriteLine(string.Format("\"{0}\"", (string)x._name)),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="InvokeBinder"/>.
		/// </summary>
		/// <param name="invokeBinder">The <see cref="InvokeBinder"/> to write.</param>
		protected void WriteCallSiteBinder(InvokeBinder invokeBinder)
		{
			Debug.Assert((invokeBinder != null), "The invokeBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(invokeBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.Invoke", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteLine(string.Format("{0}.{1}",
						typeof(CSharpBinderFlags).GetFriendlyName(that.fullyQualifyTypes),
						((Convert.ToInt32((Enum)x.m_flags) & 4) == 4) ? CSharpBinderFlags.ResultDiscarded.ToString() : CSharpBinderFlags.None.ToString())),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="InvokeMemberBinder"/>.
		/// </summary>
		/// <param name="invokeMemberBinder">The <see cref="InvokeMemberBinder"/> to write.</param>
		protected void WriteCallSiteBinder(InvokeMemberBinder invokeMemberBinder)
		{
			Debug.Assert((invokeMemberBinder != null), "The invokeMemberBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(invokeMemberBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.InvokeMember", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteInvokeMemberCSharpBinderFlags(Convert.ToInt32((Enum)x.m_flags)),
					(that, x) => that.WriteLine(string.Format("\"{0}\"", (string)x._name)),
					(that, x) => that.WriteArray((List<Type>)x.m_typeArguments, (t, y) => t.WriteLine(t.GetTypeString(y))),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="SetIndexBinder"/>.
		/// </summary>
		/// <param name="setIndexBinder">The <see cref="SetIndexBinder"/> to write.</param>
		protected void WriteCallSiteBinder(SetIndexBinder setIndexBinder)
		{
			Debug.Assert((setIndexBinder != null), "The setIndexBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(setIndexBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.SetIndex", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteSetCSharpBinderFlags((bool)x.m_isChecked, (bool)x.m_bIsCompoundAssignment),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="SetMemberBinder"/>.
		/// </summary>
		/// <param name="setMemberBinder">The <see cref="SetMemberBinder"/> to write.</param>
		protected void WriteCallSiteBinder(SetMemberBinder setMemberBinder)
		{
			Debug.Assert((setMemberBinder != null), "The setMemberBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(setMemberBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.SetMember", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteSetCSharpBinderFlags((bool)x.m_isChecked, (bool)x.m_bIsCompoundAssignment),
					(that, x) => that.WriteLine(string.Format("\"{0}\"", (string)x._name)),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="UnaryOperationBinder"/>.
		/// </summary>
		/// <param name="unaryOperationBinder">The <see cref="UnaryOperationBinder"/> to write.</param>
		protected void WriteCallSiteBinder(UnaryOperationBinder unaryOperationBinder)
		{
			Debug.Assert((unaryOperationBinder != null), "The unaryOperationBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(unaryOperationBinder);

			this.WriteNode
			(
				binder,
				string.Format("{0}.UnaryOperation", typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, dynamic>[]
				{
					(that, x) => that.WriteLine(string.Format("{0}.{1}",
						typeof(CSharpBinderFlags).GetFriendlyName(that.fullyQualifyTypes),
						((bool)x.m_isChecked) ? CSharpBinderFlags.CheckedContext.ToString() : CSharpBinderFlags.None.ToString())),
					(that, x) => that.WriteLine(string.Format("{0}.{1}",
						typeof(ExpressionType).GetFriendlyName(that.fullyQualifyTypes),
						((ExpressionType)x._operation).ToString())),
					(that, x) => that.WriteLine(that.GetTypeString((Type)x.m_callingContext)),
					(that, x) => that.WriteArray((List<CSharpArgumentInfo>)x.m_argumentInfo, (t, y) => t.WriteCSharpArgumentInfo(y))
				}
			);
		}
		/// <summary>
		/// Writes the <see cref="DynamicMetaObjectBinder"/> for the IsEvent binder.
		/// </summary>
		/// <param name="dynamicMetaObjectBinder">The <see cref="DynamicMetaObjectBinder"/> to write.</param>
		protected void WriteCallSiteBinderIsEventBinder(DynamicMetaObjectBinder dynamicMetaObjectBinder)
		{
			Debug.Assert((dynamicMetaObjectBinder != null), "The dynamicMetaObjectBinder argument cannot be null.");

			var binder = DynamicMemberInspector.Wrap(dynamicMetaObjectBinder);

			this.WriteLine(string.Format("{0}.IsEvent({1}.None, \"{2}\", {3})",
				typeof(RBinder).GetFriendlyName(this.fullyQualifyTypes),
				typeof(CSharpBinderFlags).GetFriendlyName(this.fullyQualifyTypes),
				(string)binder.m_name,
				this.GetTypeString((Type)binder.m_callingContext)));
		}

		/// <summary>
		/// Visits the children of the <see cref="BinaryExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitBinary(BinaryExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, BinaryExpression>>();

			writePerArgument.Add((that, x) => that.WriteLine(string.Format("{0}.{1}", typeof(ExpressionType).GetFriendlyName(that.fullyQualifyTypes), x.NodeType.ToString())));
			writePerArgument.Add((that, x) => that.Visit(x.Left));
			writePerArgument.Add((that, x) => that.Visit(x.Right));
			writePerArgument.Add((that, x) => that.WriteLine(that.ConvertToString(x.IsLiftedToNull)));

			if (node.Method != null)
				writePerArgument.Add((that, x) => that.WriteLine(that.GetMemberInfoString(x.Method)));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			if (node.Conversion != null)
				writePerArgument.Add((that, x) => that.Visit(node.Conversion));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			return this.WriteNode(node, string.Format("{0}.MakeBinary", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="BlockExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitBlock(BlockExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, BlockExpression>>();

			writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Type)));
			
			if (node.Variables.Count > 0)
				writePerArgument.Add(
					(that, x) => that.WriteLine(string.Format("new {0}[] {{ {1} }}",
						typeof(ParameterExpression).GetFriendlyName(that.fullyQualifyTypes),
						string.Join(", ", x.Variables.Select(v => that.GetParameterName(v))))));

			writePerArgument.Add((that, x) => that.WriteArray(x.Expressions, (t, y) => t.Visit(y)));

			return this.WriteNode(node, string.Format("{0}.Block", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the <see cref="CatchBlock" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, CatchBlock>>();

			if (node.Variable != null)
				writePerArgument.Add((that, x) => that.VisitParameter(x.Variable));
			else
				writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Test)));

			writePerArgument.Add((that, x) => that.Visit(x.Body));

			if (node.Filter != null)
				writePerArgument.Add((that, x) => that.Visit(x.Filter));

			return this.WriteNode(node, string.Format("{0}.Catch", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="ConditionalExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, ConditionalExpression>>();

			writePerArgument.Add((that, x) => that.Visit(x.Test));
			writePerArgument.Add((that, x) => that.Visit(x.IfTrue));
			writePerArgument.Add((that, x) => that.Visit(x.IfFalse));

			if (node.Type != null)
				writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Type)));

			return this.WriteNode(node, string.Format("{0}.Condition", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the <see cref="ConstantExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		/// <exception cref="NotImplementedException">When the <paramref name="node"/> value type is not supported, the exception is thrown.</exception>
		protected override Expression VisitConstant(ConstantExpression node)
		{
			string value;

			if (node.Value == null)
			{
				value = "null";
			}
			else if (node.Type == typeof(string))
			{
				value = string.Format("\"{0}\"", node.Value.ToString());
			}
			else if (node.Type == typeof(bool))
			{
				value = this.ConvertToString((bool)node.Value);
			}
			else if (node.Type.IsPrimitive)
			{
				value = node.Value.ToString();
			}
			else if (node.Type == typeof(Type))
			{
				value = this.GetTypeString((Type)node.Value);
			}
			else
			{
				value = this.GetOuterVariableString(node.Value);
			}

			this.WriteLine(string.Format("{0}.Constant({1}, {2})",
				this.expressionTypeString,
				value,
				this.GetTypeString(node.Type)));

			return node;
		}
		/// <summary>
		/// Visits the <see cref="DebugInfoExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			return this.WriteNode
			(
				node,
				string.Format("{0}.DebugInfo", this.expressionTypeString),
				new Action<ExpressionConverter, DebugInfoExpression>[]
				{
					(that, x) => that.WriteSymbolDocumentInfo(x.Document),
					(that, x) => that.WriteLine(x.StartLine.ToString()),
					(that, x) => that.WriteLine(x.StartColumn.ToString()),
					(that, x) => that.WriteLine(x.EndLine.ToString()),
					(that, x) => that.WriteLine(x.EndColumn.ToString())
				}
			);
		}
		/// <summary>
		/// Visits the <see cref="DefaultExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitDefault(DefaultExpression node)
		{
			this.WriteLine(string.Format("{0}.Default({1})", this.expressionTypeString, this.GetTypeString(node.Type)));

			return node;
		}
		/// <summary>
		/// Visits the children of the <see cref="DynamicExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitDynamic(DynamicExpression node)
		{
			return this.WriteNode(
				node,
				string.Format("{0}.MakeDynamic", this.expressionTypeString),
				new Action<ExpressionConverter, DynamicExpression>[]
				{
					(that, x) => that.WriteLine(that.GetTypeString(x.DelegateType)),
					(that, x) => that.WriteBinder(x.Binder),
					(that, x) => that.WriteArray(x.Arguments, (t, y) => t.Visit(y))
				});
		}
		/// <summary>
		/// Visits the children of the <see cref="ElementInit" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override ElementInit VisitElementInit(ElementInit node)
		{
			return this.WriteNode(
				node,
				string.Format("{0}.ElementInit", this.expressionTypeString), 
				new Action<ExpressionConverter, ElementInit>[]
				{
					(that, x) => that.WriteLine(that.GetMemberInfoString(x.AddMethod)),
					(that, x) => that.WriteArray(x.Arguments, (t, y) => t.Visit(y))
				});
		}
		/// <summary>
		/// Visits the children of the extension expression.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		/// <exception cref="NotImplementedException">When this method is called, it will throw this exception.</exception>
		protected override Expression VisitExtension(Expression node)
		{
			throw new NotImplementedException("The Extension NodeType is currently not supported.");
		}
		/// <summary>
		/// Visits the children of the <see cref="GotoExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitGoto(GotoExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, GotoExpression>>();

			writePerArgument.Add((that, x) => that.VisitLabelTarget(x.Target));

			if (node.Value != null)
				writePerArgument.Add((that, x) => that.Visit(x.Value));

			writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Type)));

			return this.WriteNode(node, string.Format("{0}.Goto", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="IndexExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitIndex(IndexExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, IndexExpression>>();

			writePerArgument.Add((that, x) => that.Visit(x.Object));

			if (node.Indexer != null)
				writePerArgument.Add((that, x) => that.WriteLine(that.GetMemberInfoString(x.Indexer)));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			writePerArgument.Add((that, x) => that.WriteArray(x.Arguments, (t, y) => t.Visit(y)));

			return this.WriteNode(node, string.Format("{0}.MakeIndex", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="InvocationExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitInvocation(InvocationExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, InvocationExpression>>();

			writePerArgument.Add((that, x) => that.Visit(x.Expression));

			if (node.Arguments.Count > 0)
				writePerArgument.Add((that, x) => that.WriteArray(x.Arguments, (t, y) => t.Visit(y)));

			return this.WriteNode(node, string.Format("{0}.Invoke", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="LabelExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitLabel(LabelExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, LabelExpression>>();

			writePerArgument.Add((that, x) => that.VisitLabelTarget(x.Target));

			if (node.DefaultValue != null)
				writePerArgument.Add((that, x) => that.Visit(x.DefaultValue));

			return this.WriteNode(node, string.Format("{0}.Label", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the <see cref="LabelTarget" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override LabelTarget VisitLabelTarget(LabelTarget node)
		{
			this.WriteLine(this.GetLabelTargetString(node));

			return node;
		}
		/// <summary>
		/// Visits the <see cref="Expression&lt;T&gt;" />.
		/// </summary>
		/// <typeparam name="T">The type of the delegate.</typeparam>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			return this.WriteNode(
				node,
				string.Format("{0}.Lambda<{1}>",
					this.expressionTypeString,
					typeof(T).GetFriendlyName(this.fullyQualifyTypes)),
				new Action<ExpressionConverter, Expression<T>>[]
				{
					(that, x) => that.Visit(x.Body),
					(that, x) => that.WriteLine(string.Format("\"{0}\"", x.Name)),
					(that, x) => that.WriteLine(that.ConvertToString(x.TailCall)),
					(that, x) => that.WriteArray(x.Parameters, (t, y) => t.VisitParameter(y))
				});
		}
		/// <summary>
		/// Visits the children of the <see cref="ListInitExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitListInit(ListInitExpression node)
		{
			return this.WriteNode(
				node,
				string.Format("{0}.ListInit", this.expressionTypeString),
				new Action<ExpressionConverter, ListInitExpression>[]
				{
					(that, x) => that.Visit(x.NewExpression),
					(that, x) => that.WriteArray(x.Initializers, (t, y) => t.VisitElementInit(y))
				});
		}
		/// <summary>
		/// Visits the children of the <see cref="LoopExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitLoop(LoopExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, LoopExpression>>();

			writePerArgument.Add((that, x) => that.Visit(x.Body));

			if (node.BreakLabel != null)
				writePerArgument.Add((that, x) => that.VisitLabelTarget(x.BreakLabel));
			if (node.ContinueLabel != null)
				writePerArgument.Add((that, x) => that.VisitLabelTarget(x.ContinueLabel));

			return this.WriteNode(node, string.Format("{0}.Loop", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="MemberExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitMember(MemberExpression node)
		{
			Action<ExpressionConverter, MemberExpression> expressionAction;

			if (node.Expression != null)
				expressionAction = (that, x) => that.Visit(x.Expression);
			else
				expressionAction = (that, x) => that.WriteLine("null");

			return this.WriteNode(node, string.Format("{0}.MakeMemberAccess", this.expressionTypeString), new Action<ExpressionConverter, MemberExpression>[]
			{
				expressionAction,
				(that, x) => that.WriteLine(that.GetMemberInfoString(x.Member))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="MemberAssignment" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			return this.WriteNode(node, string.Format("{0}.Bind", this.expressionTypeString), new Action<ExpressionConverter, MemberAssignment>[]
			{
				(that, x) => that.WriteLine(that.GetMemberInfoString(x.Member)),
				(that, x) => that.Visit(x.Expression)
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="MemberListBinding" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			return this.WriteNode(node, string.Format("{0}.ListBind", this.expressionTypeString), new Action<ExpressionConverter, MemberListBinding>[]
			{
				(that, x) => that.WriteLine(that.GetMemberInfoString(x.Member)),
				(that, x) => that.WriteArray(x.Initializers, (t, y) => t.VisitElementInit(y))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="MemberMemberBinding" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			return this.WriteNode(node, string.Format("{0}.MemberBind", this.expressionTypeString), new Action<ExpressionConverter, MemberMemberBinding>[]
			{
				(that, x) => that.WriteLine(that.GetMemberInfoString(x.Member)),
				(that, x) => that.WriteArray(x.Bindings, (t, y) => t.VisitMemberBinding(y))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="MemberInitExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitMemberInit(MemberInitExpression node)
		{
			return this.WriteNode(node, string.Format("{0}.MemberInit", this.expressionTypeString), new Action<ExpressionConverter, MemberInitExpression>[]
			{
				(that, x) => that.Visit(x.NewExpression),
				(that, x) => that.WriteArray(x.Bindings, (t, y) => t.VisitMemberBinding(y))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="MethodCallExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, MethodCallExpression>>();

			if (node.Object != null)
				writePerArgument.Add((that, x) => that.Visit(x.Object));

			writePerArgument.Add((that, x) => that.WriteLine(that.GetMemberInfoString(x.Method)));

			if (node.Arguments.Count > 0)
				writePerArgument.Add((that, x) => that.WriteArray(x.Arguments, (t, y) => t.Visit(y)));

			return this.WriteNode(node, string.Format("{0}.Call", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="NewExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitNew(NewExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, NewExpression>>();

			writePerArgument.Add((that, x) => that.WriteLine(that.GetMemberInfoString(x.Constructor)));

			if (node.Arguments.Count > 0)
				writePerArgument.Add((that, x) => that.WriteArray(x.Arguments, (t, y) => t.Visit(y)));

			if ((node.Members != null) && (node.Members.Count > 0))
				writePerArgument.Add((that, x) => that.WriteArray(x.Members, (t, y) => t.WriteLine(that.GetMemberInfoString((y)))));

			return this.WriteNode(node, string.Format("{0}.New", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="NewArrayExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitNewArray(NewArrayExpression node)
		{
			string methodName;

			if (node.NodeType == ExpressionType.NewArrayBounds)
				methodName = string.Format("{0}.NewArrayBounds", this.expressionTypeString);
			else /* if (node.NodeType == ExpressionType.NewArrayInit) */
				methodName = string.Format("{0}.NewArrayInit", this.expressionTypeString);

			return this.WriteNode(node, methodName, new Action<ExpressionConverter, NewArrayExpression>[]
			{
				(that, x) => that.WriteLine(that.GetTypeString(x.Type.GetElementType())),
				(that, x) => that.WriteArray(x.Expressions, (t, y) => t.Visit(y))
			});
		}
		/// <summary>
		/// Visits the <see cref="ParameterExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitParameter(ParameterExpression node)
		{
			this.WriteLine(this.GetParameterName(node));

			return node;
		}
		/// <summary>
		/// Visits the children of the <see cref="RuntimeVariablesExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			return this.WriteNode(node, string.Format("{0}.RuntimeVariables", this.expressionTypeString), new Action<ExpressionConverter, RuntimeVariablesExpression>[]
			{
				(that, x) => that.WriteArray(x.Variables, (t, y) => t.VisitParameter(y))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="SwitchExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitSwitch(SwitchExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, SwitchExpression>>();

			writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Type)));
			writePerArgument.Add((that, x) => that.Visit(x.SwitchValue));

			if (node.DefaultBody != null)
				writePerArgument.Add((that, x) => that.Visit(x.DefaultBody));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			if (node.Comparison != null)
				writePerArgument.Add((that, x) => that.WriteLine(that.GetMemberInfoString(x.Comparison)));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			writePerArgument.Add((that, x) => that.WriteArray(x.Cases, (t, y) => that.VisitSwitchCase(y)));

			return this.WriteNode(node, string.Format("{0}.Switch", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="SwitchCase" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			return this.WriteNode(node, string.Format("{0}.SwitchCase", this.expressionTypeString), new Action<ExpressionConverter, SwitchCase>[]
			{
				(that, x) => that.Visit(x.Body),
				(that, x) => that.WriteArray(x.TestValues, (t, y) => t.Visit(y))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="TryExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitTry(TryExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, TryExpression>>();

			writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Type)));
			writePerArgument.Add((that, x) => that.Visit(x.Body));

			if (node.Finally != null)
				writePerArgument.Add((that, x) => that.Visit(x.Finally));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));


			if (node.Fault != null)
				writePerArgument.Add((that, x) => that.Visit(x.Fault));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			if (node.Handlers.Count > 0)
				writePerArgument.Add((that, x) => that.WriteArray(x.Handlers, (t, y) => t.VisitCatchBlock(y)));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			return this.WriteNode(node, string.Format("{0}.MakeTry", this.expressionTypeString), writePerArgument);
		}
		/// <summary>
		/// Visits the children of the <see cref="TypeBinaryExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			string methodName;

			if (node.NodeType == ExpressionType.TypeIs)
				methodName = string.Format("{0}.TypeIs", this.expressionTypeString);
			else /* if (node.NodeType == ExpressionType.TypeEqual) */
				methodName = string.Format("{0}.TypeEqual", this.expressionTypeString);

			return this.WriteNode(node, methodName, new Action<ExpressionConverter, TypeBinaryExpression>[]
			{
				(that, x) => that.Visit(x.Expression),
				(that, x) => that.WriteLine(that.GetTypeString(x.TypeOperand))
			});
		}
		/// <summary>
		/// Visits the children of the <see cref="UnaryExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitUnary(UnaryExpression node)
		{
			var writePerArgument = new List<Action<ExpressionConverter, UnaryExpression>>();

			writePerArgument.Add((that, x) => that.WriteLine(string.Format("{0}.{1}", typeof(ExpressionType).GetFriendlyName(that.fullyQualifyTypes), x.NodeType.ToString())));

			if (node.Operand != null)
				writePerArgument.Add((that, x) => that.Visit(x.Operand));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			writePerArgument.Add((that, x) => that.WriteLine(that.GetTypeString(x.Type)));

			if (node.Method != null)
				writePerArgument.Add((that, x) => that.WriteLine(that.GetMemberInfoString(x.Method)));
			else
				writePerArgument.Add((that, x) => that.WriteLine("null"));

			return this.WriteNode(node, string.Format("{0}.MakeUnary", this.expressionTypeString), writePerArgument);
		}

		#endregion Protected Code

		#region Private Code

		private readonly string expressionTypeString;
		private readonly Dictionary<ParameterExpression, string> parameters;
		private readonly Dictionary<ParameterExpression, string> variables;
		private readonly Dictionary<LabelTarget, string> labelTargets;
		private readonly Dictionary<Type, string> types;
		private readonly Dictionary<MemberInfo, string> memberInfos;
		private readonly Dictionary<object, string> outerVariables;
		private readonly List<string> declarations;
		private int indent;
		private int itemCount;

		#endregion Private Code
	}
}