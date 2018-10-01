using UnityEngine;

public static class VectorExtensions
{
	/// <summary>
	/// Set the x coordonate of the vector
	/// </summary>
	public static Vector4 WithX ( this Vector4 v, float xValue )
	{
		return new Vector4 ( xValue, v.y, v.z, v.w );
	}

	/// <summary>
	/// Set the x coordonate of the vector
	/// </summary>
	public static Vector3 WithX ( this Vector3 v, float xValue )
	{
		return new Vector3 ( xValue, v.y, v.z );
	}

	/// <summary>
	/// Set the x coordonate of the vector
	/// </summary>
	public static Vector2 WithX ( this Vector2 v, float xValue )
	{
		return new Vector2 ( xValue, v.y );
	}

	/// <summary>
	/// Set the y coordonate of the vector
	/// </summary>
	public static Vector4 WithY ( this Vector4 v, float yValue )
	{
		return new Vector4 ( v.x, yValue, v.z, v.w );
	}

	/// <summary>
	/// Set the y coordonate of the vector
	/// </summary>
	public static Vector3 WithY ( this Vector3 v, float yValue )
	{
		return new Vector3 ( v.x, yValue, v.z );
	}

	/// <summary>
	/// Set the y coordonate of the vector
	/// </summary>
	public static Vector2 WithY ( this Vector2 v, float yValue )
	{
		return new Vector2 ( v.x, yValue );
	}

	/// <summary>
	/// Set the z coordonate of the vector
	/// </summary>
	public static Vector4 WithZ ( this Vector4 v, float zValue )
	{
		return new Vector4 ( v.x, v.y, zValue, v.w );
	}

	/// <summary>
	/// Set the z coordonate of the vector
	/// </summary>
	public static Vector3 WithZ ( this Vector3 v, float zValue )
	{
		return new Vector3 ( v.x, v.y, zValue );
	}

	/// <summary>
	/// Set the w coordonate of the vector
	/// </summary>
	public static Vector4 WithW ( this Vector4 v, float wValue )
	{
		return new Vector4 ( v.x, v.y, v.z, wValue );
	}

	/// <summary>
	/// Return a Vector3 where x = x, y = y and z = z
	/// </summary>
	public static Vector3 XYZ ( this Vector4 v )
	{
		return new Vector3 ( v.x, v.y, v.z );
	}

	/// <summary>
	/// Return a Vector3 where x = y, y = z and z = w
	/// </summary>
	public static Vector3 YZW ( this Vector4 v )
	{
		return new Vector3 ( v.y, v.z, v.w );
	}

	/// <summary>
	/// Return a Vector3 where x = x, y = y and z = w
	/// </summary>
	public static Vector3 XYW ( this Vector4 v )
	{
		return new Vector3 ( v.x, v.y, v.w );
	}

	/// <summary>
	/// Return a Vector3 where x = x, y = z and z = w
	/// </summary>
	public static Vector3 XZW ( this Vector4 v )
	{
		return new Vector3 ( v.x, v.z, v.w );
	}

	/// <summary>
	/// Return a Vector2 where x = x and y = y
	/// </summary>
	public static Vector2 XY ( this Vector4 v )
	{
		return new Vector2 ( v.x, v.y );
	}

	/// <summary>
	/// Return a Vector2 where x = x and y = y
	/// </summary>
	public static Vector2 XY ( this Vector3 v )
	{
		return new Vector2 ( v.x, v.y );
	}

	/// <summary>
	/// Return a Vector2 where x = y and y = z
	/// </summary>
	public static Vector2 YZ ( this Vector4 v )
	{
		return new Vector2 ( v.y, v.z );
	}

	/// <summary>
	/// Return a Vector2 where x = y and y = z
	/// </summary>
	public static Vector2 YZ ( this Vector3 v )
	{
		return new Vector2 ( v.y, v.z );
	}

	/// <summary>
	/// Return a Vector2 where x = x and y = z
	/// </summary>
	public static Vector2 XZ ( this Vector4 v )
	{
		return new Vector2 ( v.x, v.z );
	}

	/// <summary>
	/// Return a Vector2 where x = x and y = z
	/// </summary>
	public static Vector2 XZ ( this Vector3 v )
	{
		return new Vector2 ( v.x, v.z );
	}

	/// <summary>
	/// Return a Vector2 where x = x and y = w
	/// </summary>
	public static Vector2 XW ( this Vector4 v )
	{
		return new Vector2 ( v.x, v.w );
	}

	/// <summary>
	/// Return a Vector2 where x = y and y = w
	/// </summary>
	public static Vector2 YW ( this Vector4 v )
	{
		return new Vector2 ( v.y, v.w );
	}

	/// <summary>
	/// Return a Vector2 where x = z and y = w
	/// </summary>
	public static Vector2 ZW ( this Vector4 v )
	{
		return new Vector2 ( v.z, v.w );
	}

	/// <summary>
	/// Clamp all the coordinates of the vector between min and max
	/// </summary>
	public static Vector4 ClampAll ( this Vector4 v, float min, float max )
	{
		return new Vector4 ( Mathf.Clamp ( v.x, min, max ), Mathf.Clamp ( v.y, min, max ), Mathf.Clamp ( v.z, min, max ), Mathf.Clamp ( v.w, min, max ) );
	}

	/// <summary>
	/// Clamp all the coordinates of the vector between min and max
	/// </summary>
	public static Vector3 ClampAll ( this Vector3 v, float min, float max )
	{
		return new Vector3 ( Mathf.Clamp ( v.x, min, max ), Mathf.Clamp ( v.y, min, max ), Mathf.Clamp ( v.z, min, max ) );
	}

	/// <summary>
	/// Clamp all the coordinates of the vector between min and max
	/// </summary>
	public static Vector2 ClampAll ( this Vector2 v, float min, float max )
	{
		return new Vector2 ( Mathf.Clamp ( v.x, min, max ), Mathf.Clamp ( v.y, min, max ) );
	}

	/// <summary>
	/// Clamp the x coordinates of the vector between min and max
	/// </summary>
	public static Vector4 ClampX ( this Vector4 v, float min, float max )
	{
		return new Vector4 ( Mathf.Clamp ( v.x, min, max ), v.y, v.z, v.w );
	}

	/// <summary>
	/// Clamp the x coordinates of the vector between min and max
	/// </summary>
	public static Vector3 ClampX ( this Vector3 v, float min, float max )
	{
		return new Vector3 ( Mathf.Clamp ( v.x, min, max ), v.y, v.z );
	}

	/// <summary>
	/// Clamp the x coordinates of the vector between min and max
	/// </summary>
	public static Vector2 ClampX ( this Vector2 v, float min, float max )
	{
		return new Vector2 ( Mathf.Clamp ( v.x, min, max ), v.y );
	}

	/// <summary>
	/// Clamp the y coordinates of the vector between min and max
	/// </summary>
	public static Vector4 ClampY ( this Vector4 v, float min, float max )
	{
		return new Vector4 ( v.x, Mathf.Clamp ( v.y, min, max ), v.z, v.w );
	}

	/// <summary>
	/// Clamp the y coordinates of the vector between min and max
	/// </summary>
	public static Vector3 ClampY ( this Vector3 v, float min, float max )
	{
		return new Vector3 ( v.x, Mathf.Clamp ( v.y, min, max ), v.z );
	}

	/// <summary>
	/// Clamp the y coordinates of the vector between min and max
	/// </summary>
	public static Vector2 ClampY ( this Vector2 v, float min, float max )
	{
		return new Vector2 ( v.x, Mathf.Clamp ( v.y, min, max ) );
	}

	/// <summary>
	/// Clamp the z coordinates of the vector between min and max
	/// </summary>
	public static Vector4 ClampZ ( this Vector4 v, float min, float max )
	{
		return new Vector4 ( v.x, v.y, Mathf.Clamp ( v.z, min, max ), v.w );
	}

	/// <summary>
	/// Clamp the z coordinates of the vector between min and max
	/// </summary>
	public static Vector3 ClampZ ( this Vector3 v, float min, float max )
	{
		return new Vector3 ( v.x, v.y, Mathf.Clamp ( v.z, min, max ) );
	}

	/// <summary>
	/// Clamp the w coordinates of the vector between min and max
	/// </summary>
	public static Vector4 ClampW ( this Vector4 v, float min, float max )
	{
		return new Vector4 ( v.x, v.y, v.z, Mathf.Clamp ( v.w, min, max ) );
	}
}