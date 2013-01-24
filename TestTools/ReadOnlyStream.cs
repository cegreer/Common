using System;
using System.IO;

namespace Vizistata.TestTools {
	/// <summary>
	/// Represents a read-only memory stream.  This class may not be inherited.
	/// </summary>
	public sealed class ReadOnlyStream : MemoryStream {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReadOnlyStream"/> class.
		/// </summary>
		/// <param name="buffer">The buffer to read.</param>
		public ReadOnlyStream(Byte[] buffer) : base(buffer) { }

	// Properties
		/// <summary>
		/// Gets a value indicating if this instance supports reading.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public override Boolean CanRead {
			get {
				this.ThrowIfDisposed();
				return base.CanRead;
			}
		}
		/// <summary>
		/// Gets a value indicating if this instance supports writing.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public override Boolean CanWrite {
			get {
				this.ThrowIfDisposed();
				return false;
			}
		}
		/// <summary>
		/// Gets or sets the write timeout value.
		/// </summary>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override Int32 WriteTimeout {
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}

	// Methods
		/// <summary>
		/// Begins an asynchronous write operation.
		/// </summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The byte offset in buffer from which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the write is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <returns>An <see cref="T:IAsyncResult"/> that represents the asynchronous write, which could still be pending.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Disposes of any resources held by this instance.
		/// </summary>
		/// <param name="disposing"><c>true</c> if this method is called from the <see cref="M:IDisposable.Dispose"/> method; otherwise, <c>false</c>.</param>
		protected override void Dispose(Boolean disposing) {
			base.Dispose(disposing);
			this._isDisposed = true;
		}
		/// <summary>
		/// Throws an <see cref="T:System.ObjectDisposedException"/> if this instance is disposed.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		private void ThrowIfDisposed() {
			if (this._isDisposed) {
				throw new ObjectDisposedException(this.ToString());
			}
		}
		/// <summary>
		/// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes.  This method copies count bytes from buffer to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override void Write(Byte[] buffer, Int32 offset, Int32 count) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
		/// </summary>
		/// <param name="value">The byte to write to the stream.</param>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override void WriteByte(Byte value) {
			throw new NotSupportedException();
		}
	}
}
