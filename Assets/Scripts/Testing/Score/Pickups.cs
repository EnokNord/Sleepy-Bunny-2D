using Unity.Cinemachine;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    // The code is based on https://www.youtube.com/watch?v=EfUCEwKmcjc 

    // The Layer the payer is on. Used for a hit check
    private const int PlayerLayer = 7;
    public bool BigPoints;

    AudioSource Sound;

    private void Start()
    {
        Sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer != PlayerLayer) return;

        //Grabbing the score system on the player
        Scoresystem scoresystem = other.GetComponent<Scoresystem>();

        if (scoresystem == null) return;
        
        //Sound.Play();

        
        if (BigPoints)
        {
            scoresystem.BigCollected();
        }
        else
        {
            scoresystem.SmallCollected();
        }

        Destroy(gameObject);


    }
}
