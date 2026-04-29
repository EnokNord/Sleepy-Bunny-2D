using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerHealthComponent : HealthComponent
{
    [SerializeField] float deathHeight = 30;
    [SerializeField] float impactVelocityThreshold = 40;
    [SerializeField] float swimTimeTilDeath = 10;



    PlayerInputManager inputManager;
    Rigidbody2D rb;
    bool falling;
    float fallHeight;
    float impactVelocity = 0;
    float swimTimer = 0;
    bool swimming = false;
    bool inWater = false;
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
        if (swimming)
        {
            Debug.Log(swimTimer);
            swimTimer -= Time.fixedDeltaTime;
            if(swimTimer <= 0)
            {
                TriggerDeathEvent();
            }
            
        }
        if (!inWater && swimTimer != 0 && Global.GlobalFunctionsLibrary.IsGrounded(rb))
        {
             swimming = false;
             swimTimer = 0;
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        BuoyancyEffector2D buoyancy = collision.GetComponent<BuoyancyEffector2D>();
        if (buoyancy)
        {
            swimming = false;
            inWater = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuoyancyEffector2D buoyancy = collision.GetComponent<BuoyancyEffector2D>();
        if (buoyancy)
        {
            Debug.Log("Swimming");
            if(swimTimer == 0) swimTimer = swimTimeTilDeath;
            swimming = true;
            inWater = true;
        }
    }
}
