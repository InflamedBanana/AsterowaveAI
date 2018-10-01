using UnityEngine;

public class PlayerSound: MonoBehaviour 
{
    [SerializeField] AudioClip shipShock;
    [SerializeField] AudioClip loadWave;
    [SerializeField] AudioClip shockWave;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        playerAttack.onLoadWave += PlayLoad;
        playerAttack.onShockWave += PlayShockWave;
    }

	void OnCollisionEnter(Collision col)
    {
        if(col.transform.GetComponent<PlayerMoves>() != null)
        {
            audioSource.PlayOneShot(shipShock);
        }
    }

    void PlayLoad()
    {
        audioSource.PlayOneShot(loadWave);
    }

    void PlayShockWave()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(shockWave);
    }
}