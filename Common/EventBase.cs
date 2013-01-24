using System;

namespace Vizistata {
	/// <summary>
	/// The base class for objects that represent events.
	/// </summary>
	[Serializable()]
	public abstract class EventBase : Object {
	// Fields
		/// <summary>
		/// <c>true</c> if the all event handlers should be treated as non-serialized; otherwise, <c>false</c>.  This field is read-only.
		/// </summary>
		private readonly Boolean _ignoreSerializability;
		/// <summary>
		/// The collection of event handlers that cannot be serialized.
		/// </summary>
		[NonSerialized()]
		private Delegate _nonSerializedHandlers;
		/// <summary>
		/// The collection of event handlers that can be serialized.
		/// </summary>
		private Delegate _serializedHandlers;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventBase"/> class.
		/// </summary>
		internal EventBase() : this(false) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventBase"/> class.
		/// </summary>
		/// <param name="ignoreSerializability"><c>true</c> to treat all event handlers as non-serialized; otherwise, <c>false</c>.  Setting this value to <c>true</c> will result in performance improvements, but all event handlers will be lost during serialization.</param>
		internal EventBase(Boolean ignoreSerializability)
			: base() {
			this._ignoreSerializability = ignoreSerializability;
		}

	// Properties
		/// <summary>
		/// Gets the event handler delegate for the event.
		/// </summary>
		protected Delegate EventHandler {
			get { return Delegate.Combine(this._nonSerializedHandlers, this._serializedHandlers); }
		}

	// Methods
		/// <summary>
		/// Adds the delegate specified.
		/// </summary>
		/// <param name="value">The delegate to add.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		protected void AddDelegate(Delegate value) {
			if (value == null) {
				throw new ArgumentNullException("value");
			}

			if (this.ShouldSerialize(value)) {
				this._serializedHandlers = Delegate.Combine(this._serializedHandlers, value);
			}
			else {
				this._nonSerializedHandlers = Delegate.Combine(this._nonSerializedHandlers, value);
			}
		}
		/// <summary>
		/// Removes the delegate specified.
		/// </summary>
		/// <param name="value">The delegate to remove.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		protected void RemoveDelegate(Delegate value) {
			if (value == null) {
				throw new ArgumentNullException("value");
			}

			if (this.ShouldSerialize(value)) {
				this._serializedHandlers = Delegate.Remove(this._serializedHandlers, value);
			}
			else {
				this._nonSerializedHandlers = Delegate.Remove(this._nonSerializedHandlers, value);
			}
		}
		/// <summary>
		/// Returns a value indicating if the delegate should be serialized.
		/// </summary>
		/// <param name="value">The delegate to check.</param>
		/// <returns><c>true</c> if the delegate can and should be serialized; otherwise, <c>false</c>.</returns>
		private Boolean ShouldSerialize(Delegate value) {
			if (this._ignoreSerializability) {
				return false;
			}
			return value == null || value.Method.IsStatic || value.Method.DeclaringType.IsSerializable;
		}
	}
}
