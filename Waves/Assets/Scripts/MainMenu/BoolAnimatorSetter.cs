using UnityEngine;
using UnityEngine.UI;

public class BoolAnimatorSetter : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] string prefName;
	[SerializeField] bool value;

	void Start ()
	{
		GetComponent<Button> ().onClick.AddListener ( Click );
	}

	void Click ()
	{
		animator.SetBool ( prefName, value );
	}
}