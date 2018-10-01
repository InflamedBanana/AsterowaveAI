using System.Collections;
using UnityEngine;

public enum AddForceMode
{
	Impulse,
	Acceleration
}

public class CustomPhysics : MonoBehaviour
{
	// Serializables
	[SerializeField] bool useGravity = true;
	[Range ( 0.0f, 1.0f )]
	[SerializeField] float drag = 0.1f;

	// Parameters
	float physicsCadence;
	Vector3 gravity;
	Vector3 velocity;

	// Routines
	Coroutine fakeUpdateRoutine;

	void Start ()
	{
		gravity = PhysicsManager.Instance.Gravity;
		physicsCadence = PhysicsManager.Instance.PhysicsCadence;
		EnablePhysics ( true );
	}

	IEnumerator FakeUpdate ()
	{
		WaitForSeconds wait = new WaitForSeconds ( physicsCadence );

		while ( true )
		{
			if ( useGravity )
				velocity += gravity;

			velocity *= ( 1 - drag );

			transform.Translate ( velocity );

			yield return wait;
		}
	}

	public void EnablePhysics ( bool enableState )
	{
		if ( enableState && fakeUpdateRoutine == null )
			fakeUpdateRoutine = StartCoroutine ( FakeUpdate () );
		else if ( !enableState && fakeUpdateRoutine != null )
		{
			StopCoroutine ( fakeUpdateRoutine );
			fakeUpdateRoutine = null;
		}
	}

	public void AddForce ( Vector3 force, AddForceMode addForceMode = AddForceMode.Impulse )
	{
		switch ( addForceMode )
		{
			case (AddForceMode.Impulse):
				velocity += force;
				break;
			case (AddForceMode.Acceleration):
				velocity += force * physicsCadence;
				break;
		}
	}

	#region Getter / Setter

	public Vector3 Velocity
	{
		get
		{
			return this.velocity;
		}
		set
		{
			velocity = value;
		}
	}

	


	#endregion
}