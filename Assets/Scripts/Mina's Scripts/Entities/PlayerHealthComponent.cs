using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerHealthComponent : HealthComponent
{
    [SerializeField] float deathHeight = 30;
    [SerializeField] float impactVelocityThreshold = 40;
    PlayerInputManager inputManager;
    Rigidbody2D rb;
    bool falling;
    float fallHeight;
    float impactVelocity = 0;
    protected override void Initialize()
    {
        inputManager = GetComponent<PlayerInputManager>();
        rb = GetComponent<Rigidbody2D>();
        base.Initialize();
    }
    private void FixedUpdate()
    {
        if(!falling && rb.linearVelocityY < 0)
        {
            falling = true;
            fallHeight = transform.position.y;
        }
        if(falling && rb.linearVelocityY >= 0)
        {
            if(fallHeight - transform.position.y >= deathHeight && impactVelocity > impactVelocityThreshold) TriggerDeathEvent();
            falling = false;
            impactVelocity = 0;
        }
        
    }
    private void Awake()
    {
        Initialize();
    }
    public override void TriggerDeathEvent()
    {
        base.TriggerDeathEvent();
        inputManager.DisableInput();
        StartCoroutine("ResetLevel");

    }
    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(2);
        LevelFunctionsLibrary.LevelFunctions.ResetCurrentLevel();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        impactVelocity = collision.relativeVelocity.y;
    }
}
