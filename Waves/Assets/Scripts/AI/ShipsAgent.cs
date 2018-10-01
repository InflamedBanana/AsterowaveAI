using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ShipsAgent : Agent
{
	[SerializeField] private Goal m_myGoal = null;
	[SerializeField] private Goal m_othersGoal = null;


	PlayerMoves moves = null;
	PlayerAttack attacks = null;

	bool isCharging = false;

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
		Goal.GoalState goalState = m_myGoal.GetCurrentState();
		AddVectorObs( goalState.health / goalState.maxHealth );
		AddVectorObs( goalState.hasShield );
		AddVectorObs( goalState.isGrowthed );
		AddVectorObs( goalState.isInversed );
		AddVectorObs( goalState.isInvulnerable );
		AddVectorObs( goalState.isShrinked );
//		AddVectorObs( m_myGoal.transform.position );
		AddVectorObs( new Vector2( ( m_myGoal.transform.position.x - 19.69f ) / ( 19.69f * 2f ), ( m_myGoal.transform.position.y + 11 ) / 22f ) );
		AddVectorObs( (int)m_myGoal.Team );
		// x -19.69  +  Y 11 
		//others goal state
		goalState = m_othersGoal.GetCurrentState();
		AddVectorObs( goalState.health );
		AddVectorObs( new Vector2( ( m_othersGoal.transform.position.x - 19.69f ) / ( 19.69f * 2f ), ( m_othersGoal.transform.position.y + 11 ) / 22f ) );
//		AddVectorObs( m_othersGoal.transform.position );
		AddVectorObs( (int)m_othersGoal.Team );

		//AgentState
		AddVectorObs( new Vector2( ( transform.position.x - 19.69f ) / ( 19.69f * 2f ), ( transform.position.y + 11 ) / 22f ) );
		AddVectorObs( new Vector2( moves.rigidbody.velocity.x, moves.rigidbody.velocity.y ) );
		AddVectorObs( transform.rotation.eulerAngles.z / 360f );
		AddVectorObs( (int)m_myGoal.Team );

		//junkState
		for( int i = 0; i < 5; ++i )
		{
			if( i >= JunkSpawner.Instance.junkSpawned.Count )
			{
				AddVectorObs( 0 );
				AddVectorObs( Vector2.zero );
				AddVectorObs( Vector2.zero );

			}
			else
			{
				AddVectorObs( 1 );
				Transform junk = JunkSpawner.Instance.junkSpawned[i].transform;
				AddVectorObs( new Vector2( ( junk.position.x - 19.69f ) / ( 19.69f * 2f ), ( junk.position.y + 11 ) / 22f ) );
				Vector3 velocity = junk.GetComponent<Rigidbody>().velocity;
				AddVectorObs( new Vector2( velocity.x, velocity.y ) );
			}
		}

		//if( CollectiblesSpawners.Instance.currentBonus != null )
		//{
		//	AddVectorObs( 2 );

		//	Transform trans = CollectiblesSpawners.Instance.currentBonus.transform;
		//	AddVectorObs( new Vector2( ( trans.position.x - 19.69f ) / ( 19.69f * 2f ), ( trans.position.y + 11 ) / 22f ) );
		//	Vector3 velocity = CollectiblesSpawners.Instance.currentBonus.GetComponent<Rigidbody>().velocity;
		//	AddVectorObs( new Vector2( velocity.x, velocity.y ) );
		//}
		//else
		//{
		//	AddVectorObs( 0 );
		//	AddVectorObs( Vector2.zero );
		//	AddVectorObs( Vector2.zero );
		//}

		//if( CollectiblesSpawners.Instance.currentMalus != null )
		//{
		//	AddVectorObs( 3 );

		//	Transform trans = CollectiblesSpawners.Instance.currentMalus.transform;
		//	AddVectorObs( new Vector2( ( trans.position.x - 19.69f ) / ( 19.69f * 2f ), ( trans.position.y + 11 ) / 22f ) );
		//	Vector3 velocity = CollectiblesSpawners.Instance.currentMalus.GetComponent<Rigidbody>().velocity;
		//	AddVectorObs( new Vector2( velocity.x, velocity.y ) );
		//}
		//else
		{
			AddVectorObs( 0 );
			AddVectorObs( Vector2.zero );
			AddVectorObs( Vector2.zero );
		}
	}

	public override void AgentAction( float[] vectorAction, string textAction )
	{
		base.AgentAction( vectorAction, textAction );

		if( vectorAction[0] != .0f && vectorAction[1] != .0f && vectorAction[0] <= 1f && vectorAction[1] <= 1f && vectorAction[0] >= -1f && vectorAction[1] >= -1f )
		{
			moves.Move( Vector2.ClampMagnitude( new Vector2( vectorAction[0], vectorAction[1] ), 1f ) );
			AddReward( .01f );
		}

		if( vectorAction[2] < .5f && !isCharging )
		{
			attacks.Charge();
			AddReward( .01f );
			isCharging = true;
		}
		else if( vectorAction[2] >= .5f && isCharging )
		{
			AddReward( .02f );
			attacks.Shock();
			isCharging = false;
		}
	}

	void LateUpdate()
	{
		SetReward( Mathf.Clamp( GetReward(), -1f, 1f ) );
	}

	public void PlayerDone()
	{
//		Done();
		SetReward( -1f );
	}

	public void PlayerWin()
	{
//		Done();
		SetReward( 1f );
	}

	public override void AgentReset()
	{
	}
}
