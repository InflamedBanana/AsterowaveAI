using System.Collections;
using UnityEngine;

public class Boiiiiing : MonoBehaviour
{
	[SerializeField] float duration;
	[SerializeField] AnimationCurve form;
	[SerializeField] float boingMinDelay;

	Coroutine boingRoutine;

	Vector3 baseScale;

	float lastBoingTime;

	void Start ()
	{
		baseScale = transform.localScale;
	}

	void OnTriggerEnter ( Collider other )
	{
		if ( other.GetComponent<ShockWave> () != null )
			LaunchBoing ();
	}

	void OnCollisionEnter ( Collision coll )
	{
		if ( coll.collider.GetComponent<Boiiiiing> () != null || coll.collider.GetComponent<BorderCollision> () != null )
			LaunchBoing ();
	}

	public void LaunchBoing ()
	{
		if ( Time.time - lastBoingTime < boingMinDelay )
			return;

		lastBoingTime = Time.time;

		if ( boingRoutine != null )
		{
			StopCoroutine ( boingRoutine );
			boingRoutine = null;
		}

		if ( boingRoutine == null )
			boingRoutine = StartCoroutine ( Boing () );
	}

	IEnumerator Boing ()
	{
		float precision = 0.02f;

		WaitForSeconds wait = new WaitForSeconds ( precision );

		float counter = 0;

		while ( counter < duration )
		{
			transform.localScale = baseScale * form.Evaluate ( counter / duration );

			yield return wait;

			counter += precision;
		}

		boingRoutine = null;
	}
}