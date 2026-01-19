using Unity.Cinemachine;
using UnityEngine;


public class BecomeCamera : MonoBehaviour
{
    // Checks if it is the main camrea or not
    private bool currentCamera = false;
    private bool secondHit = false; //due to the players hit boxes the game regirsters players hiting this twice. Need a fix for final version

    // The Layer the payer is on. Used for a hit check
    private const int PlayerLayer = 7;


    //The cameras use 2D coldiers that cover their enteir space to check if the player should switch to the camrea or not.
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer != PlayerLayer) return;

        if (!currentCamera)
        {
            //Blocks a dubble hit bug
            if (!secondHit)
            {
                secondHit = true;
                return;
            }
            //Sets the camera as the main camera
            gameObject.GetComponent<CinemachineCamera>().Priority = 1;
            currentCamera = true;
            secondHit = false;
        }
    }

    //When the player leaves the cameras zone, it will relinquish the camrea roll to the players camera
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != PlayerLayer) return;

        //Removes the camrea from the main camrea
        gameObject.GetComponent<CinemachineCamera>().Priority = -1;
        currentCamera = false;
        secondHit = false;
    }
}
