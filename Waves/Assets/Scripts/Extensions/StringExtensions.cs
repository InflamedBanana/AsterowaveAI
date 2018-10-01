using UnityEngine;
using System;

public static class StringExtensions
{
	/// <summary>
	/// Spaces the words separated by upper characters.
	/// </summary>
	public static string SpaceWords ( this string s )
	{
		string output = "";

		foreach ( char c in s )
		{
			if ( Char.IsUpper ( c ) )
				output += " ";
			
			output += c;
		}

		if ( output.Substring ( 0, 1 ) == " " )
			output = output.Substring ( 1, output.Length - 1 );

		return output;
	}
}