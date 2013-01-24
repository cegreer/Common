using System;
using System.Security.Principal;

namespace Vizistata {
	/// <summary>
	/// Defines extensions methods for the types in the <see cref="N:System.Security.Principal"/> namespace.
	/// </summary>
	public static class PrincipalExtensions {
	// Methods
		/// <summary>
		/// Gets a value indicating if the user is in all roles specified.
		/// </summary>
		/// <param name="principal">The <see cref="T:IPrincipal"/> from which this method is being called.</param>
		/// <param name="roles">The roles to check.</param>
		/// <returns><c>true</c> if the user is in each role specified; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="principal"/> is a null reference.</exception>
		public static Boolean IsInAllRoles(this IPrincipal principal, params String[] roles) {
			if (principal == null) {
				throw new ArgumentNullException("principal");
			}

			if (roles != null) {
				foreach (String role in roles) {
					if (!principal.IsInRole(role)) {
						return false;
					}
				}
			}
			return true;
		}
		/// <summary>
		/// Gets a value indicating if the user is in any of the roles specified.
		/// </summary>
		/// <param name="principal">The <see cref="T:IPrincipal"/> from which this method is being called.</param>
		/// <param name="roles">The roles to check.</param>
		/// <returns><c>true</c> if the user is in any of the roles specified; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="principal"/> is a null reference.
		/// -or- <paramref name="roles"/> is a null reference.</exception>
		public static Boolean IsInAnyRole(this IPrincipal principal, params String[] roles) {
			if (principal == null) {
				throw new ArgumentNullException("principal");
			}

			if (roles != null) {
				foreach (String role in roles) {
					if (principal.IsInRole(role)) {
						return true;
					}
				}
			}
			return false;
		}
	}
}
