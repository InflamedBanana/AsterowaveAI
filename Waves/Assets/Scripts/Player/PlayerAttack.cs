using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] GameObject shockWavePrefab;
	[SerializeField] float maxLoadValue;
	[SerializeField] SphereCollider attractCollider;
	[SerializeField] float attractMultiplier;
	[Space]
	[SerializeField] Animator loadFxAnimator;
	[SerializeField] MeshRenderer loadFxRenderer;
	[SerializeField] float loadFxShowSpeed;
	[Space]
	[SerializeField] float shockWaveOffset;

	float shockWaveCharge;
	public float normalizedCharge { get { return shockWaveCharge / maxLoadValue; } }

	Coroutine loadRoutine, showRoutine, hideRoutine;

	bool attractObjects = false;
	float attractColliderSize;
	List<Rigidbody> attractedObjects = new List<Rigidbody>();
	Coroutine attractCo;

	public Action onLoadWave;
	public Action onShockWave;

	public bool isCharging { get { return loadRoutine != null; } }

	void Start()
	{
		attractColliderSize = attractCollider.radius;
		attractCollider.radius = 0.01f;

		loadFxRenderer.material.SetFloat( "_Refraction", 0 );
		loadFxRenderer.material.SetFloat( "_Transparancy", 0 );
	}

	public void Charge()
	{
		if( loadRoutine == null )
		{
			loadRoutine = StartCoroutine( ShockLoad() );

			loadFxAnimator.SetTrigger( "Trigga" );
			if( hideRoutine != null )
			{
				StopCoroutine( hideRoutine );
				hideRoutine = null;
			}
			if( showRoutine == null )
				showRoutine = StartCoroutine( Show() );
		}

		if( attractObjects && attractCollider != null )
		{
			attractCollider.radius = attractColliderSize;
			foreach( AimantFX fx in GetComponentsInChildren<AimantFX>() )
				fx.StartEffect();
		}
	}

	public void Shock()
	{
		if( loadRoutine != null )
		{
			StopCoroutine( loadRoutine );
			if( onShockWave != null )
				onShockWave();
			loadRoutine = null;
			loadFxAnimator.SetTrigger( "Stop" );
			if( showRoutine != null )
			{
				StopCoroutine( showRoutine );
				showRoutine = null;
			}
			if( hideRoutine == null )
				hideRoutine = StartCoroutine( Hide() );

			if( attractObjects && attractCollider != null )
			{
				attractCollider.radius = 0.01f;
				foreach( AimantFX fx in GetComponentsInChildren<AimantFX>() )
					fx.EndEffect();
			}

			GameObject shockWave = Instantiate( shockWavePrefab, transform.position + new Vector3( 0.0f, 0.0f, shockWaveOffset ), shockWavePrefab.transform.rotation ) as GameObject;

			shockWave.GetComponent<ShockWave>().Init( Mathf.Clamp( shockWaveCharge, 0, maxLoadValue ) / maxLoadValue );

			shockWaveCharge = 0;
		}

	}

	IEnumerator Show()
	{
		WaitForSeconds wait = new WaitForSeconds( 0.1f );
		Material m = loadFxRenderer.material;

		loadFxRenderer.material.SetFloat( "_Refraction", 10.0f );
		loadFxRenderer.material.SetFloat( "_Transparancy", 0.01f );

		yield return wait;

		while( m.GetFloat( "_Refraction" ) < 100 )
		{
			m.SetFloat( "_Refraction", Mathf.Clamp( m.GetFloat( "_Refraction" ) + Time.deltaTime * loadFxShowSpeed * 100, 0, 100 ) );
			m.SetFloat( "_Transparancy", Mathf.Clamp01( m.GetFloat( "_Transparancy" ) + Time.deltaTime * loadFxShowSpeed ) );

			yield return wait;
		}

		m.SetFloat( "_Refraction", 100 );
		m.SetFloat( "_Transparancy", 1 );
	}

	IEnumerator Hide()
	{
		WaitForSeconds wait = new WaitForSeconds( 0.1f );
		Material m = loadFxRenderer.material;

		while( m.GetFloat( "_Refraction" ) > 0 )
		{
			m.SetFloat( "_Refraction", Mathf.Clamp( m.GetFloat( "_Refraction" ) - Time.deltaTime * loadFxShowSpeed * 100 * 1.5f, 0, 100 ) );
			m.SetFloat( "_Transparancy", Mathf.Clamp01( m.GetFloat( "_Transparancy" ) - Time.deltaTime * loadFxShowSpeed * 1.5f ) );

			yield return wait;
		}

		m.SetFloat( "_Refraction", 0 );
		m.SetFloat( "_Transparancy", 0 );
	}

	IEnumerator ShockLoad()
	{
		float precision = 0.1f;
		if( onLoadWave != null )
			onLoadWave();

		WaitForSeconds wait = new WaitForSeconds( precision );
		ShipsAgent agent = GetComponent<ShipsAgent>();

		while( true )
		{
			shockWaveCharge += precision;

			yield return wait;
		}
	}

	public void EnableAttractObjects( bool enabled )
	{
		print( "Enable Attract : " + enabled );
		if( enabled )
			attractObjects = true;
		else
		{
			attractCollider.radius = 0.01f;
			attractedObjects.Clear();
			attractObjects = false;
			foreach( AimantFX fx in GetComponentsInChildren<AimantFX>() )
				fx.EndEffect();
		}
	}

	void OnTriggerEnter( Collider coll )
	{
		if( coll.GetComponent<BounceableObject>() )
		{
			attractedObjects.Add( coll.attachedRigidbody );
			
			if( attractCo == null )
				attractCo = StartCoroutine( Attract() );
		}
	}

	void OnTriggerExit( Collider coll )
	{
		attractedObjects.Remove( coll.attachedRigidbody );
	}

	IEnumerator Attract()
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();
		Vector3 attractForce = Vector3.zero;

		while( attractedObjects.Count > 0 )
		{
			for( int i = 0; i < attractedObjects.Count; i++ )
			{
				if( attractedObjects[i] != null )
				{
					attractForce = transform.position - attractedObjects[i].position;
					float force = attractMultiplier / Mathf.Pow( attractForce.magnitude, 2 );
					attractedObjects[i].AddForce( attractForce * force * attractMultiplier, ForceMode.Acceleration );
				}
				else
					attractedObjects.Remove( attractedObjects[i] );
			}

			yield return wait;
		}
		attractCo = null;
	}
}