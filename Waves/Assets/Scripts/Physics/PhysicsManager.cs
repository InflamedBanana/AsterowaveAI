using System.Collections;
using UnityEngine;

public class PhysicsManager : MonoBehaviourSingleton <PhysicsManager>
{
	[SerializeField] Vector3 gravity;
	[SerializeField] float physicsCadence;

	#region Getter / Setter

	public Vector3 Gravity
	{
		get
		{
			return this.gravity;
		}
	}

	public float PhysicsCadence
	{
		get
		{
			return this.physicsCadence;
		}
	}

	#endregion
}