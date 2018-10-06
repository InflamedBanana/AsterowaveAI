using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
	[SerializeField] float acceleration;
	[SerializeField] float maxSpeed;
	[SerializeField] ParticleSystem confuseFX;
	//PlayerInputs inputs;
	Rigidbody m_body;

	public Rigidbody body { get { return m_body; } }

	int inputDir = 1;

	void Start()
	{
		//inputs = GetComponent<PlayerInputs> ();

		m_body = GetComponent<Rigidbody>();

		//EnableMoves ( true );
	}

	public void Move( Vector2 direction )
	{
		m_body.AddForce( direction * acceleration * inputDir, ForceMode.Acceleration );

		if( direction != Vector2.zero )
			transform.rotation = Quaternion.LookRotation( direction, -Vector3.forward );

		m_body.velocity = Vector3.ClampMagnitude( m_body.velocity, maxSpeed );
	}

	public void InverseDir( bool enable )
	{
		if( enable )
		{ 
			confuseFX.Play();
			inputDir = -1;
		}
		else
		{
			confuseFX.Stop();
			inputDir = 1;
		}
	}

	//public void EnableMoves ( bool enable )
	//{
	//	if ( enable )
	//		inputs.LeftJoystick = Move;
	//	else
	//		inputs.LeftJoystick -= Move;
	//}
}