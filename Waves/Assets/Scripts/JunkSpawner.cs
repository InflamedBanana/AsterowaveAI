using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviourSingleton<JunkSpawner>
{
	[Header( "Junks" )]
	[SerializeField] List <GameObject> junkPrefabs = new List<GameObject>();
	[SerializeField] GameObject junkDestroyPrefab;
	[HideInInspector]public List <GameObject> junkSpawned = new List<GameObject>();

	[Header( "Refs" )]
	[SerializeField] Transform goalRef;
	[SerializeField] Transform upSpawner;
	[SerializeField] Transform downSpawner;

	[Header( "Stats" )]
	[SerializeField] float maxJunkCount;
	[SerializeField] float junkSpawnCadence;
	[SerializeField] float spawnForceIntensity;

	[Header( "4 Players" )]
	[SerializeField] GameObject[] players;

	float junkCadence;
	[SerializeField] private bool m_allowAutoSpawn = false;
	[HideInInspector]public bool canSpawn = true;

	void Start()
	{
		if( PlayerPrefs.GetInt( "PlayersCount" ) == 4 )
			for( int i = 0; i < players.Length; i++ )
				players[i].SetActive( true );
		else
			for( int i = 0; i < players.Length; i++ )
				players[ i ].SetActive( false );
	}


	void Update()
	{
		if( !m_allowAutoSpawn || !canSpawn )
			return;

		junkCadence += Time.deltaTime;

		if( junkSpawned.Count == maxJunkCount || junkCadence < junkSpawnCadence )
			return;

		SpawnJunk();

		junkCadence = 0;
	}

	public void SpawnJunk()
	{
		GameObject junkToSpawn = junkPrefabs[ Random.Range( 0, junkPrefabs.Count ) ];
		GameObject junk = null;
		
		if( goalRef == null )
		{
			int randUpOrDown = Random.Range( 0, 2 );
			
			if( randUpOrDown == 0 )
				junk = Instantiate( junkToSpawn, downSpawner.position, junkToSpawn.transform.rotation ) as GameObject;
			else
				junk = Instantiate( junkToSpawn, upSpawner.position, junkToSpawn.transform.rotation ) as GameObject;
		}
		else
		{
			if( goalRef.transform.position.y >= 0 )
				junk = Instantiate( junkToSpawn, downSpawner.position, junkToSpawn.transform.rotation ) as GameObject;
			else
				junk = Instantiate( junkToSpawn, upSpawner.position, junkToSpawn.transform.rotation ) as GameObject;
		}

		Vector3 force = -junk.transform.position.normalized;
		force = force.WithX( Random.Range( -1.0f, 1.0f ) );
		force.Normalize();
		force *= spawnForceIntensity;
		junk.GetComponent<Rigidbody>().AddForce( force, ForceMode.Impulse );
		junk.GetComponent<Rigidbody>().AddTorque( new Vector3( Random.Range( 0f, 1f ), Random.Range( 0f, 1f ), Random.Range( 0f, 1f ) ) * 50 );

		junk.transform.localScale *= Random.Range( 1f, 1.5f );

		junkSpawned.Add( junk );
		junk.GetComponent<Junk>().OnDestroyAct += RemoveJunk;
	}

	public void CleanJunks()
	{
		foreach( GameObject junk in junkSpawned )
			Destroy( junk );

		junkSpawned.Clear();
		junkCadence = 0;
	}

	void RemoveJunk( GameObject g )
	{
		if( junkSpawned.Contains( g ) )
		{
			Instantiate( junkDestroyPrefab, g.transform.position, junkDestroyPrefab.transform.rotation );
			junkSpawned.Remove( g );
		}
	}
}