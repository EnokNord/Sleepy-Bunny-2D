using Events;
using Input;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Rendering;
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
        StopTime = 0;
     
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
        Vector2 Dist = gameObject.transform.position - Player.transform.position;
        bool bellow= false;
        if (gameObject.transform.position.y < Player.transform.position.y)
        {
            bellow = true;
        }
       
        //down is too short
        if (Dist.magnitude <= TooFar)
        {
            
            if (cameraControls.up.IsPressed())
            {
                gameObject.transform.Translate(Vector2.up * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }

            if (cameraControls.Down.IsPressed())
            {
                gameObject.transform.Translate(Vector2.down * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }

            if (cameraControls.right.IsPressed())
            {

                gameObject.transform.Translate(Vector2.right * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }

            if (cameraControls.left.IsPressed())
            {
                gameObject.transform.Translate(Vector2.left * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }
            return;
        }

        
        //strukturen är janky hitta en annan lösning
        //Slå ihop höger vänser & up och ner använd position
        //clampa ner till rätt magnitude (flytta cameran tillbaka rätt distanse, istället för att blocka inputs)
        if (cameraControls.up.IsPressed())
        {
            if (gameObject.transform.position.y <= Player.transform.position.y || gameObject.transform.position.y - Player.transform.position.y < 5f)
            {
                gameObject.transform.Translate(Vector2.up * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }  
        }

        if (cameraControls.Down.IsPressed())
        {
            if (gameObject.transform.position.y >= Player.transform.position.y || gameObject.transform.position.y + Player.transform.position.y < 5f)
            {
                gameObject.transform.Translate(Vector2.down * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }      
        }

        if (cameraControls.right.IsPressed())
        {
            if (gameObject.transform.position.x <= Player.transform.position.x || gameObject.transform.position.x - Player.transform.position.x < 5f)
            {
                gameObject.transform.Translate(Vector2.right * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }     
        }

        if (cameraControls.left.IsPressed())
        {
            if (gameObject.transform.position.x >= Player.transform.position.x || gameObject.transform.position.x + Player.transform.position.x < 5f)
            {
                gameObject.transform.Translate(Vector2.left * Time.deltaTime * CameraSpeed);
                trackerOn = false;
                StopTime = TimeTillStop;
            }     
        }

    }

    private void FixedUpdate()
    {
        StopTime -= Time.deltaTime;

        if (StopTime <= 0)
        {
            trackerOn = true;
        }
        if (!trackerOn) return;


        float step = CameraSpeed * 0.5f * Time.deltaTime;
        transform.position = Vector2.MoveTowards(Player.transform.position, Player.transform.position, step);
    }
}
