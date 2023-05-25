<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
  <RuntimeVersion>7.0</RuntimeVersion>
</Query>

public class Program
{
	private static readonly char[] lowerCaseLetters = "abcdefghijkmnpqrstuvwxyz".ToCharArray();
	private static readonly char[] upperCaseLetters = "ABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();
	private static readonly char[] digits = "123456790".ToCharArray();
	private static readonly char[] specialCharacters = "!@#$%^&*()".ToCharArray();
	private static readonly char[][] characterArrays = new[] { lowerCaseLetters, upperCaseLetters, digits, specialCharacters };

	public static void Main()
	{
		string password = GenerateRandomPassword(10);
		System.Windows.Forms.Clipboard.SetText(password);
		Console.WriteLine("Password generated and copied to clipboard.");
	}

	public static string GenerateRandomPassword(int length)
	{
		if (length < 4)
			throw new ArgumentException("Password length must be at least 4 to ensure it contains a lower case, upper case, digit and special character.");

		char[] password = new char[length];
		byte[] buffer = new byte[length];

		using (var rng = RandomNumberGenerator.Create()) {
			rng.GetBytes(buffer);

			for (int i = 0; i < 4; i++)
				password[i] = characterArrays[i][buffer[i] % characterArrays[i].Length];

			for (int i = 4; i < length; i++) {
				char[] selectedArray = characterArrays[buffer[i] % characterArrays.Length];
				password[i] = selectedArray[buffer[i] % selectedArray.Length];
			}
		}

		return new string(password);
	}
}
