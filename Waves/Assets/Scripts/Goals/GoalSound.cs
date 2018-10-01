using UnityEngine;

public class GoalSound: MonoBehaviour 
{
    [SerializeField] AudioClip bonusSound;
    [SerializeField] AudioClip malusSound;
    [SerializeField] AudioClip junkSound;
    [SerializeField] AudioClip explosionSound;
    AudioSource audioSource;

	void Start () 
	{
        audioSource = GetComponent<AudioSource>();
        Goal goal = GetComponent<Goal>();
        goal.onBonus += PlayBonus;
        goal.onMalus += PlayMalus;
        goal.onShock += PlayJunk;
        goal.onWin += PlayFinalExplosion;
    }
	
	void PlayBonus () 
	{
        audioSource.PlayOneShot(bonusSound);
    }
    void PlayMalus()
    {
        audioSource.PlayOneShot(malusSound);
    }
    void PlayJunk()
    {
        audioSource.PlayOneShot(junkSound);
    }
    void PlayFinalExplosion()
    {
        audioSource.PlayOneShot(explosionSound);
    }
}