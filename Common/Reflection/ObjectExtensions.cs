using System;
using System.Linq;
using System.Reflection;
using Vizistata.Linq;

using Res = Vizistata.Properties.Resources;

namespace Vizistata.Reflection {
	/// <summary>
	/// Provides reflection-based extension methods for the <see cref="T:Object"/> class.  This class may not be inherited.
	/// </summary>
	public static class ObjectExtensions {
	// Methods
		/// <summary>
		/// Returns the value of the field specified.
		/// </summary>
		/// <param name="instance">The instance from which to get the field value.  For static fields, this should be the type.</param>
		/// <param name="name">The name of the field.</param>
		/// <returns>The value of the field.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.MissingMemberException">The field specified cannot be found.</exception>
		public static Object GetFieldValue(this Object instance, String name) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = type != null;
			if (type == null) {
				type = instance.GetType();
			}

			FieldInfo field = type.FindField(name, isStatic);

			if (field == null) {
				throw new MissingMemberException(Res.MemberNotOnTypeFormat.FormatInvariant(name, isStatic ? Res.AStatic : Res.AnInstance, Res.Field));
			}
			try {
				return field.GetValue(isStatic ? null : instance);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Returns the value of the property specified.
		/// </summary>
		/// <param name="instance">The instance from which to get the property value.  For static properties, this should be the type.</param>
		/// <param name="name">The name of the property.</param>
		/// <returns>The value of the property.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.MissingMemberException">The property specified cannot be found.</exception>
		/// <exception cref="System.InvalidOperationException">The property specified is write-only.</exception>
		public static Object GetPropertyValue(this Object instance, String name) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = type != null;
			if (type == null) {
				type = instance.GetType();
			}

			PropertyInfo property = type.FindProperty(name, isStatic);

			if (property == null) {
				throw new MissingMemberException(Res.MemberNotOnTypeFormat.FormatInvariant(name, isStatic ? Res.AStatic : Res.AnInstance, Res.Property));
			}

			if (!property.CanRead) {
				throw new InvalidOperationException(Res.PropertyIsWriteOnlyMessage);
			}

			try {
				return property.GetValue(isStatic ? null : instance, null);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Invokes a method and returns the result.
		/// </summary>
		/// <param name="instance">The object or type from which to invoke the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="args">The array of arguments to supply to the method.</param>
		/// <returns>The instance created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		/// <exception cref="System.MissingMemberException">A method matching the types specified could not be found.</exception>
		public static Object InvokeMethod(this Object instance, String name, params Object[] args) {
			Type[] argTypes = null;
			if (args != null) {
				argTypes = args.ToArray(arg => arg != null ? arg.GetType() : typeof(Object));
			}
			return ObjectExtensions.InvokeMethod(instance, name, argTypes, args);
		}
		/// <summary>
		/// Invokes a method and returns the result.
		/// </summary>
		/// <param name="instance">The object or type from which to invoke the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="argTypes">The array of argument types expected.</param>
		/// <param name="args">The array of arguments to supply to the method.</param>
		/// <returns>The return result of the method.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">The length of <paramref name="args"/> does not match the length of <paramref name="argTypes"/>.</exception>
		/// <exception cref="System.MissingMemberException">A method matching the types specified could not be found.</exception>
		public static Object InvokeMethod(this Object instance, String name, Type[] argTypes, Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			if ((argTypes ?? new Type[0]).Length != (args ?? new Object[0]).Length) {
				throw new ArgumentException(Res.ArgumentLengthDoesNotMatchTypeLengthMessage, "args");
			}

			Type type = instance as Type;
			Boolean isStatic = type != null;
			if (type == null) {
				type = instance.GetType();
			}

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic) {
				bindingFlags |= BindingFlags.Static;
			}
			else {
				bindingFlags |= BindingFlags.Instance;
			}

			MethodInfo method = type.GetMethod(name, bindingFlags, null, argTypes ?? new Type[0], null);
			if (method == null) {
				throw new MissingMemberException(Res.MethodNotFoundMessage);
			}
			try {
				Object target = !isStatic ? instance : null;
				return method.Invoke(target, args ?? new Object[0]);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Sets a field to a specified value.
		/// </summary>
		/// <param name="instance">The instance on which to set the field.  For static fields, this should be the type.</param>
		/// <param name="name">The name of the field.</param>
		/// <param name="value">The value to which to set the field.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.MissingMemberException">The field specified cannot be found.</exception>
		/// <exception cref="System.InvalidOperationException">The field specified is read-only.</exception>
		/// <exception cref="System.InvalidCastException">The value specified does not match the type of the field.</exception>
		public static void SetFieldValue(this Object instance, String name, Object value) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = type != null;
			if (type == null) {
				type = instance.GetType();
			}

			FieldInfo field = type.FindField(name, isStatic);

			if (field == null) {
				throw new MissingMemberException(Res.MemberNotOnTypeFormat.FormatInvariant(name, isStatic ? Res.AStatic : Res.AnInstance, Res.Field));
			}

			if (field.IsInitOnly) {
				throw new InvalidOperationException(Res.FieldIsReadOnlyMessage);
			}

			if (value == null) {
				if (!field.FieldType.IsByRef) {
					throw new InvalidCastException();
				}
			}
			else {
				if (!field.FieldType.IsAssignableFrom(value.GetType())) {
					throw new InvalidCastException();
				}
			}
			field.SetValue(isStatic ? null : instance, value);
		}
		/// <summary>
		/// Sets a property to a specified value.
		/// </summary>
		/// <param name="instance">The instance on which to set the property.  For static properties, this should be the type.</param>
		/// <param name="name">The name of the field.</param>
		/// <param name="value">The value to which to set the field.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.MissingMemberException">The property specified cannot be found.</exception>
		/// <exception cref="System.InvalidOperationException">The property is read-only.</exception>
		/// <exception cref="System.InvalidCastException">The value specified does not match the type of the property.</exception>
		public static void SetPropertyValue(this Object instance, String name, Object value) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = type != null;
			if (type == null) {
				type = instance.GetType();
			}

			PropertyInfo property = type.FindProperty(name, isStatic);

			if (property == null) {
				throw new MissingMemberException(Res.MemberNotOnTypeFormat.FormatInvariant(name, isStatic ? Res.AStatic : Res.AnInstance, Res.Field));
			}

			if (!property.CanWrite) {
				throw new InvalidOperationException(Res.PropertyIsReadOnlyMessage);
			}

			if (value == null) {
				if (!property.PropertyType.IsByRef) {
					throw new InvalidCastException();
				}
			}
			else {
				if (!property.PropertyType.IsAssignableFrom(value.GetType())) {
					throw new InvalidCastException();
				}
			}
			property.SetValue(isStatic ? null : instance, value, null);
		}
	}
}
