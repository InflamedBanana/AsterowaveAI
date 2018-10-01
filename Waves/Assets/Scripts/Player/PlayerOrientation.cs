using UnityEngine;

public class PlayerOrientation: MonoBehaviour 
{
    Vector3 lastForward;
    Transform parent;
    Animator animator;
    float dotProduct;

    void Start()
    {
        parent = transform.parent;
        animator = GetComponent<Animator>();
    }

	void Update () 
	{
        dotProduct = Mathf.Lerp(dotProduct, MathGeom.SignedDotProduct(parent.forward, lastForward, Vector3.forward) * 10f, 0.2f);
        animator.SetFloat("Blend", dotProduct);
        lastForward = parent.forward;

    }
}