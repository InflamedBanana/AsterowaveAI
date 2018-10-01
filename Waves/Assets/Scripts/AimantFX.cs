using System.Collections;
using UnityEngine;

public class AimantFX : MonoBehaviour
{
	Vector3 scale;

	[SerializeField] float reductionSpeed;
	[SerializeField] float offset;
	[SerializeField] MeshRenderer rend;
	float startTime;

	Coroutine fakeUpdateRoutine;

	bool quit;

	void Start ()
	{
		scale = transform.localScale;
		rend.material.SetFloat ( "_Refraction", 0 );
		rend.material.SetFloat ( "_Transparancy", 0 );
	}

	public void StartEffect ()
	{
		startTime = Time.time;

		if ( fakeUpdateRoutine != null )
		{
			StopCoroutine ( fakeUpdateRoutine );
			fakeUpdateRoutine = null;
		}
		if ( fakeUpdateRoutine == null )
		{
			quit = false;
			fakeUpdateRoutine = StartCoroutine ( FakeUpdate () );
		}
	}

	public void EndEffect ()
	{
		if ( fakeUpdateRoutine != null )
		{
			print("EndEffect");
			quit = true;
		}
	}

	IEnumerator FakeUpdate ()
	{
		transform.localScale = scale;

		rend.material.SetFloat ( "_Refraction", 0 );
		rend.material.SetFloat ( "_Transparancy", 0 );

		while ( Time.time - startTime <= offset )
			yield return null;

		while ( !quit )
		{
			yield return null;

			transform.localScale -= reductionSpeed.ToVector3 () * Time.deltaTime;

			rend.material.SetFloat ( "_Refraction", Mathf.Clamp ( rend.material.GetFloat ( "_Refraction" ) + reductionSpeed * Time.deltaTime * 10, 0, 100 ) );
			rend.material.SetFloat ( "_Transparancy", Mathf.Clamp01 ( rend.material.GetFloat ( "_Transparancy" ) + reductionSpeed * Time.deltaTime * 3 ) );

			if ( transform.localScale.x <= 0f )
			{
				transform.localScale = scale;

				rend.material.SetFloat ( "_Refraction", 0 );
				rend.material.SetFloat ( "_Transparancy", 0 );
			}

		}
		rend.material.SetFloat ( "_Refraction", 0 );
		rend.material.SetFloat ( "_Transparancy", 0 );
		fakeUpdateRoutine = null;
		print("QUItQUIQUIt");
	}
}