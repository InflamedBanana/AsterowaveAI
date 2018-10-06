using UnityEngine;
using MLAgents;

public class ShipsAgent : Agent
{
	[SerializeField] private Goal m_myGoal = null;
	[SerializeField] private Goal m_othersGoal = null;


	PlayerMoves moves = null;
	PlayerAttack attacks = null;

	bool m_isCharging = false;

	private void Awake()
	{
		Getrefs();
	}

	private void Getrefs()
	{
		moves = GetComponent<PlayerMoves>();
		attacks = GetComponent<PlayerAttack>();
	}

	public override void InitializeAgent()
	{
		if( moves == null )
			Getrefs();
	}

	public override void CollectObservations()
	{
		//Goal state :
		//Goal.GoalState goalState = m_myGoal.GetCurrentState();
		//AddVectorObs( goalState.health / goalState.maxHealth ); don't care
		//AddVectorObs( goalState.hasShield ); // maybelater
		//AddVectorObs( goalState.isGrowthed );	// maybelater
		//AddVectorObs( goalState.isInversed );	// maybelater
		//AddVectorObs( goalState.isInvulnerable );	// maybelater
		//AddVectorObs( goalState.isShrinked );	// maybelater

		//agent
		AddVectorObs( (int)m_myGoal.Team );
		AddVectorObs( GetViewportPos( transform.position ) );
		AddVectorObs( new Vector2( moves.body.velocity.x, moves.body.velocity.y ).normalized );

		//ally goal
		AddVectorObs( GetViewportPos( m_myGoal.transform.position ) );
		AddVectorObs( (int)m_myGoal.Team );

		//enemy goal
		AddVectorObs( GetViewportPos( m_othersGoal.transform.position ) );
		AddVectorObs( (int)m_othersGoal.Team );

		//junkState
		//for( int i = 0; i < 1; ++i )
		//{
		//	if( i >= JunkSpawner.Instance.junkCount )
		//	{
		//		//AddVectorObs( 0 );
		//		AddVectorObs( Vector2.zero );
		//		AddVectorObs( Vector2.zero );

		//	}
		//	else
		//	{
		//		//AddVectorObs( 1 );

		//		Junk junk = JunkSpawner.Instance.GetJunk( i );

		//		AddVectorObs( GetViewportPos( junk.transform.position ) );
		//		AddVectorObs( new Vector2( junk.body.velocity.x, junk.body.velocity.y ).normalized );
		//	}
		//}

		Junk junk = JunkSpawner.Instance.GetJunk( 0 );

		if( junk != null && junk.body != null )
		{
			AddVectorObs( GetViewportPos( junk.transform.position ) );
			AddVectorObs( new Vector2( junk.body.velocity.x, junk.body.velocity.y ).normalized );
		}
		else
		{
			AddVectorObs( Vector2.zero );
			AddVectorObs( Vector2.zero );
		}
	}

	private Vector2 GetViewportPos( Vector3 _position )
	{
		return Camera.main.WorldToViewportPoint( _position );
	}

	public override void AgentAction( float[] vectorAction, string textAction )
	{
		base.AgentAction( vectorAction, textAction );

		if( vectorAction[0] != .0f && vectorAction[1] != .0f && vectorAction[0] <= 1f && vectorAction[1] <= 1f && vectorAction[0] >= -1f && vectorAction[1] >= -1f )
		{
			moves.Move( Vector2.ClampMagnitude( new Vector2( vectorAction[0], vectorAction[1] ), 1f ) );
			AddReward( .001f );
		}

		if( !attacks.isCharging && vectorAction[ 2] >= .5f )
		{
			attacks.Charge();
			AddReward( .07f );
		}
		else if( attacks.isCharging && vectorAction[ 2 ] > 0 && vectorAction[2] < .5f )
		{
			attacks.Shock();
			
			AddReward( .015f * attacks.normalizedCharge );
		}

		AddReward( -.005f );
	}

	public void PlayerLoose()
	{
		SetReward( -1f );
		Debug.LogError( "Player Loose" );
		Done();
	}

	public void PlayerWin()
	{
		SetReward( 1f );
		Debug.LogError( "Player Win" );
		Done();
	}

	public override void AgentReset()
	{
		moves.body.velocity = Vector3.zero;

		transform.position = Camera.main.ViewportToWorldPoint( new Vector3( .2f, .5f, 0F ) ).WithZ( moves.body.position.z );
	}
}
