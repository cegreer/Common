using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using IEnumerable = System.Collections.IEnumerable;

namespace Vizistata.Reflection {
	/// <summary>
	/// Provides extensions methods for the <see cref="T:IDisposable"/> interface.  This class may not be inherited.
	/// </summary>
	public static class DisposableExtensions {
	// Constants
		/// <summary>
		/// Represents all instance members = Instance | Public | NonPublic.
		/// </summary>
		private const BindingFlags AllInstanceMembers = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	// Methods
		/// <summary>
		/// Assists in disposing or finalizing instances of objects.
		/// </summary>
		/// <param name="disposable">The instance to dispose.</param>
		/// <param name="isDisposing"><c>true</c> if called from the <see cref="M:IDisposable.Dispose"/> method; otherwise, <c>false</c>.</param>
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a valid implementation of the Disposable pattern.")]
		private static void Dispose(IDisposable disposable, Boolean isDisposing) {
			Debug.Assert(disposable != null);
			
			// Dispose of the fields in each type down to, but excluding, System.Object
			Type type = disposable.GetType();
			do {
				FieldInfo[] instanceFields = type.GetFields(AllInstanceMembers);
				if (isDisposing) {
					DisposableExtensions.ReleaseManagedResources(disposable, instanceFields);
				}
				DisposableExtensions.ReleaseUnmanagedResources(disposable, instanceFields);
			}
			while ((type = type.BaseType) != typeof(Object));

			if (isDisposing) {
				GC.SuppressFinalize(disposable);
			}
		}
		/// <summary>
		/// Assists in disposing an instance of a class without requiring a full implementation.  Only call this method from the Dispose() method.
		/// </summary>
		/// <param name="disposable">The instance whose fields should be disposed.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="disposable"/> is a null reference.</exception>
		public static void InvokeDispose(this IDisposable disposable) {
			if (disposable == null) {
				throw new ArgumentNullException("disposable");
			}

			DisposableExtensions.Dispose(disposable, true);
		}
		/// <summary>
		/// Assists in finalizing an instance of a class without requiring a full implementation.  Only call this method from the Finalize() method.
		/// </summary>
		/// <param name="disposable">The instance whose fields should be disposed.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="disposable"/> is a null reference.</exception>
		public static void InvokeFinalize(this IDisposable disposable) {
			if (disposable == null) {
				throw new ArgumentNullException("disposable");
			}

			DisposableExtensions.Dispose(disposable, false);
		}
		/// <summary>
		/// Assists in releasing any managed resources from the fields specified.
		/// </summary>
		/// <param name="disposable">The instance whose fields should be disposed.</param>
		/// <param name="fields">The enumerable collection of fields to dispose.</param>
		private static void ReleaseManagedResources(Object disposable, IEnumerable<FieldInfo> fields) {
			Debug.Assert(disposable != null);
			Debug.Assert(fields != null);
			Debug.Assert(!fields.Any(field => field.IsStatic));

			// Try to dispose each field
			foreach (FieldInfo fieldInfo in fields) {
				if (!fieldInfo.IsInitOnly) {
					Object value = fieldInfo.GetValue(disposable);
					
					// Try IDisposable first
					IDisposable disposableFieldValue = value as IDisposable;
					if (disposableFieldValue != null) {
						Type disposableType = disposableFieldValue.GetType();
						disposableFieldValue.Dispose();
						if (disposableType.IsClass) {
							fieldInfo.SetValue(disposable, null);
						}
						continue;
					}

					// Try to dispose of the objects in a collection
					IEnumerable enumerableFieldValue = value as IEnumerable;
					if (enumerableFieldValue != null && !(enumerableFieldValue is String)) {
						foreach (Object element in enumerableFieldValue) {
							IDisposable disposableElement = element as IDisposable;
							if (disposableElement != null) {
								disposableElement.Dispose();
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Assists in releasing any unmanaged resources from the fields specified.
		/// </summary>
		/// <param name="disposable">The instance whose fields should be disposed.</param>
		/// <param name="fields">The enumerable collection of fields to dispose.</param>
		private static void ReleaseUnmanagedResources(Object disposable, IEnumerable<FieldInfo> fields) {
			Debug.Assert(disposable != null);
			Debug.Assert(fields != null);
			Debug.Assert(!fields.Any(field => field.IsStatic));

			// Try to dispose each field
			foreach (FieldInfo fieldInfo in fields) {
				if (!fieldInfo.IsInitOnly) {
					Object value = fieldInfo.GetValue(disposable);

					if (value != null) {
						Type fieldType = value.GetType();
						if (fieldType == typeof(IntPtr)) {
							if (!IntPtr.Zero.Equals(value)) {
								fieldInfo.SetValue(disposable, IntPtr.Zero);
							}
						}
						else if (fieldType == typeof(UIntPtr)) {
							if (!UIntPtr.Zero.Equals(value)) {
								fieldInfo.SetValue(disposable, UIntPtr.Zero);
							}
						}
#warning // TODO: Add other unsafe types here.
					}
				}
			}
		}
	}
}
