using System.Collections;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
	[SerializeField] string horInput;
	[SerializeField] string vertInput;
	[SerializeField] string shockInput;

	bool inputsEnabled;
	Coroutine fakeUpdateCo, fakeFixedCo;
	
	bool lastFrame;

	PlayerMoves moves = null;
	PlayerAttack attacks = null;

	private void Awake()
	{
		moves = GetComponent<PlayerMoves>();
		attacks = GetComponent<PlayerAttack>();
	}

	void Start()
	{
		EnableInputs( true );
	}

	IEnumerator FakeUpdate()
	{
		while( true )
		{
			if( Input.GetButtonDown( shockInput ) )
				attacks.Charge();
			else if( Input.GetButtonUp( shockInput ) )
				attacks.Shock();
			
			yield return null;
		}
	}

	IEnumerator FakeFixedUpdate()
	{
		WaitForSeconds wait = new WaitForSeconds( Time.fixedDeltaTime ); //change with CustomPhysics

		while( true )
		{
			if( Input.GetAxisRaw( vertInput ) != 0 || Input.GetAxisRaw( horInput ) != 0 )
			{
				lastFrame = true;
				moves.Move( Vector2.ClampMagnitude( new Vector2( Input.GetAxis( horInput ), Input.GetAxis( vertInput ) ), 1f ) );

			}
			else if( lastFrame )
			{
				moves.Move( Vector2.zero );
				lastFrame = false;
			}
				
			yield return wait;
		}
	}

	public void EnableInputs( bool enable )
	{
		if( enable )
		{
			if( fakeUpdateCo == null )
				fakeUpdateCo = StartCoroutine( FakeUpdate() );
			if( fakeFixedCo == null )
				fakeFixedCo = StartCoroutine( FakeFixedUpdate() );

			inputsEnabled = true;
		}
		else
		{
			if( fakeUpdateCo != null )
			{
				StopCoroutine( fakeUpdateCo );
				fakeUpdateCo = null;
			}
			if( fakeFixedCo != null )
			{
				StopCoroutine( fakeFixedCo );
				fakeFixedCo = null;
			}

			inputsEnabled = false;
		}
	}

	public bool LeftJoystickHeld { get { return ( Input.GetAxis( vertInput ) != 0 || Input.GetAxis( horInput ) != 0 ); } }

	public bool InputsEnabled { get { return inputsEnabled; } }
}
