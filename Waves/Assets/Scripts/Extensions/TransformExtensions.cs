using UnityEngine;

public static class TransformExtensions
{
	/// <summary>
	/// Sets X position of the transform
	/// </summary>
	public static void SetPositionX ( this Transform t, float xValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.position = new Vector3 ( xValue, t.position.y, t.position.z );
				break;
			case Space.Self:
				t.localPosition = new Vector3 ( xValue, t.localPosition.y, t.localPosition.z );
				break;
		}
	}

	/// <summary>
	/// Incr X position of the transform by xValue
	/// </summary>
	public static void IncrPositionX ( this Transform t, float xValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.position += new Vector3 ( xValue, 0f, 0f );
				break;
			case Space.Self:
				t.localPosition += new Vector3 ( xValue, 0f, 0f );
				break;
		}
	}

	/// <summary>
	/// Sets Y position of the transform
	/// </summary>
	public static void SetPositionY ( this Transform t, float yValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.position = new Vector3 ( t.position.x, yValue, t.position.z );
				break;
			case Space.Self:
				t.localPosition = new Vector3 ( t.localPosition.x, yValue, t.localPosition.z );
				break;
		}
	}

	/// <summary>
	/// Incr Y position of the transform by yValue
	/// </summary>
	public static void IncrPositionY ( this Transform t, float yValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.position += new Vector3 ( 0f, yValue, 0f );
				break;
			case Space.Self:
				t.localPosition += new Vector3 ( 0f, yValue, 0f );
				break;
		}
	}

	/// <summary>
	/// Sets Z position of the transform
	/// </summary>
	public static void SetPositionZ ( this Transform t, float zValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.position = new Vector3 ( t.position.x, t.position.y, zValue );
				break;
			case Space.Self:
				t.localPosition = new Vector3 ( t.localPosition.x, t.localPosition.y, zValue );
				break;
		}
	}

	/// <summary>
	/// Incr Z position of the transform by zValue
	/// </summary>
	public static void IncrPositionZ ( this Transform t, float zValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.position += new Vector3 ( 0f, 0f, zValue );
				break;
			case Space.Self:
				t.localPosition += new Vector3 ( 0f, 0f, zValue );
				break;
		}
	}

	/// <summary>
	/// Sets X rotation (in eulerAngles) of the transform
	/// </summary>
	public static void SetEulerX ( this Transform t, float xValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.eulerAngles = new Vector3 ( xValue, t.eulerAngles.y, t.eulerAngles.z );
				break;
			case Space.Self:
				t.localEulerAngles = new Vector3 ( xValue, t.localEulerAngles.y, t.localEulerAngles.z );
				break;
		}
	}

	/// <summary>
	/// Sets Y rotation (in eulerAngles) of the transform
	/// </summary>
	public static void SetEulerY ( this Transform t, float yValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.eulerAngles = new Vector3 ( t.eulerAngles.x, yValue, t.eulerAngles.z );
				break;
			case Space.Self:
				t.localEulerAngles = new Vector3 ( t.localEulerAngles.x, yValue, t.localEulerAngles.z );
				break;
		}
	}

	/// <summary>
	/// Sets Z rotation (in eulerAngles) of the transform
	/// </summary>
	public static void SetEulerZ ( this Transform t, float zValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.eulerAngles = new Vector3 ( t.eulerAngles.x, t.eulerAngles.y, zValue );
				break;
			case Space.Self:
				t.localEulerAngles = new Vector3 ( t.localEulerAngles.x, t.localEulerAngles.y, zValue );
				break;
		}
	}

	/// <summary>
	/// Sets X value of the quaternion rotation of the transform
	/// </summary>
	public static void SetRotationX ( this Transform t, float xValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.rotation = new Quaternion ( xValue, t.rotation.y, t.rotation.z, t.rotation.w );
				break;
			case Space.Self:
				t.localRotation = new Quaternion ( xValue, t.localRotation.y, t.localRotation.z, t.localRotation.w );
				break;
		}
	}

	/// <summary>
	/// Sets Y value of the quaternion rotation of the transform
	/// </summary>
	public static void SetRotationY ( this Transform t, float yValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.rotation = new Quaternion ( t.rotation.x, yValue, t.rotation.z, t.rotation.w );
				break;
			case Space.Self:
				t.localRotation = new Quaternion ( t.localRotation.x, yValue, t.localRotation.z, t.localRotation.w );
				break;
		}
	}

	/// <summary>
	/// Sets Z value of the quaternion rotation of the transform
	/// </summary>
	public static void SetRotationZ ( this Transform t, float zValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.rotation = new Quaternion ( t.rotation.x, t.rotation.y, zValue, t.rotation.w );
				break;
			case Space.Self:
				t.localRotation = new Quaternion ( t.localRotation.x, t.localRotation.y, zValue, t.localRotation.w );
				break;
		}
	}

	/// <summary>
	/// Sets W value of the quaternion rotation of the transform
	/// </summary>
	public static void SetRotationW ( this Transform t, float wValue, Space referentiel = Space.Self )
	{
		switch ( referentiel )
		{
			case Space.World:
				t.rotation = new Quaternion ( t.rotation.x, t.rotation.y, t.rotation.z, wValue );
				break;
			case Space.Self:
				t.localRotation = new Quaternion ( t.localRotation.x, t.localRotation.y, t.localRotation.z, wValue );
				break;
		}
	}

	/// <summary>
	/// Sets X value of the scale of the transform
	/// </summary>
	public static void SetScaleX ( this Transform t, float xValue )
	{
		t.localScale = new Vector3 ( xValue, t.localScale.y, t.localScale.z );
	}

	/// <summary>
	/// Sets Y value of the scale of the transform
	/// </summary>
	public static void SetScaleY ( this Transform t, float yValue )
	{
		t.localScale = new Vector3 ( t.localScale.x, yValue, t.localScale.z );
	}

	/// <summary>
	/// Sets Z value of the scale of the transform
	/// </summary>
	public static void SetScaleZ ( this Transform t, float zValue )
	{
		t.localScale = new Vector3 ( t.localScale.x, t.localScale.y, zValue );
	}

	/// <summary>
	/// Gets the total number of children of this transform in the hierarchy.
	/// </summary>
	public static int GetTotalNumberChildren ( this Transform t )
	{
		int nb = 0;

		for ( int i = 0; i < t.childCount; i++ )
		{
			nb++;
			nb += GetTotalNumberChildren ( t.GetChild ( i ) );
		}

		return nb;
	}

	/// <summary>
	/// Gets the number parents.
	/// </summary>
	static int GetNumberParents ( GameObject g )
	{
		Transform newTrans = g.transform;
		int i = 0;
		while ( newTrans.parent != null )
		{
			newTrans = newTrans.parent;
			i++;
		}
		return i++;
	}

	/// <summary>
	/// Gets the parent of the object at a certain index
	/// </summary>
	public static Transform GetParent ( this Transform t, int indiceFromObject )
	{
		if ( indiceFromObject > GetNumberParents ( t.gameObject ) )
			return null;

		Transform parent = t;

		for ( int i = 0; i < indiceFromObject; i++ )
			parent = parent.parent;

		return parent;
	}
}