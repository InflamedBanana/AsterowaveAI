using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] Transform target;
	SphereCollider coll;

	void Start()
	{
		coll = GetComponentInChildren<SphereCollider>();
	}

	void OnTriggerEnter ( Collider other )
	{
		if ( other.GetComponent<PlayerInputs> () != null )
			return;

		Vector3 offset = other.attachedRigidbody.velocity.normalized * coll.radius;
		offset += other.attachedRigidbody.velocity.normalized * other.GetComponentInChildren<SphereCollider>().radius;
		offset += other.attachedRigidbody.velocity.normalized;

		offset.z = 0.0f;

		other.transform.position = target.transform.position + offset;
	}
}