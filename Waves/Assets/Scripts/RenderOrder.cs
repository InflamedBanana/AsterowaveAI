using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RenderOrder : MonoBehaviour {

	public int renderOrder;

	// Use this for initialization
	void Start () {
	
		GetComponent<Renderer>().sortingOrder = renderOrder;

	}

}
