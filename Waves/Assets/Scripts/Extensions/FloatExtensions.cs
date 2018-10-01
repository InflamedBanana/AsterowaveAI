using UnityEngine;

public static class FloatExtensions
{
	/// <summary>
	/// Force the value to be higher than 0
	/// </summary>
	public static float ZeroIfLess ( this float f )
	{
		if ( f < 0 )
			f = 0;

		return f;
	}

	/// <summary>
	/// Force the value to be lesser than 0
	/// </summary>
	public static float ZeroIfMore ( this float i )
	{
		if ( i > 0 )
			i = 0;

		return i;
	}

	/// <summary>
	/// Convert the given float to an int value
	/// </summary>
	public static float ToInt ( this float f )
	{
		return (int) f;
	}

	/// <summary>
	/// Return a new Vector2 with x = i and y = i
	/// </summary>
	public static Vector2 ToVector2 ( this float i )
	{
		return new Vector2 ( i, i );
	}

	/// <summary>
	/// Return a new Vector3 with x = i, y = i and z =i
	/// </summary>
	public static Vector3 ToVector3 ( this float i )
	{
		return new Vector3 ( i, i, i );
	}

	/// <summary>
	/// Return a new Vector4 with x = i, y = i, z = i and w = i
	/// </summary>
	public static Vector4 ToVector4 ( this float i )
	{
		return new Vector4 ( i, i, i, i );
	}
}