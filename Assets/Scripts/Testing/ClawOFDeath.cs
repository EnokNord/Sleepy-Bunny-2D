using UnityEngine;
using UnityEngine.Splines;

public class ClawOFDeath : MonoBehaviour
{
    //Players layer
    private const int PlayerLayer = 7;
    
    //The entire claw object
    public GameObject Move;

    public bool HitGround = false;

    bool PlayerDetected = false;

    public float FallSpeed = 3;



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer != PlayerLayer) return;

        PlayerDetected = true;

        //Stops the object from moving on the spline 
        Move.GetComponent<SplineAnimate>().enabled = false;

    }

    private void FixedUpdate()
    {

        if (!PlayerDetected) return;

        // is done in the Ground detection script
        if (HitGround)
        {

            HitGround = false;

            PlayerDetected = false;

            //Get the object to move on the spline again
            Move.GetComponent<SplineAnimate>().enabled = true;
            return;

        }


        //Going down
        Move.transform.Translate(Vector3.down * (FallSpeed * Time.fixedDeltaTime));
    }

}
