using UnityEngine;

public class VinjetFlicker : MonoBehaviour
{
    private SpriteRenderer spriterender;
    [SerializeField] Sprite[] flickerStates;

    // [SerializeField] GameObject[] Cinemachine; No clue how to fix that ask mina perhaps?
    private Sprite newState;

    [SerializeField] float flickerSpeed;

    public bool IsEyey = false;

    private int CurrentSprite;

    private float flickerTime;

    private void Start()
    {
        flickerTime = flickerSpeed;
        spriterender = GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {

        if(flickerTime <= 0)
        {

            if (CurrentSprite == flickerStates.Length)
            {

                //Debug.Log(flickerStates.Length + "crurrent Sprite");

                CurrentSprite = 0;
                return;
            }
            if(IsEyey)
            {
                flickerTime = Random.Range(0.5f, 2);
            }
            flickerTime = flickerSpeed;

            newState = flickerStates[CurrentSprite];
            spriterender.sprite = newState;


            CurrentSprite++;
            return;
        }

        flickerTime -= Time.deltaTime;
        
    }
}
