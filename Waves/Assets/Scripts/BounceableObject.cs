using System;
using System.Collections.Generic;
using UnityEngine;

public class BounceableObject : MonoBehaviour
{
	[SerializeField] float colliderDelay;
	[SerializeField] float bounceMultiplier = 3;

	List <ShockWave> shockWavesAlreadyHurt = new List <ShockWave> ();

	Action<GameObject> onDestroy;

	Rigidbody body;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();
		Invoke ( "ActivateCollider", colliderDelay );
	}

	void ActivateCollider ()
	{
		GetComponent<Collider> ().enabled = true;
	}

	void OnTriggerEnter ( Collider other )
	{
		if ( other.GetComponent <ShockWave> () == null || shockWavesAlreadyHurt.Contains ( other.GetComponent <ShockWave> () ) )
			return;

		ShockWave shockWave = other.GetComponent <ShockWave> ();

		Vector3 force = transform.position - other.transform.position;

		force *= bounceMultiplier * ( 1 - ( ( other.transform.localScale.x / 2 ) / shockWave.MaxRay ) );

		body.velocity = Vector3.zero;

		body.AddForce ( force * 5, ForceMode.Impulse );

		shockWavesAlreadyHurt.Add ( shockWave );
	}

	public void RemoveObject()
	{
		if( onDestroy != null )
			onDestroy( gameObject );

		Destroy( gameObject );
	}

	//void OnDestroy ()
	//{
	//	if ( onDestroy != null )
	//		onDestroy ( gameObject );
	//}

	public Action<GameObject> OnDestroyAct
	{
		get
		{
			return this.onDestroy;
		}
		set
		{
			onDestroy = value;
		}
	}
}
