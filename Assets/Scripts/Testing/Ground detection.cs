using UnityEngine;
using UnityEngine.Events;

public class Grounddetection : MonoBehaviour
{

    /*
        Use animations to move in real version
        Use light engine thingy for detection box in real version
    */
    private const int Ground = 3;

    //Used to find the player
    public GameObject Light;

    //The entire claw object
    public GameObject Move;

    
    private float Y;

    private void Start()
    {
        //Finds the hight the object should stay at
        Y = Move.transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.layer != Ground) return;
        


        ClawOFDeath DeathClaw = Light.GetComponent<ClawOFDeath>();

        // sets the ground
        DeathClaw.HitGround = true;

        //Resets hight
        Move.transform.position =  new Vector2(Move.transform.position.x,Y);

    }

}
