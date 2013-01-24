using System;
using System.Collections.Generic;
using Vizistata.Reflection;

namespace Vizistata.Mocks {
	/// <summary>
	/// A mock disposable object that uses the reflection dispose functionality.  This class may not be inherited.
	/// </summary>
	internal sealed class MockReflectionDisposable : Object, IDisposable {
	// Fields
		/// <summary>
		/// An enumerable collection of disposable objects, or a null reference.
		/// </summary>
		private IEnumerable<DisposableBase> _disposableCollection;
		/// <summary>
		/// A disposable object, or a null reference.
		/// </summary>
		private DisposableBase _disposableObject;
		/// <summary>
		/// An <see cref="T:IntPtr"/> object.
		/// </summary>
		private IntPtr _intPtr;
		/// <summary>
		/// A non-disposable object, or a null reference.
		/// </summary>
		private Object _nonDisposableObject;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject) : this(nonDisposableObject, null, null, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		public MockReflectionDisposable(DisposableBase disposableObject) : this(null, disposableObject, null, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		public MockReflectionDisposable(IEnumerable<DisposableBase> disposableCollection) : this(null, null, disposableCollection, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(IntPtr intPtr) : this(null, null, null, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, DisposableBase disposableObject) : this(nonDisposableObject, disposableObject, null, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, IEnumerable<DisposableBase> disposableCollection) : this(nonDisposableObject, null, disposableCollection, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, IntPtr intPtr) : this(nonDisposableObject, null, null, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		public MockReflectionDisposable(DisposableBase disposableObject, IEnumerable<DisposableBase> disposableCollection) : this(null, disposableObject, disposableCollection, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(DisposableBase disposableObject, IntPtr intPtr) : this(null, disposableObject, null, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(IEnumerable<DisposableBase> disposableCollection, IntPtr intPtr) : this(null, null, disposableCollection, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, DisposableBase disposableObject, IEnumerable<DisposableBase> disposableCollection) : this(nonDisposableObject, disposableObject, disposableCollection, IntPtr.Zero) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, DisposableBase disposableObject, IntPtr intPtr) : this(nonDisposableObject, disposableObject, null, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, IEnumerable<DisposableBase> disposableCollection, IntPtr intPtr) : this(nonDisposableObject, null, disposableCollection, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(DisposableBase disposableObject, IEnumerable<DisposableBase> disposableCollection, IntPtr intPtr) : this(null, disposableObject, disposableCollection, intPtr) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		/// <param name="nonDisposableObject">A non-disposable object, or a null reference.</param>
		/// <param name="disposableObject">A disposable object, or a null reference.</param>
		/// <param name="disposableCollection">A disposable collection, or a null reference.</param>
		/// <param name="intPtr">An <see cref="T:IntPtr"/> value, or a null reference.</param>
		public MockReflectionDisposable(Object nonDisposableObject, DisposableBase disposableObject, IEnumerable<DisposableBase> disposableCollection, IntPtr intPtr)
			: base() {
			this._nonDisposableObject = nonDisposableObject;
			this._disposableObject = disposableObject;
			this._disposableCollection = disposableCollection;
			this._intPtr = intPtr;
		}
		/// <summary>
		/// Finalizes an instance of the <see cref="T:MockReflectionDisposable"/> class.
		/// </summary>
		~MockReflectionDisposable() {
			this.CallFinalize();
		}

	// Properties
		/// <summary>
		/// Gets the disposable collection provided when this instance was created, or a null reference.
		/// </summary>
		public IEnumerable<DisposableBase> DisposableCollection {
			get { return this._disposableCollection; }
		}
		/// <summary>
		/// Gets the dispoable object provided when this instance was created, or a null reference.
		/// </summary>
		public DisposableBase DisposableObject {
			get { return this._disposableObject; }
		}
		/// <summary>
		/// Gets the <see cref="T:IntPtr"/> object provided when this instance was created.
		/// </summary>
		public IntPtr IntPtr {
			get { return this._intPtr; }
		}
		/// <summary>
		/// Gets the non-disposable object provided when this instance was created, or a null reference.
		/// </summary>
		public Object NonDisposableObject {
			get { return this._nonDisposableObject; }
		}

	// Methods
		/// <summary>
		/// Invokes the dispose functionality.
		/// </summary>
		public void CallDispose() {
			this.InvokeDispose();
		}
		/// <summary>
		/// Invokes the finalize functionality.
		/// </summary>
		public void CallFinalize() {
			this.InvokeFinalize();
		}

		#region IDisposable Members
		void IDisposable.Dispose() {
			this.CallDispose();
		}
		#endregion
	}
}
