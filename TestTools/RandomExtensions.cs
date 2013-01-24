using System;

namespace Vizistata.TestTools {
	/// <summary>
	/// Provides extension methods for the <see cref="T:Random"/> class.  This class may not be inherited.
	/// </summary>
	public static class RandomExtensions {
	// Fields
		/// <summary>
		/// The alphanumeric characters.
		/// </summary>
		private static readonly Char[] _alphanumericCharacters = new Char[] {
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z'
		};

	// Methods
		/// <summary>
		/// Returns the next random string.
		/// </summary>
		/// <param name="random">The random object used to create the value.</param>
		/// <returns>The random string that was generated.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="random"/> is a null reference.</exception>
		public static String NextString(this Random random) {
			if (random == null) {
				throw new ArgumentNullException("random");
			}

			Int32 length = random.Next(1, 100);
			Char[] characters = new Char[length];
			for (Int32 i = 0; i < length; i++) {
				characters[i] = random.NextAlphanumericCharacter();
			}
			String value = new String(characters);
			return value;
		}
		/// <summary>
		/// Returns the next random alpha-numeric character.
		/// </summary>
		/// <param name="random">The random object used to create the value.</param>
		/// <returns>The random alpha-numeric character that was generated.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="random"/> is a null reference.</exception>
		public static Char NextAlphanumericCharacter(this Random random) {
			if (random == null) {
				throw new ArgumentNullException("random");
			}

			Int32 index = random.Next(0, RandomExtensions._alphanumericCharacters.Length);
			Char character = RandomExtensions._alphanumericCharacters[index];
			return character;
		}
	}
}
