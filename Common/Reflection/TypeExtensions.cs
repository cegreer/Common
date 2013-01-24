using System;
using System.Linq;
using System.Reflection;
using Vizistata.Linq;

using Res = Vizistata.Properties.Resources;

namespace Vizistata.Reflection {
	/// <summary>
	/// Provides reflection-based extension methods for the <see cref="T:Type"/> class.  This class may not be inherited.
	/// </summary>
	public static class TypeExtensions {
	// Methods
		/// <summary>
		/// Creates and returns an instance of the type specified.
		/// </summary>
		/// <param name="type">The type to create.</param>
		/// <param name="args">The array of arguments to supply to the constructor.</param>
		/// <returns>The instance created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.</exception>
		/// <exception cref="System.MissingMemberException">A constructor matching the types specified could not be found.</exception>
		public static Object CreateInstance(this Type type, params Object[] args) {
			Type[] argTypes = null;
			if (args != null) {
				argTypes = args.ToArray(arg => arg != null ? arg.GetType() : typeof(Object));
			}
			return TypeExtensions.CreateInstance(type, argTypes, args);
		}
		/// <summary>
		/// Creates and returns an instance of the type specified.
		/// </summary>
		/// <param name="type">The type to create.</param>
		/// <param name="argTypes">The array of types accepted in the constructor.</param>
		/// <param name="args">The array of arguments to supply to the constructor.</param>
		/// <returns>The instance created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">The length of <paramref name="args"/> does not match the length of <paramref name="argTypes"/>.</exception>
		/// <exception cref="System.MissingMemberException">A constructor matching the types specified could not be found.</exception>
		public static Object CreateInstance(this Type type, Type[] argTypes, Object[] args) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			if ((argTypes ?? new Type[0]).Length != (args ?? new Object[0]).Length) {
				throw new ArgumentException(Res.ArgumentLengthDoesNotMatchTypeLengthMessage, "args");
			}

			BindingFlags bindingFlags = BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, argTypes ?? new Type[0], null);
			if (constructor == null) {
				throw new MissingMemberException(Res.ConstructorNotFoundMessage);
			}
			try {
				return constructor.Invoke(args ?? new Object[0]);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Finds a field on the type specified.  Base types are also checked.
		/// </summary>
		/// <param name="type">The type on which to find the field.</param>
		/// <param name="name">The name of the field.</param>
		/// <param name="isStatic"><c>true</c> if the field is static; otherwise, <c>false</c>.</param>
		/// <returns>The field found, or a null reference if it is not found.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		public static FieldInfo FindField(this Type type, String name, Boolean isStatic) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic) {
				bindingFlags |= BindingFlags.Static;
			}
			else {
				bindingFlags |= BindingFlags.Instance;
			}

			Type currentType = type;
			while (currentType != typeof(Object)) {
				foreach (FieldInfo fieldInfo in currentType.GetFields(bindingFlags)) {
					if (fieldInfo.Name == name) {
						return fieldInfo;
					}
				}
				currentType = currentType.BaseType;
			}
			return null;
		}
		/// <summary>
		/// Finds the nested type specified.
		/// </summary>
		/// <param name="type">The type on which to search.</param>
		/// <param name="name">The name of the nested type.</param>
		/// <returns>The type found.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		public static Type FindNestedType(this Type type, String name) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			return type.GetNestedType(name, BindingFlags.Public | BindingFlags.NonPublic);
		}
		/// <summary>
		/// Finds a property on the type specified.  Base types are also checked.
		/// </summary>
		/// <param name="type">The type on which to find the property.</param>
		/// <param name="name">The name of the property.</param>
		/// <param name="isStatic"><c>true</c> if the property is static; otherwise, <c>false</c>.</param>
		/// <returns>The property found, or a null reference if it is not found.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		public static PropertyInfo FindProperty(this Type type, String name, Boolean isStatic) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic) {
				bindingFlags |= BindingFlags.Static;
			}
			else {
				bindingFlags |= BindingFlags.Instance;
			}

			Type currentType = type;
			while (currentType != typeof(Object)) {
				foreach (PropertyInfo propertyInfo in currentType.GetProperties(bindingFlags)) {
					if (propertyInfo.Name == name) {
						return propertyInfo;
					}
				}
				currentType = currentType.BaseType;
			}
			return null;
		}
	}
}
