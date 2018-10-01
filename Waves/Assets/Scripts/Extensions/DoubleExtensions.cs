using UnityEngine;

public static class DoubleExtensions
{
	/// <summary>
	/// Force the value to be higher than 0
	/// </summary>
	public static double ZeroIfLess ( this double d )
	{
		if ( d < 0 )
			d = 0;

		return d;
	}

	/// <summary>
	/// Force the value to be lesser than 0
	/// </summary>
	public static double ZeroIfMore ( this double i )
	{
		if ( i > 0 )
			i = 0;

		return i;
	}

	/// <summary>
	/// Convert the given double to an int value
	/// </summary>
	public static double ToInt ( this double d )
	{
		return (int) d;
	}

	/// <summary>
	/// Convert the given double to a float value
	/// </summary>
	public static double ToFloat ( this double d )
	{
		return (float) d;
	}

	/// <summary>
	/// Return a new Vector2 with x = i and y = i
	/// </summary>
	public static Vector2 ToVector2 ( this double i )
	{
		return new Vector2 ( (float) i, (float) i );
	}

	/// <summary>
	/// Return a new Vector3 with x = i, y = i and z =i
	/// </summary>
	public static Vector3 ToVector3 ( this double i )
	{
		return new Vector3 ( (float) i, (float) i, (float) i );
	}

	/// <summary>
	/// Return a new Vector4 with x = i, y = i, z = i and w = i
	/// </summary>
	public static Vector4 ToVector4 ( this double i )
	{
		return new Vector4 ( (float) i, (float) i, (float) i, (float) i );
	}
}