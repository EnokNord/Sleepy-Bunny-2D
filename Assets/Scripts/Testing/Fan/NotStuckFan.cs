using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NotStuckFan : MonoBehaviour
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }
    // The Layer the payer is on. Used for a hit check
    private const int PlayerLayer = 7;

    private GameObject Pushing;

    [SerializeField] private Directions fanDirection;
    [SerializeField] private float fanPower;
    [SerializeField] private float fanRange;
    [SerializeField] private bool onOff = true;
    //public bool OnOff { get; private set; }

    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;
    
    public bool OnOff = true;

    private Rigidbody2D PlayerRigidbody;

    private void Start()
    {
        OnOff = onOff;

    }
    //brakes the code
    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer != PlayerLayer) return;
        PlayerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != PlayerLayer) return;
        PlayerRigidbody = null;
    }
    */
    /*
    private void FixedUpdate()
    {

        if (!OnOff) return;
       // if (PlayerRigidbody is null) return;

        switch (fanDirection)
        {
            case Directions.Up:
                if (Physics2D.Raycast(transform.position, Vector2.up, fanRange, LayerMask.GetMask("Player")))
                {
                    PlayerRigidbody.AddForce(transform.up * fanPower);
                }
                else if (Physics2D.Raycast(transform.position, Vector2.up, fanRange, GameObject.name("")))
                {

                }
                Debug.DrawRay(transform.position, Vector2.up * fanRange, Color.green);
                break;
            case Directions.Down:
                if (Physics2D.Raycast(transform.position, vector2.up, fanrange (hit && hit.name.contains("name"))
                {
                    PlayerRigidbody.AddForce(-transform.up * fanPower);
                }
                Debug.DrawRay(transform.position, Vector2.down * fanRange, Color.green);

                break;
            case Directions.Left:
                if (Physics2D.Raycast(transform.position, Vector2.left, fanRange, LayerMask.GetMask("Player")))
                {
                    PlayerRigidbody.AddForce(-transform.right * fanPower);
                }
                Debug.DrawRay(transform.position, Vector2.left * fanRange, Color.green);

                break;
            case Directions.Right:
                if (Physics2D.Raycast(transform.position, Vector2.right, fanRange, LayerMask.GetMask("Player")))
                {
                    PlayerRigidbody.AddForce(transform.right * fanPower);
                }
                Debug.DrawRay(transform.position, Vector2.right * fanRange, Color.green);

                break;
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer != PlayerLayer && other.gameObject.name != "box_small") return;

        PlayerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();

        Debug.Log(PlayerRigidbody);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != PlayerLayer && other.gameObject.name != "box_small") return;

        PlayerRigidbody = null; 
    }

    private void FixedUpdate()
    {
        if (PlayerRigidbody == null) return;

        switch (fanDirection)
        {
            case Directions.Up:
                PlayerRigidbody.AddForce(transform.up * fanPower);
                
                break;

            case Directions.Down:
                PlayerRigidbody.AddForce(-transform.up * fanPower);
                break;

            case Directions.Left:
                PlayerRigidbody.AddForce(-transform.right * fanPower);
                break;

            case Directions.Right:
                PlayerRigidbody.AddForce(transform.right * fanPower);
                break;
        }

    }

    //When press button call this method 
    public void setFanOnOff(bool value)
    {
        OnOff = value;
    }
}
