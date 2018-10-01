using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPortal : MonoBehaviour
{
	public float speed;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (0f, speed * Time.deltaTime, 0f));
	}
}
