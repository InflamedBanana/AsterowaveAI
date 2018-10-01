using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalExplosion : MonoBehaviour 
{
	public void EnableExplosion()
	{
		foreach( Rigidbody r in GetComponentsInChildren<Rigidbody>())
			r.isKinematic = false;
		
		foreach(BoxCollider coll in GetComponentsInChildren<BoxCollider>())
			coll.enabled = true;
	}
}
