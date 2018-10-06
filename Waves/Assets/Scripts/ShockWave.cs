using UnityEngine;

public class ShockWave : MonoBehaviour
{
	[SerializeField] float growSpeed;
	[SerializeField] AnimationCurve speedEvolution;
	[SerializeField] MeshRenderer[] renderers;

	[SerializeField] float FxHideSpeed;

	float maxRay = 2f;
	float growSpeedCoef = 1;

	ShipsAgent m_myAgent;

	void Start()
	{
		transform.localScale = new Vector3( 0.1f, 0.1f, 0.1f );
	}

	void Update()
	{
		Vector3 growValue = new Vector3( growSpeed, 0, growSpeed ) * growSpeedCoef * Time.deltaTime * speedEvolution.Evaluate( ( transform.localScale.x / 2 ) / maxRay );

		transform.localScale += growValue;

		if( transform.localScale.x / 2 >= maxRay )
			Destroy( gameObject );

		foreach( MeshRenderer r in renderers )
		{
			Material m = r.material;

			m.SetFloat( "_Refraction", Mathf.Clamp( m.GetFloat( "_Refraction" ) - Time.deltaTime * FxHideSpeed * 100, 0, 100 ) );
			m.SetFloat( "_Transparancy", Mathf.Clamp01( m.GetFloat( "_Transparancy" ) - Time.deltaTime * FxHideSpeed ) );
		}
	}

	void OnTriggerEnter( Collider _coll )
	{
		if( _coll.GetComponent<BounceableObject>() == null || m_myAgent == null )
			return;

		Debug.Log( "agent hit" );
		m_myAgent.AddReward( .1f );
	}

	public void Init( float chargePercentage, ShipsAgent _agent = null )
	{
		maxRay *= ( 1 + chargePercentage );
		growSpeedCoef *= ( 1 + chargePercentage );

		m_myAgent = _agent;
	}

	public float MaxRay { get { return this.maxRay; } }

	public float GrowSpeedCoef { get { return this.growSpeedCoef; } set { growSpeedCoef = value; } }
}