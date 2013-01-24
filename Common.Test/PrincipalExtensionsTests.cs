using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vizistata {
	/// <summary>
	/// This is a test class for <see cref="T:PrincipalExtensions"/> and is intended to contain all <see cref="T:PrincipalExtensions"/> Unit Tests.
	///</summary>
	[TestClass()]
	public class PrincipalExtensionsTests {
		#region Test Class Implementation
		/// <summary>
		/// Describes the context under which the current test is running.
		/// </summary>
		private TestContext _testContextInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PrincipalExtensionsTests"/> class.
		/// </summary>
		public PrincipalExtensionsTests() : base() { }

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

	// Method Tests
		[TestMethod()]
		[Description("IsInAllRoles(IPrincipal, String[]) method when 'roles' contains all necessary roles.")]
		public void PrincipalExtensions_Unit_IsInAllRoles_RolesContainsAllNecessaryRoles() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = principalRoles.Take(2).ToArray();

			Boolean actual = PrincipalExtensions.IsInAllRoles(principal, roles);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsInAllRoles(IPrincipal, String[]) method when 'roles' contains no necessary roles.")]
		public void PrincipalExtensions_Unit_IsInAllRoles_RolesContainsNoNecessaryRoles() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = new String[] { "Designer" };

			Boolean actual = PrincipalExtensions.IsInAllRoles(principal, roles);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsInAllRoles(IPrincipal, String[]) method when 'principal' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PrincipalExtensions_Unit_IsInAllRoles_PrincipalIsNull() {
			IPrincipal principal = null;
			String[] roles = new String[] { "Designer" };

			PrincipalExtensions.IsInAllRoles(principal, roles);
		}
		[TestMethod()]
		[Description("IsInAllRoles(IPrincipal, String[]) method when 'roles' is a null reference.")]
		public void PrincipalExtensions_Unit_IsInAllRoles_RolesIsNull() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = null;

			Boolean actual = PrincipalExtensions.IsInAllRoles(principal, roles);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsInAllRoles(IPrincipal, String[]) method when 'roles' is empty.")]
		public void PrincipalExtensions_Unit_IsInAllRoles_RolesIsEmpty() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = new String[0];

			Boolean actual = PrincipalExtensions.IsInAllRoles(principal, roles);
			Assert.AreEqual(true, actual);
		}

		[TestMethod()]
		[Description("IsInAnyRole(IPrincipal, String[]) method when 'roles' contains all necessary roles.")]
		public void PrincipalExtensions_Unit_IsInAnyRole_RolesContainsAnyNecessaryRoles() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = principalRoles.Take(2).Union(new String[] { "Designer" }).ToArray();

			Boolean actual = PrincipalExtensions.IsInAnyRole(principal, roles);
			Assert.AreEqual(true, actual);
		}
		[TestMethod()]
		[Description("IsInAnyRole(IPrincipal, String[]) method when 'roles' contains no necessary roles.")]
		public void PrincipalExtensions_Unit_IsInAnyRole_RolesContainsNoNecessaryRoles() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = new String[] { "Designer" };

			Boolean actual = PrincipalExtensions.IsInAnyRole(principal, roles);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsInAnyRole(IPrincipal, String[]) method when 'principal' is a null reference.")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PrincipalExtensions_Unit_IsInAnyRole_PrincipalIsNull() {
			IPrincipal principal = null;
			String[] roles = new String[] { "Designer" };

			PrincipalExtensions.IsInAnyRole(principal, roles);
		}
		[TestMethod()]
		[Description("IsInAnyRole(IPrincipal, String[]) method when 'roles' is a null reference.")]
		public void PrincipalExtensions_Unit_IsInAnyRole_RolesIsNull() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = null;

			Boolean actual = PrincipalExtensions.IsInAnyRole(principal, roles);
			Assert.AreEqual(false, actual);
		}
		[TestMethod()]
		[Description("IsInAnyRole(IPrincipal, String[]) method when 'roles' is empty.")]
		public void PrincipalExtensions_Unit_IsInAnyRole_RolesIsEmpty() {
			String name = "Chad.Greer";
			IIdentity identity = new GenericIdentity(name);
			String[] principalRoles = new String[] { "Admin", "Contributor", "Member" };
			IPrincipal principal = new GenericPrincipal(identity, principalRoles);
			String[] roles = new String[0];

			Boolean actual = PrincipalExtensions.IsInAnyRole(principal, roles);
			Assert.AreEqual(false, actual);
		}
	}
}