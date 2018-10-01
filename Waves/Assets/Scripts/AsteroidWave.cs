using UnityEngine;
using System.Collections;

public class AsteroidWave: MonoBehaviour 
{
    [SerializeField] float waitTime = 1.5f;
	void Start () 
	{
        StartCoroutine(Death());
	}
	
	IEnumerator Death () 
	{
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
	}
}