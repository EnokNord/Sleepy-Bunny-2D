using System.Threading;
using Unity.Cinemachine;
using UnityEngine;


public class BecomeCamera : MonoBehaviour
{
    // Checks if it is the main camrea or not
    private bool currentCamera = false;
    private bool secondHit = false; //due to the players hit boxes the game regirsters players hiting this twice. Need a fix for final version

    private bool TimerStart;
    private bool TimerDone;

    private int FramesToWait = 69;
    private int CurrentFrames;

    // The Layer the payer is on. Used for a hit check
    private const int PlayerLayer = 7;

    private GameObject Top;
    private GameObject Bottom;

    private  Vector3 TopPos;
    private Vector3 BottomPos;


    private void FixedUpdate()
    {
        if (TimerStart)
        {
            CurrentFrames--;
            if (CurrentFrames == 0)
                TimerDone = true;
        }

        if (TimerDone)
        {
            //Add timer

            //Finds top and bottom of screen
            Vector3 TipDimensions = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, 0));
            Vector3 BottomDimensions = Camera.main.ScreenToWorldPoint(Vector3.zero);

            Top.transform.position = TipDimensions;
            Bottom.transform.position = BottomDimensions;

            TimerDone = false;
        }
    }
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

            Top = GameObject.Find("CamraTop");
            Bottom = GameObject.Find("CamraBottom");

            TopPos = Top.transform.position;
            BottomPos = Bottom.transform.position;

            CurrentFrames = FramesToWait;
            
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

        Top.transform.position = TopPos;
        Bottom.transform.position = BottomPos;
    }
}
