using System;
using System.IO;

namespace Vizistata.TestTools {
	/// <summary>
	/// Represents a write-only memory stream.  This class may not be inherited.
	/// </summary>
	public sealed class WriteOnlyStream : MemoryStream {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance has been disposed; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isDisposed;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:WriteOnlyStream"/> class.
		/// </summary>
		public WriteOnlyStream() : base() { }

	// Properties
		/// <summary>
		/// Gets a value indicating if this instance supports reading.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public override Boolean CanRead {
			get {
				this.ThrowIfDisposed();
				return false;
			}
		}
		/// <summary>
		/// Gets a value indicating if this instance supports writing.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">This instance has been disposed.</exception>
		public override Boolean CanWrite {
			get {
				this.ThrowIfDisposed();
				return base.CanWrite;
			}
		}
		/// <summary>
		/// Gets or sets the read timeout value.
		/// </summary>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override Int32 ReadTimeout {
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}

	// Methods
		/// <summary>
		/// Begins an asynchronous read operation.
		/// </summary>
		/// <param name="buffer">The buffer to read the data into.</param>
		/// <param name="offset">The byte offset in buffer at which to begin writing data read from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An <see cref="T:IAsyncResult"/> that represents the asynchronous read, which could still be pending.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object state) {
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
		/// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">An array of bytes.  When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer.  This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) {
			throw new NotSupportedException();
		}
		/// <summary>
		/// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
		/// </summary>
		/// <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.</returns>
		/// <exception cref="System.NotSupportedException">Always thrown.</exception>
		public override Int32 ReadByte() {
			throw new NotSupportedException();
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
	}
}
