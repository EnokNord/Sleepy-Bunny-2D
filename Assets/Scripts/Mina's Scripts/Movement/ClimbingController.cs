using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    GameObject climbableObject;
    public void TryClimb(float climbDir)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Climbable") 
        {
            climbableObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Climbable")
        {
            climbableObject = null;
        }
    }
}
