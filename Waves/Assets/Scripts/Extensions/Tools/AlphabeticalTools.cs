using UnityEngine;
using System.Collections;

public static class AlphabeticalTools
{
	public static string ConvertNumberToAlphabet (int number)
	{
		string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		int mod = number % 26;

		string answer = alphabet.Substring (mod, 1);

		int div = number / 26;
		while (div > 26)
		{
			answer = answer.Insert (0, alphabet.Substring (div, 1));
			div /= 26;
		}

		if (div != 0)
			answer = answer.Insert (0, alphabet.Substring (div, 1));

		return answer;
	}
}