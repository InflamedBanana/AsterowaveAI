using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesSpawners : MonoBehaviourSingleton<CollectiblesSpawners>
{
	[Header( "Collectibles" )]
	[SerializeField] List <GameObject> bonusPrefabs = new List<GameObject>();
	[SerializeField] List <GameObject> malusPrefabs = new List<GameObject>();

	[Header( "Refs" )]
	[SerializeField] Transform goalRef;
	[SerializeField] Transform upSpawner;
	[SerializeField] Transform downSpawner;

	[Header( "Stats" )]
	[SerializeField] float collectibleSpawnMinDelay;
	[SerializeField] float collectibleSpawnMaxDelay;
	[SerializeField] float spawnForceIntensity;

	public GameObject currentBonus;
	public GameObject currentMalus;

	[HideInInspector] public bool canSpawn = true;

	float delay;
	float waitDelay;

	public void ClearCollectibles()
	{
		if( currentBonus != null )
		{
			Destroy( ( currentBonus ) );
			currentBonus = null;
		}
		if( currentMalus != null )
		{
			Destroy( currentMalus );
			currentMalus = null;
		}

		delay = .0f;
		waitDelay = Random.Range( collectibleSpawnMinDelay, collectibleSpawnMaxDelay );
	}

	void Start()
	{
		waitDelay = Random.Range( collectibleSpawnMinDelay, collectibleSpawnMaxDelay );
	}

	void Update()
	{
		if( !canSpawn )
			return;

		if( currentBonus != null && currentMalus != null )
			return;
		
		delay += Time.deltaTime;

		if( delay < waitDelay )
			return;

		int i = 0;

		if( currentBonus == null && currentMalus == null )
			i = Random.Range( 0, 2 );
		else if( currentBonus != null )
			i = 1;


		GameObject collectibleToSpawn = null;
		if( i == 0 )
			collectibleToSpawn = bonusPrefabs[Random.Range( 0, bonusPrefabs.Count )];
		else
			collectibleToSpawn = malusPrefabs[Random.Range( 0, malusPrefabs.Count )];
		
		GameObject collectible = null;
		if( goalRef.transform.position.y >= 0 )
			collectible = Instantiate( collectibleToSpawn, downSpawner.position, collectibleToSpawn.transform.rotation ) as GameObject;
		else
			collectible = Instantiate( collectibleToSpawn, upSpawner.position, collectibleToSpawn.transform.rotation ) as GameObject;

		Vector3 force = -collectible.transform.position.normalized;
		force = force.WithX( Random.Range( -1.0f, 1.0f ) );
		force.Normalize();
		force *= spawnForceIntensity;
		collectible.GetComponent<Rigidbody>().AddForce( force, ForceMode.Impulse );

		if( i == 0 )
			currentBonus = collectible;
		else
			currentMalus = collectible;

		delay = 0;
		waitDelay = Random.Range( collectibleSpawnMinDelay, collectibleSpawnMaxDelay );
	}
}