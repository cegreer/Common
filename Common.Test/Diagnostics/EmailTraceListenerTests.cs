using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WindowsIdentity = System.Security.Principal.WindowsIdentity;

namespace Vizistata.Diagnostics {
	/// <summary>
	/// This is a test class for <see cref="T:EmailTraceListener"/> and is intended to contain all <see cref="T:EmailTraceListener"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class EmailTraceListenerTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmailTraceListenerTests"/> class.
		/// </summary>
		public EmailTraceListenerTests() : base() { }

		/// <summary>
		/// Gets or sets the test context which provides information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get { return this._testContextInstance; }
			set { this._testContextInstance = value; }
		}
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion
		#endregion

	// Fields
		/// <summary>
		/// THIS FIELD SHOULD ONLY BE REFERENCED FROM THE <see cref="P:CurrentUserEmailAddress"/> PROPERTY.  The cached e-mail address for the current user.
		/// </summary>
		private static String _currentUserEmailAddress;

	// Properties
		/// <summary>
		/// Gets the e-mail address for the current user.
		/// </summary>
		private static String CurrentUserEmailAddress {
			get {
				if (EmailTraceListenerTests._currentUserEmailAddress == null) {
					WindowsIdentity identity = WindowsIdentity.GetCurrent();
					String fullUserName = identity.Name;
					String userName = fullUserName.Split('\\')[1];
					String emailAddress = userName + "@ParivedaSolutions.com";
					EmailTraceListenerTests._currentUserEmailAddress = emailAddress;
				}
				return EmailTraceListenerTests._currentUserEmailAddress;
			}
		}

	// Constructor Tests
		[TestMethod()]
		[Description(".ctor() constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor1_Optimal() {
			using (new EmailTraceListener()) { }
		}

		[TestMethod()]
		[Description(".ctor(MailAddress) constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor2_Optimal() {
			MailAddress toAddress = new MailAddress(EmailTraceListenerTests.CurrentUserEmailAddress);
			using (new EmailTraceListener(toAddress)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddress) constructor when 'toAddress' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailTraceListener_Unit_Constructor2_ToAddressIsNull() {
			MailAddress toAddress = null;
			using (new EmailTraceListener(toAddress)) { }
		}

		[TestMethod()]
		[Description(".ctor(MailAddress, String) constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor3_Optimal() {
			MailAddress toAddress = new MailAddress(EmailTraceListenerTests.CurrentUserEmailAddress);
			String name = "MyListener";
			using (new EmailTraceListener(toAddress, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddress, String) constructor when 'toAddress' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailTraceListener_Unit_Constructor3_ToAddressIsNull() {
			MailAddress toAddress = null;
			String name = "MyListener";
			using (new EmailTraceListener(toAddress, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddress, String) constructor when 'name' is a null reference.")]
		public void EmailTraceListener_Unit_Constructor3_NameIsNull() {
			MailAddress toAddress = new MailAddress(EmailTraceListenerTests.CurrentUserEmailAddress);
			String name = null;
			using (new EmailTraceListener(toAddress, name)) { }
		}

		[TestMethod()]
		[Description(".ctor(MailAddressCollection) constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor4_Optimal() {
			MailAddressCollection toAddresses = new MailAddressCollection() {
				new MailAddress(EmailTraceListenerTests.CurrentUserEmailAddress)
			};
			using (new EmailTraceListener(toAddresses)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddressCollection) constructor when 'toAddresses' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailTraceListener_Unit_Constructor4_ToAddressesIsNull() {
			MailAddressCollection toAddresses = null;
			using (new EmailTraceListener(toAddresses)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddressCollection) constructor when 'toAddresses' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailTraceListener_Unit_Constructor4_ToAddressesIsEmpty() {
			MailAddressCollection toAddresses = new MailAddressCollection();
			using (new EmailTraceListener(toAddresses)) { }
		}

		[TestMethod()]
		[Description(".ctor(MailAddressCollection, String) constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor5_Optimal() {
			MailAddressCollection toAddresses = new MailAddressCollection() {
				new MailAddress(EmailTraceListenerTests.CurrentUserEmailAddress)
			};
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddressCollection, String) constructor when 'toAddresses' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailTraceListener_Unit_Constructor5_ToAddressesIsNull() {
			MailAddressCollection toAddresses = null;
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddressCollection, String) constructor when 'toAddresses' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailTraceListener_Unit_Constructor5_ToAddressesIsEmpty() {
			MailAddressCollection toAddresses = new MailAddressCollection();
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(MailAddressCollection, String) constructor when 'name' is a null reference.")]
		public void EmailTraceListener_Unit_Constructor5_NameIsNull() {
			MailAddressCollection toAddresses = new MailAddressCollection() {
				new MailAddress(EmailTraceListenerTests.CurrentUserEmailAddress)
			};
			String name = null;
			using (new EmailTraceListener(toAddresses, name)) { }
		}

		[TestMethod()]
		[Description(".ctor(String) constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor6_Optimal() {
			String toAddresses = EmailTraceListenerTests.CurrentUserEmailAddress + ",SomeoneElse@Nowhere.com";
			using (new EmailTraceListener(toAddresses)) { }
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'toAddresses' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailTraceListener_Unit_Constructor6_ToAddressesIsNull() {
			String toAddresses = null;
			using (new EmailTraceListener(toAddresses)) { }
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'toAddresses' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailTraceListener_Unit_Constructor6_ToAddressesIsEmpty() {
			String toAddresses = String.Empty;
			using (new EmailTraceListener(toAddresses)) { }
		}
		[TestMethod()]
		[Description(".ctor(String) constructor when 'toAddresses' is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void EmailTraceListener_Unit_Constructor6_ToAddressesIsInvalid() {
			String toAddresses = "  \t This is \r not \n valid.";
			using (new EmailTraceListener(toAddresses)) { }
		}

		[TestMethod()]
		[Description(".ctor(String, String) constructor for the optimal path.")]
		public void EmailTraceListener_Unit_Constructor7_Optimal() {
			String toAddresses = EmailTraceListenerTests.CurrentUserEmailAddress + ",SomeoneElse@Nowhere.com";
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'toAddresses' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void EmailTraceListener_Unit_Constructor7_ToAddressesIsNull() {
			String toAddresses = null;
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'toAddresses' is empty.")]
		[ExpectedException(typeof(ArgumentException))]
		public void EmailTraceListener_Unit_Constructor7_ToAddressesIsEmpty() {
			String toAddresses = String.Empty;
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'toAddresses' is invalid.")]
		[ExpectedException(typeof(FormatException))]
		public void EmailTraceListener_Unit_Constructor7_ToAddressesIsInvalid() {
			String toAddresses = "  \t This is \r not \n valid.";
			String name = "MyListener";
			using (new EmailTraceListener(toAddresses, name)) { }
		}
		[TestMethod()]
		[Description(".ctor(String, String) constructor when 'name' is a null reference.")]
		public void EmailTraceListener_Unit_Constructor7_NameIsNull() {
			String toAddresses = EmailTraceListenerTests.CurrentUserEmailAddress + ",SomeoneElse@Nowhere.com";
			String name = null;
			using (new EmailTraceListener(toAddresses, name)) { }
		}

	// Property Tests
		[TestMethod()]
		[Description("Lines property when there are lines.")]
		public void EmailTraceListener_Unit_Lines_HasNoLines() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String message = "This is a test.";
				target.WriteLine(message);

				IEnumerable<String> actual = target.LinesDerived;
				Assert.AreEqual(1, actual.Count());
				Assert.AreEqual(message, actual.Single());
			}
		}
		[TestMethod()]
		[Description("Lines property when there are no lines.")]
		public void EmailTraceListener_Unit_Lines_HasLines() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {

				IEnumerable<String> actual = target.LinesDerived;
				Assert.AreEqual(0, actual.Count());
			}
		}

	// Method Tests
		[TestMethod()]
		[Description("CreateMailBody() method for the optimal path.")]
		public void EmailTraceListener_Unit_CreateMailBody_Optimal() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String message1 = "This is a test.";
				String message2 = "This is another test.";
				target.WriteLine(message1);
				target.WriteLine(message2);

				String actual = target.CreateMailBodyDerived();
				StringAssert.Contains(actual, message1);
				StringAssert.Contains(actual, message2);
			}
		}
		[TestMethod()]
		[Description("CreateMailBody() method when there are no lines.")]
		public void EmailTraceListener_Unit_CreateMailBody_HasNoLines() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {

				String actual = target.CreateMailBodyDerived();
				Assert.AreEqual(String.Empty, actual);
			}
		}

		[TestMethod()]
		[Description("CreateMailSubject() method for the optimal path.")]
		public void EmailTraceListener_Unit_CreateMailSubject_Optimal() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String actual = target.CreateMailSubjectDerived();
				Assert.IsNotNull(actual);
			}
		}

		[TestMethod()]
		[Description("Flush() method for the optimal path.")]
		public void EmailTraceListener_System_Flush_Optimal() {
			using (MockEmailTraceListener target = new MockEmailTraceListener(EmailTraceListenerTests.CurrentUserEmailAddress, "'Should Receive' Listener")) {
				String message = "This is a test.";
				target.WriteLine(message);

				try {
					// This line should generate an e-mail.
					target.Flush();
				}
				catch (SmtpException ex) {
					Assert.Inconclusive("SMTP is not enabled on this machine: {0}", ex);
				}

				IEnumerable<String> lines = target.LinesDerived;
				Assert.AreEqual(0, lines.Count());
			}
		}
		[TestMethod()]
		[Description("Flush() method when there are no lines.")]
		public void EmailTraceListener_System_Flush_HasNoLines() {
			using (MockEmailTraceListener target = new MockEmailTraceListener(EmailTraceListenerTests.CurrentUserEmailAddress, "'Should Not Receive' Listener")) {
				try {
					// This line should NOT generate an e-mail.
					target.Flush();
				}
				catch (SmtpException ex) {
					Assert.Inconclusive("SMTP is not enabled on this machine: {0}", ex);
				}

				IEnumerable<String> lines = target.LinesDerived;
				Assert.AreEqual(0, lines.Count());
			}
		}

		[TestMethod()]
		[Description("SendMailMessage() method for the optimal path.")]
		public void EmailTraceListener_System_SendMailMessage_Optimal() {
			using (MockEmailTraceListener target = new MockEmailTraceListener(EmailTraceListenerTests.CurrentUserEmailAddress, "'Should Receive' Listener")) {
				String message = "This is a test.";
				target.WriteLine(message);

				Boolean actual = false;
				try {
					// This line should generate an e-mail.
					actual = target.SendMailMessageDerived();
				}
				catch (SmtpException ex) {
					Assert.Inconclusive("SMTP is not enabled on this machine: {0}", ex);
				}
				Assert.AreEqual(true, actual);
				Assert.AreEqual(0, target.LinesDerived.Count());
			}
		}
		[TestMethod()]
		[Description("SendMailMessage() method when there are no lines.")]
		public void EmailTraceListener_System_SendMailMessage_HasNoLines() {
			using (MockEmailTraceListener target = new MockEmailTraceListener(EmailTraceListenerTests.CurrentUserEmailAddress, "'Should Not Receive' Listener")) {
				// This line should NOT generate an e-mail.
				Boolean actual = target.SendMailMessageDerived();
				Assert.AreEqual(false, actual);
				Assert.AreEqual(0, target.LinesDerived.Count());
			}
		}

		[TestMethod()]
		[Description("Write(String) method for the optimal path.")]
		public void EmailTraceListener_Unit_Write_Optimal() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String message = "This is a test.";
				target.Write(message);

				IEnumerable<String> lines = target.LinesDerived;
				Assert.AreEqual(0, lines.Count());
			}
		}
		[TestMethod()]
		[Description("Write(String) method when the method is called twice.")]
		public void EmailTraceListener_Unit_Write_CalledTwice() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String message1 = "This is a test.";
				target.Write(message1);
				String message2 = "This is another test.";
				target.Write(message2);

				IEnumerable<String> lines = target.LinesDerived;
				Assert.AreEqual(0, lines.Count());
			}
		}

		[TestMethod()]
		[Description("WriteLine(String) method for the optimal path.")]
		public void EmailTraceListener_Unit_WriteLine_Optimal() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String message = "This is a test.";
				target.WriteLine(message);

				IEnumerable<String> lines = target.LinesDerived;
				Assert.AreEqual(1, lines.Count());
				Assert.AreEqual(message, lines.Single());
			}
		}
		[TestMethod()]
		[Description("WriteLine(String) method when the method is called twice.")]
		public void EmailTraceListener_Unit_WriteLine_CalledTwice() {
			using (MockEmailTraceListener target = new MockEmailTraceListener()) {
				String message1 = "This is a test.";
				target.WriteLine(message1);
				String message2 = "This is another test.";
				target.WriteLine(message2);

				IEnumerable<String> lines = target.LinesDerived;
				Assert.AreEqual(2, lines.Count());
				Assert.AreEqual(message1, lines.ElementAt(0));
				Assert.AreEqual(message2, lines.ElementAt(1));
			}
		}
	}
}