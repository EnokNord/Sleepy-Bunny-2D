using Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Splines;
[RequireComponent(typeof(Rigidbody2D))]
public class ClawController : MonoBehaviour
{
    //[SerializeField] List<Vector3> travelPoints = new List<Vector3>();
    [SerializeField] float detectionRangeHeight = 40;
    [SerializeField] float detectionWidth = 30;
    [SerializeField] float detectionTime = 2;
    [SerializeField] float attackCooldown = 3;
    [SerializeField] float speed = 20;
    [SerializeField] SplineAnimate splineAnimator;

    [SerializeField] Light2D spotLight;
    [SerializeField] Color neutralColor = Color.yellow;
    [SerializeField] Color searchColor = Color.red;

    Transform playerTransform;
    Vector3 currentTravelPoint;
   
    float detWidthDecimal;
    float detectionTimer = 0;
    float attackCooldownTimer = 0;
    
    bool moveBackUp = true;
    bool foundPlayer = false;
    bool searching = false;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        detWidthDecimal = detectionWidth * 0.01f;
        currentTravelPoint = transform.position;
        PlayerInputManager player = FindAnyObjectByType<PlayerInputManager>();
        if (player == null)
        {
            Debug.LogError("Couldnt find player! object: " + name);
            Destroy(this);
            return;
        }
        playerTransform = player.transform;
    }

    void FixedUpdate()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
            return;
        }
        searching = false;
        // Player detection loop
        if (!foundPlayer && !moveBackUp)
        {
            Vector2 dist =  playerTransform.position - transform.position;
            if (dist.magnitude <= detectionRangeHeight && dist.normalized.x < detWidthDecimal && dist.normalized.x > -detWidthDecimal)
            {
                Debug.DrawRay(transform.position, dist.normalized);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dist.normalized, detectionRangeHeight);
                if (hit && hit.transform == playerTransform)
                {
                    detectionTimer += Time.fixedDeltaTime;
                    searching = true;
                    if (splineAnimator.enabled)
                    {
                        spotLight.color = searchColor;
                        splineAnimator.enabled = false;
                    }
                    if (detectionTimer >= detectionTime)
                    {
                        rb.simulated = true;
                        foundPlayer = true;
                        currentTravelPoint = transform.position;
                    }
                }
            }
          
        }
        if (!searching && !moveBackUp && !foundPlayer)
        {
            detectionTimer -= Time.fixedDeltaTime;
            if(detectionTimer < 0)
            {
                detectionTimer = 0;
                if (!splineAnimator.enabled)
                {
                    spotLight.color = neutralColor;
                    splineAnimator.enabled = true;
                }
            }
            
        }
        //Handle Movement
        if (moveBackUp)
        {
            Vector3 dist = (currentTravelPoint - transform.position);
            if (dist.magnitude > 1)
            {
                transform.Translate(dist.normalized * speed * Time.fixedDeltaTime);
            }
            else
            {
                spotLight.color = neutralColor;
                splineAnimator.enabled = true;
                moveBackUp = false;
                attackCooldownTimer = attackCooldown;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.simulated = false;
        
        IDeathEvent killTarget = collision.gameObject.GetComponent<IDeathEvent>();
        if (killTarget != null)
        {
            killTarget.TriggerDeathEvent();
            //TODO: Play Animation
        }
        else
        {
            detectionTimer = 0;
            foundPlayer = false;
            moveBackUp = true;
        }
    }
}
