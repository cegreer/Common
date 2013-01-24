using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

using StringBuilder = System.Text.StringBuilder;
using Res = Vizistata.Windows.Forms.Properties.Resources;

namespace Vizistata.Windows.Forms {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Form"/> class.  This class may not be inherited.
	/// </summary>
	public static class FormExtensions {
	// Methods
		/// <summary>
		/// Creates a "loading" form.
		/// </summary>
		/// <param name="instance">The instance on which to invoke the method.</param>
		/// <param name="displayText">The text to display.</param>
		/// <param name="action">The action to execute.</param>
		/// <returns>A reference to the <see cref="T:LoadingForm"/> created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="displayText"/> is a null reference.
		/// -or- <paramref name="action"/> is a null reference.</exception>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is a factory method.")]
		public static LoadingForm CreateLoadingForm(this Form instance, String displayText, Action action) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			LoadingForm loadingForm = new LoadingForm(displayText, action);
			loadingForm.BackColor = instance.BackColor;
			loadingForm.Owner = instance;
			return loadingForm;
		}
		/// <summary>
		/// Prompts the user with "Yes" and "No" buttons.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="caption">The caption of the message box.</param>
		/// <returns><see cref="F:DialogResult.Yes"/> or <see cref="F:DialogResult.No"/>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static DialogResult PromptYesNo(this Form instance, String text, String caption) {
			return FormExtensions.ShowMessageBox(instance, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
		}
		/// <summary>
		/// Displays an error message to the user.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="caption">The caption of the message box.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static void ShowError(this Form instance, String text, String caption) {
			FormExtensions.ShowMessageBox(instance, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
		}
		/// <summary>
		/// Displas an error message to the user.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="caption">The caption of the message box.</param>
		/// <param name="error">The optional exception to display.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="text"/> is a null reference.</exception>
		public static void ShowError(this Form instance, String text, String caption, Exception error) {
			if (error == null) {
				FormExtensions.ShowError(instance, text, caption);
				return;
			}

			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (text == null) {
				throw new ArgumentNullException("text");
			}

			StringBuilder messageBuilder = new StringBuilder();
			messageBuilder.AppendLine(text);
			messageBuilder.AppendLine();
			messageBuilder.AppendLine("Exception:");
			messageBuilder.AppendLine(error.Message);

			Exception innerException = error.InnerException;
			while (innerException != null) {
				messageBuilder.AppendLine();
				messageBuilder.AppendLine("Inner exception:");
				messageBuilder.AppendLine(innerException.Message);
				innerException = innerException.InnerException;
			}

			FormExtensions.ShowMessageBox(instance, messageBuilder.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
		}
		/// <summary>
		/// Displays an informational message to the user.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="caption">The caption of the message box.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static void ShowInformation(this Form instance, String text, String caption) {
			FormExtensions.ShowMessageBox(instance, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
		}
		/// <summary>
		/// Displays the form or provides focus to it if already displayed.
		/// </summary>
		/// <param name="instance">The form to show or focus.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static void ShowOrFocus(this Form instance) {
			FormExtensions.ShowOrFocus(instance, null);
		}
		/// <summary>
		/// Displays the form or provides focus to it if already displayed.
		/// </summary>
		/// <param name="instance">The form to show or focus.</param>
		/// <param name="owner">The optional owner of the form.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static void ShowOrFocus(this Form instance, IWin32Window owner) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (!instance.Visible) {
				instance.Show(owner);
			}
			else {
				instance.Focus();
			}
		}
		/// <summary>
		/// Displays an unexpected error message to the user.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="exception">The error that occurred.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="exception"/> is a null reference.</exception>
		public static void ShowUnexpectedError(this Form instance, Exception exception) {
			if (exception == null) {
				throw new ArgumentNullException("exception");
			}
			String text = Res.UnexpectedErrorPrefix + exception.ToString();
			String caption = Res.UnexpectedErrorCaption;
			FormExtensions.ShowError(instance, text, caption);
		}
		/// <summary>
		/// Displays a warning message to the user.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="caption">The caption of the message box.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		public static void ShowWarning(this Form instance, String text, String caption) {
			FormExtensions.ShowMessageBox(instance, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
		}
		/// <summary>
		/// Shows a message box.
		/// </summary>
		/// <param name="instance">The instance on which to invoke this method.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="caption">The caption of the message box.</param>
		/// <param name="buttons">The buttons to display.</param>
		/// <param name="icon">The icon to display.</param>
		/// <param name="defaultButton">The default button.</param>
		/// <returns>The button pushed by the user.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		private static DialogResult ShowMessageBox(Form instance, String text, String caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			MessageBoxOptions options = instance.RightToLeftLayout ? MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading : (MessageBoxOptions)0;
			return MessageBox.Show(instance, text, caption, buttons, icon, defaultButton, options);
		}
	}
}
