using System;
using System.Diagnostics.CodeAnalysis;

namespace Vizistata {
	/// <summary>
	/// Represents an event object for <see cref="T:EventHandler"/> delegates.  This class may not be inherited.
	/// </summary>
	[Serializable()]
	public sealed class EventObject : EventBase {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventObject"/> class.
		/// </summary>
		public EventObject() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventObject"/> class.
		/// </summary>
		/// <param name="ignoreSerializable"><c>true</c> to treat all event handlers as non-serialized; otherwise, <c>false</c>.  Setting this value to <c>true</c> will result in performance improvements, but all event handlers will be lost during serialization.</param>
		public EventObject(Boolean ignoreSerializable) : base(ignoreSerializable) { }

	// Methods
		/// <summary>
		/// Adds an event handler to the delegate.
		/// </summary>
		/// <param name="value">The event handler delegate to add.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public void AddHandler(EventHandler value) {
			this.AddDelegate(value);
		}
		/// <summary>
		/// Raises the event.
		/// </summary>
		/// <param name="sender">The object sending the event.</param>
		/// <param name="e">Provides information about the event.</param>
		[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This type represents an event.")]
		public void RaiseEvent(Object sender, EventArgs e) {
			EventHandler eventHandler = (EventHandler)this.EventHandler;
			if (eventHandler != null) {
				eventHandler(sender, e);
			}
		}
		/// <summary>
		/// Removes an event handler from the delegate.
		/// </summary>
		/// <param name="value">The event handler delegate to remove.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public void RemoveHandler(EventHandler value) {
			this.RemoveDelegate(value);
		}
	}
	/// <summary>
	/// Represents an event object for <see cref="T:EventHandler&lt;T&gt;"/> delegates.  This class may not be inherited.
	/// </summary>
	/// <typeparam name="T">The type of <see cref="T:EventArgs"/> object.</typeparam>
	[Serializable()]
	public sealed class EventObject<T> : EventBase where T : EventArgs {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventObject&lt;T&gt;"/> class.
		/// </summary>
		public EventObject() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EventObject&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="ignoreSerializable"><c>true</c> to treat all event handlers as non-serialized; otherwise, <c>false</c>.  Setting this value to <c>true</c> will result in performance improvements, but all event handlers will be lost during serialization.</param>
		public EventObject(Boolean ignoreSerializable) : base(ignoreSerializable) { }

	// Methods
		/// <summary>
		/// Adds an event handler to the delegate.
		/// </summary>
		/// <param name="value">The event handler delegate to add.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public void AddHandler(EventHandler<T> value) {
			this.AddDelegate(value);
		}
		/// <summary>
		/// Raises the event.
		/// </summary>
		/// <param name="sender">The object sending the event.</param>
		/// <param name="e">Provides information about the event.</param>
		[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This type represents an event.")]
		public void RaiseEvent(Object sender, T e) {
			EventHandler<T> eventHandler = (EventHandler<T>)this.EventHandler;
			if (eventHandler != null) {
				eventHandler(sender, e);
			}
		}
		/// <summary>
		/// Removes an event handler from the delegate.
		/// </summary>
		/// <param name="value">The event handler delegate to remove.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public void RemoveHandler(EventHandler<T> value) {
			this.RemoveDelegate(value);
		}
	}
}
