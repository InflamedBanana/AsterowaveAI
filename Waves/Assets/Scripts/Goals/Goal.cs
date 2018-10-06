using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
	[SerializeField]
	TeamSide team;
	[SerializeField]
	List <GameObject> myPlayers = new List <GameObject>();
	[SerializeField]
	List <GameObject> ennemies = new List <GameObject>();
	[Space]
	[SerializeField]
	int life;
	int maxLife;

	[Space]
	[SerializeField] float moveSpeed;
	[SerializeField] bool reverseRotate;
	[SerializeField] Transform[] movePoints;
	[SerializeField] float endStopTime;
	[Space]
	[SerializeField] float repulseMultiplier;
	[Space]
	[SerializeField] float invulnerabilityTime;
	bool isInvulnerable;
	[Space]
	[SerializeField] GameObject[] basesMeshes;
	[SerializeField] Text scoreText;

	GoalCollectibles collectibles;

	//Action pour les sons
	public Action onBonus;
	public Action onMalus;
	public Action onShock;
	public Action onWin;

	public struct GoalState
	{
		public float health;
		public float maxHealth;
		public bool isInvulnerable;
		public bool hasShield;
		public bool isShrinked;
		public bool isGrowthed;
		public bool isInversed;
	}

	public GoalState GetCurrentState()
	{
		return new GoalState() {
			health = life,
			maxHealth = maxLife,
			isInvulnerable = isInvulnerable,
			hasShield = collectibles.HasShield,
			isShrinked = collectibles.IsShrinked,
			isGrowthed = collectibles.IsGrowthed,
			isInversed = collectibles.IsInverse
		};
	}

	void Start()
	{
		collectibles = GetComponent<GoalCollectibles>();
		maxLife = life;
		if( movePoints.Length > 0 )
			StartCoroutine( Move() );

		SetStringScore();
	}

	IEnumerator Move()
	{
		int i = 1;
		int direction = 1;

		WaitForSeconds wait = new WaitForSeconds( endStopTime );

		while( true )
		{
			if( Vector3.Distance( transform.position, movePoints[i].position ) < 0.1f )
			{
				if( i == movePoints.Length - 1 && direction == 1 )
				{
					direction = -1;
					yield return wait;
				}
				else if( i == 0 && direction == -1 )
				{
					direction = 1;
					yield return wait;
				}

				i += direction;
			}

			if( ( transform.position - movePoints[i].position ) != Vector3.zero )
			{
				transform.rotation = ( direction > 0 ) ? Quaternion.LookRotation( transform.position - movePoints[i].position, transform.up ) : Quaternion.LookRotation( movePoints[i].position - transform.position, transform.up );
				if( !reverseRotate )
					transform.Rotate( new Vector3( 0.0f, -90.0f, 0.0f ) );
				else
					transform.Rotate( new Vector3( 0.0f, 90.0f, 0.0f ) );
			}

			transform.position = Vector3.MoveTowards( transform.position, movePoints[i].position, moveSpeed * Time.deltaTime );
			yield return null;
		}
	}

	public void ResetGoal()
	{
		life = maxLife;
		SetStringScore();
	}

	void OnCollisionEnter( Collision coll )
	{
		if( !myPlayers.Contains( coll.gameObject ) && coll.rigidbody.GetComponent <PlayerMoves>() )
		{
			coll.rigidbody.GetComponent <Rigidbody>().velocity = Vector3.zero;
			coll.rigidbody.GetComponent <Rigidbody>().AddForce( ( coll.transform.position - transform.position ).normalized * 5 * repulseMultiplier, ForceMode.VelocityChange );
			return;
		}

		BounceableObject bounceable = coll.rigidbody.GetComponent<BounceableObject>();

		if( bounceable == null )
			return;

		if( bounceable is Junk )
		{
			TakeDamage();
			if( onShock != null )
				onShock();
		}
		else if( bounceable is ShieldBonus )
		{
			collectibles.GetShield();
			PlayersAddReward( .4f, myPlayers );
			PlayersAddReward( -.1f, ennemies );

			if( onBonus != null )
				onBonus();
		}
		else if( bounceable is ShrinkBonus )
		{
			collectibles.ShrinkBase();
			PlayersAddReward( .3f, myPlayers );
			PlayersAddReward( -.1f, ennemies );

			if( onBonus != null )
				onBonus();
		}
		else if( bounceable is GrowMalus )
		{
			collectibles.GrowBase();
			PlayersAddReward( -.1f, myPlayers );
			PlayersAddReward( .3f, ennemies );

			if( onMalus != null )
				onMalus();
		}
		else if( bounceable is InverseInputsMalus )
		{
			collectibles.InverseInputs();
			PlayersAddReward( -.1f, myPlayers );
			PlayersAddReward( .4f, ennemies );

			if( onMalus != null )
				onMalus();
		}
		else if( bounceable is AttractAsteroidBonus )
		{
			collectibles.EnableAttract();
			PlayersAddReward( .1f, myPlayers );
			PlayersAddReward( -.1f, ennemies );

			if( onBonus != null )
				onBonus();
		}
		else
			return;

		bounceable.RemoveObject();
	}

	void PlayersAddReward( float _value, List<GameObject> _players )
	{
		//foreach( GameObject go in _players )
		//	if( go.GetComponent<ShipsAgent>() != null )
		//		go.GetComponent<ShipsAgent>().AddReward( _value );
	}

	void TakeDamage()
	{
		//if( isInvulnerable )
		//	return;

		foreach( GameObject go in myPlayers )
			if( go.GetComponent<ShipsAgent>() != null )
				go.GetComponent<ShipsAgent>().PlayerLoose();

		foreach( GameObject go in ennemies )
			if( go.GetComponent<ShipsAgent>() != null )
				go.GetComponent<ShipsAgent>().PlayerWin();

		ShipsAcademy.Instance.AcademyReset();

		//CameraUtils.Instance.Shake( 0.2f, 0.2f );

		//if( collectibles.HasShield )
		//{
		//	collectibles.LooseShield();
		//	return;
		//}
		//if( collectibles.IsGrowthed )
		//	collectibles.InverseGrow();
		//else if( collectibles.IsShrinked )
		//	collectibles.RegrowBase();

		//life--;

		//if( basesMeshes.Length > 0 )
		//{
		//	if( life == maxLife - ( maxLife / 3 ) )
		//	{
		//		basesMeshes[0].SetActive( false );
		//		basesMeshes[1].SetActive( true );
		//	}
		//	else if( life == maxLife / 3 )
		//	{
		//		basesMeshes[1].SetActive( false );
		//		basesMeshes[2].SetActive( true );
		//	}
		//}

		//if( life <= 0 )
		//{
		//	Death();
		//	return;
		//}

		//StartCoroutine( Invulnerability() );
	}

	void SetStringScore()
	{
		scoreText.text = life.ToString( "00" ) + " / " + maxLife.ToString( "00" );
	}

	void Death()
	{
//		GetComponentInChildren<Collider>().enabled = false;
//		GetComponentInChildren<GoalExplosion>().EnableExplosion();
//		CameraUtils.Instance.Shake( 0.5f, 3f );

		foreach( GameObject go in myPlayers )
			if( go.GetComponent<ShipsAgent>() != null )
				go.GetComponent<ShipsAgent>().PlayerLoose();

		foreach( GameObject go in ennemies )
			if( go.GetComponent<ShipsAgent>() != null )
				go.GetComponent<ShipsAgent>().PlayerWin();

		ShipsAcademy.Instance.AcademyReset();

		SetStringScore();
//		Goal[] goals = FindObjectsOfType<Goal>();
//		Goal winnerGoal = null;
//
//		for( int i = 0; i < goals.Length; i++ )
//			if( goals[i].Team != team )
//				winnerGoal = goals[i];
		
//
//		PlayerInputs[] players = FindObjectsOfType<PlayerInputs>();
//
//		for( int i = 0; i < players.Length; i++ )
//			players[i].EnableInputs( false );
//
//		JunkSpawner.Instance.canSpawn = false;

		JunkSpawner.Instance.CleanJunks();
		//CollectiblesSpawners.Instance.ClearCollectibles();
		

//		VictoryCanvas.Instance.Show( winnerGoal.Team, winnerGoal.life, maxLife );
//		if( onWin != null )
//			onWin();
		print( "DEATH" );
	}

	IEnumerator Invulnerability()
	{
		isInvulnerable = true;
		yield return new WaitForSeconds( invulnerabilityTime );
		isInvulnerable = false;
	}

	public List <GameObject> MyPlayers { get { return myPlayers; } }

	public TeamSide Team { get { return team; } }

}

public enum Players
{
	Player1,
	Player2,
}