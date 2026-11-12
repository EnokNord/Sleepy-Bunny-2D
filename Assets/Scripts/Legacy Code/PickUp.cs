using UnityEngine;
using static Global.GlobalVariablesLibrary;


namespace PickUps
{
    public enum CollectibleType
    {
        Dustbunny, Carrot
    }
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PickUp: MonoBehaviour
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
            audioManager.PlaySoundEffect(soundEffect);
            OnPickUp(other);
            GetComponent<SpriteRenderer>().enabled = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != PlayerLayer) return;
            OnAfterPickUp(other);
            Destroy(this);
        }

        public abstract void OnPickUp(Collider2D collisionObject);
        public abstract void OnAfterPickUp(Collider2D collisionObject);
    }
}