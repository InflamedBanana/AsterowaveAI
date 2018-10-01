#if UNITY_EDITOR

using UnityEngine;

[ExecuteInEditMode]
public class LockedObject : MonoBehaviour
{
	#region Properties
	Vector3 pos;
	Quaternion rot;
	Vector3 scl;
	#endregion

	#region Functions
	void Update ()
	{
		if ( UnityEditor.EditorApplication.isPlaying )
			return;
		
		transform.position = Pos;
		transform.rotation = Rot;
		transform.localScale = Scl;
	}
	#endregion

	#region Getter / Setter
	public Vector3 Pos
	{
		get
		{
			return this.pos;
		}
		set
		{
			pos = value;
		}
	}

	public Quaternion Rot
	{
		get
		{
			return this.rot;
		}
		set
		{
			rot = value;
		}
	}

	public Vector3 Scl
	{
		get
		{
			return this.scl;
		}
		set
		{
			scl = value;
		}
	}
	#endregion
}
#endif