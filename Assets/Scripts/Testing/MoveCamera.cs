using Events;
using Input;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using static Player.Movement.ForceAccumulate;
using static UnityEngine.InputSystem.InputAction;

public class MoveCamera : MonoBehaviour
{
    public PlayerInputController.CameraActions cameraControls => inputControls.Camera;
    PlayerInputController inputControls;

    public float TooFar = 10f;
    public GameObject Player;

    public GameObject Tracker;

    private bool trackerOn = true;
    public float TimeTillStop = 3;
    private float StopTime;

    public float CameraSpeed = 3;

    private void Awake()
    {
        inputControls = new PlayerInputController();
        cameraControls.Enable();
        
     
    }
    void OnDisable()
    {
        cameraControls.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Dist = Player.transform.position - gameObject.transform.position;

        StopTime -= Time.deltaTime;

        if (Dist.magnitude <= TooFar)
        {
            
            

            if (cameraControls.up.IsPressed())
            {
                Tracker.transform.Translate(Vector2.up * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }

            if (cameraControls.Down.IsPressed())
            {
                Tracker.transform.Translate(Vector2.down * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }

            if (cameraControls.right.IsPressed())
            {

                Tracker.transform.Translate(Vector2.right * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }

            if (cameraControls.left.IsPressed())
            {
                Tracker.transform.Translate(Vector2.left * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }
        }

        if(StopTime <= 0)
        {
            trackerOn =true;
        }

        if (trackerOn)
        {
            Tracker.GetComponent<CinemachineCamera>().enabled = true;
        }

        if (!trackerOn)
        {
            Tracker.GetComponent<CinemachineCamera>().enabled = false;
        }

    }
}
