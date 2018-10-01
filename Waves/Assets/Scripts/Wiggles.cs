using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggles : MonoBehaviourSingleton<Wiggles>
{
	public void ScaleWiggle(Transform toWiggle, float intensity, float time)
	{
		StartCoroutine( WiggleScale(toWiggle, intensity, time) );
	}

	IEnumerator WiggleScale(Transform toWiggle, float intensity, float time)
	{
		float tempTime = Time.time;
		Vector3 targetScale = toWiggle.localScale * intensity;

		while(Time.time - tempTime < time)
		{
			if(toWiggle != null)
				toWiggle.localScale = Vector3.MoveTowards( toWiggle.localScale,targetScale, Time.deltaTime * 10 );
			else
				break;

			yield return null;
		}
	}
}
