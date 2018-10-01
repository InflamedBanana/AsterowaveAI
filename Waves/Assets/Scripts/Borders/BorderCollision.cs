using UnityEngine;

public class BorderCollision: MonoBehaviour
{
	GameObject maskPrefab;
	[SerializeField] AudioClip shockClip;
	AudioSource audioSource;

	void Start()
	{
		maskPrefab = Resources.Load( "Mask" ) as GameObject;
		audioSource = GetComponent<AudioSource>();
	}

	void OnCollisionEnter( Collision collision )
	{
		GameObject go = Instantiate( maskPrefab, collision.contacts[0].point, Quaternion.identity );
		go.transform.eulerAngles = new Vector3( 270f, 0f, 0f );
		if( audioSource != null )
			audioSource.PlayOneShot( shockClip );
		CameraUtils.Instance.Shake( 0.06f, 0.1f );

		if( collision.collider.GetComponent <BounceableObject>() )
			collision.collider.GetComponent<Rigidbody>().AddForce( ( collision.collider.transform.position - transform.position ).normalized, ForceMode.Impulse );

		if( collision.collider.GetComponent<ShipsAgent>() )
			collision.collider.GetComponent<ShipsAgent>().AddReward( -.05f );
	}
}