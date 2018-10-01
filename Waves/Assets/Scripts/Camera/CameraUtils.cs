using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraUtils : MonoBehaviourSingleton<CameraUtils>
{
	Coroutine shakeCo;
	VignetteAndChromaticAberration chroma;
	float baseChroma;

	void Start()
	{
		chroma = GetComponent<VignetteAndChromaticAberration>();
		baseChroma = chroma.chromaticAberration;
	}

	public void Shake ( float intensity, float time )
	{
		StartCoroutine ( ShakeCo ( intensity, time ) );
	}

	IEnumerator ShakeCo ( float intensity, float time )
	{
		float tempTime = Time.time;
		Vector2 offset = Vector2.zero;

		while ( Time.time - tempTime < time )
		{
			offset.x = Random.Range ( -1.0f, 1.0f ) * intensity;
			offset.y = Random.Range ( -1.0f, 1.0f ) * intensity;
			chroma.chromaticAberration = Random.Range(2.0f,4.0f)* ( intensity + 1);

			transform.Translate ( offset );
			yield return null;
			transform.position = transform.position.WithX ( 0.0f );
			transform.position = transform.position.WithY ( 0.0f );
			chroma.chromaticAberration = baseChroma;
		}
	}
}
