using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Valve : MonoBehaviour
{
    public GameObject MoveableWater;

    private const int PlayerLayer = 7;

    public float Moveby;

    public float MoveMuch;
    public float WaitBy;
    private float targetHight;

    //private bool move = false;
    private bool Stop = true;

    private void Awake()
    {
        targetHight = MoveableWater.transform.position.y + MoveMuch;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.layer != PlayerLayer) return;
        //move = true;

            Stop = false;
        
    }

    private void Update()
    {

        if (Stop) return;

        if(MoveableWater.transform.position.y < targetHight)
            MoveableWater.transform.position = new Vector2(MoveableWater.transform.position.x, MoveableWater.transform.position.y + Moveby * Time.deltaTime);           
        
    }

}
