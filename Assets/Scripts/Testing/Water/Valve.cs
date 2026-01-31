using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Valve : MonoBehaviour
{
    public GameObject MoveableWater;

    private const int PlayerLayer = 7;

    public float Moveby;

    public int MoveMuch;
    public float WaitBy;

    private bool move = false;
    private bool Stop = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.layer != PlayerLayer) return;
        move = true;


        StartCoroutine("Water");
    }
    IEnumerator Water()
    {
        if (Stop == false)
            for (int i = 0; i < MoveMuch; i++)
            {
                MoveableWater.transform.position = new Vector2(MoveableWater.transform.position.x, MoveableWater.transform.position.y + Moveby);
                yield return new WaitForSeconds(WaitBy);
            }
        Stop = true;
       yield break;
    }

}
