using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviourSingleton<JunkSpawner>
{
	[Header( "Junks" )]
	[SerializeField] List <GameObject> junkPrefabs = new List<GameObject>();
	[SerializeField] GameObject junkDestroyPrefab;
	[HideInInspector]private List <Junk> m_junkSpawned = new List<Junk>();

	public int junkCount { get { return m_junkSpawned.Count; } }
	public Junk GetJunk( int _index ) { return m_junkSpawned[ _index ]; }

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

		if( m_junkSpawned.Count == maxJunkCount || junkCadence < junkSpawnCadence )
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
				junk = Instantiate( junkToSpawn, downSpawner.position, junkToSpawn.transform.rotation );
			else
				junk = Instantiate( junkToSpawn, upSpawner.position, junkToSpawn.transform.rotation );
		}
		else
		{
			if( goalRef.transform.position.y >= 0 )
				junk = Instantiate( junkToSpawn, downSpawner.position, junkToSpawn.transform.rotation );
			else
				junk = Instantiate( junkToSpawn, upSpawner.position, junkToSpawn.transform.rotation );
		}

		Vector3 force = -junk.transform.position.normalized;
		force = force.WithX( Random.Range( -1.0f, 1.0f ) );
		force.Normalize();
		force *= spawnForceIntensity;
		//junk.GetComponent<Rigidbody>().AddForce( force, ForceMode.Impulse );
		junk.GetComponent<Rigidbody>().AddTorque( new Vector3( Random.Range( 0f, 1f ), Random.Range( 0f, 1f ), Random.Range( 0f, 1f ) ) * 50 );

		junk.transform.localScale *= Random.Range( 1f, 1.5f );

		m_junkSpawned.Add( junk.GetComponent<Junk>() );
		junk.GetComponent<Junk>().OnDestroyAct += RemoveJunk;
	}

	public void CleanJunks()
	{
		foreach( Junk junk in m_junkSpawned )
			Destroy( junk.gameObject );

		m_junkSpawned.Clear();
		junkCadence = 0;

		SpawnJunk();
	}

	void RemoveJunk( BounceableObject g )
	{
		if( g is Junk )
		{
			var junk = (Junk)g;

			if( m_junkSpawned.Contains( junk ) )
			{
				Instantiate( junkDestroyPrefab, junk.transform.position, junkDestroyPrefab.transform.rotation );
				m_junkSpawned.Remove( junk );
			}
		}
	}
}