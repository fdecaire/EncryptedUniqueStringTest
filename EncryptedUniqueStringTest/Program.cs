using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptedUniqueStringTest
{
	class Program
	{
		static void Main(string[] args)
		{
			int min=int.MaxValue, max=int.MinValue, average;
			int total = 0;

			string prevToken = GetUniqueKey(40);
			for (int i = 0; i < 50000; i++)
			{
				string currentToken = GetUniqueKey(40);

				int difference = LevenshteinDistance.Compute(prevToken, currentToken);
				prevToken = currentToken;

				if (difference < min)
				{
					min = difference;
				}

				if (difference > max)
				{
					max = difference;
				}

				total += difference;
			}

			average = (int)(total / 50000.0);

			Console.WriteLine("min=" + min + " max=" + max + " avg=" + average);
			Console.ReadKey();
		}

		private static string GetUniqueKey(int length)
		{
			char[] AvailableCharacters = {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
              };

			char[] identifier = new char[length];
			byte[] randomData = new byte[length];

			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(randomData);
			}

			for (int idx = 0; idx < identifier.Length; idx++)
			{
				int pos = randomData[idx] % AvailableCharacters.Length;
				identifier[idx] = AvailableCharacters[pos];
			}

			return new string(identifier);
		}
	}


	/// <summary>
	/// Contains approximate string matching
	/// </summary>
	static class LevenshteinDistance
	{
		/// <summary>
		/// Compute the distance between two strings.
		/// </summary>
		public static int Compute(string s, string t)
		{
			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];

			// Step 1
			if (n == 0)
			{
				return m;
			}

			if (m == 0)
			{
				return n;
			}

			// Step 2
			for (int i = 0; i <= n; d[i, 0] = i++)
			{
			}

			for (int j = 0; j <= m; d[0, j] = j++)
			{
			}

			// Step 3
			for (int i = 1; i <= n; i++)
			{
				//Step 4
				for (int j = 1; j <= m; j++)
				{
					// Step 5
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

					// Step 6
					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			// Step 7
			return d[n, m];
		}
	}
}
