using UnityEngine;

public static class IntExtensions
{
	/// <summary>
	/// Force the value to be higher than 0
	/// </summary>
	public static int ZeroIfLess ( this int i )
	{
		if ( i < 0 )
			i = 0;

		return i;
	}

	/// <summary>
	/// Force the value to be lesser than 0
	/// </summary>
	public static int ZeroIfMore ( this int i )
	{
		if ( i > 0 )
			i = 0;

		return i;
	}

	/// <summary>
	/// Return a new Vector2 with x = i and y = i
	/// </summary>
	public static Vector2 ToVector2 ( this int i )
	{
		return new Vector2 ( i, i );
	}

	/// <summary>
	/// Return a new Vector3 with x = i, y = i and z =i
	/// </summary>
	public static Vector3 ToVector3 ( this int i )
	{
		return new Vector3 ( i, i, i );
	}

	/// <summary>
	/// Return a new Vector4 with x = i, y = i, z = i and w = i
	/// </summary>
	public static Vector4 ToVector4 ( this int i )
	{
		return new Vector4 ( i, i, i, i );
	}
}