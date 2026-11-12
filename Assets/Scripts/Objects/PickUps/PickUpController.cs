using PickUps;
using UnityEngine;
using static Global.GlobalVariablesLibrary;

public class PickUpController : MonoBehaviour
{
    [SerializeField] CollectibleType collectibleType;
    [SerializeField] string soundEffect;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != PlayerLayer) return;
        if(audioManager) audioManager.PlaySoundEffect(soundEffect);
        
        GetComponent<SpriteRenderer>().enabled = false;
    }

   
}
