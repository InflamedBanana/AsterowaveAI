using UnityEngine;
using System.Collections;

public class PlayerLight: MonoBehaviour 
{
	[SerializeField] float timeMin;
	[SerializeField] float timeMax;
	[SerializeField] float alphaMin;
	[SerializeField] float alphaMax;
    MeshRenderer rend;

	void Start () 
	{
        rend = GetComponent<MeshRenderer>();
        StartCoroutine(Blink());
    }
	
	IEnumerator Blink () 
	{
		while(true)
        {
			yield return new WaitForSeconds(Random.Range(timeMin, timeMax));
			rend.material.SetColor("_TintColor", new Color(rend.material.GetColor("_TintColor").r, rend.material.GetColor("_TintColor").g, rend.material.GetColor("_TintColor").b, Random.Range(alphaMin, alphaMax)));
        }
	}
}