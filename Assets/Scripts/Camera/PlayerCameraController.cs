using System;
using Unity.Cinemachine;
using UnityEngine;
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] GameObject focusPoint;
    [SerializeField] float distanceFromPlayerAllowed = 10.0f;
    [SerializeField] float lookArountSpeed = 20.0f;

    float horizontalMoveDir = 0;
    float verticalMoveDir = 0;
    private void Awake()
    {
        if (focusPoint == null)
        {
            Debug.LogError("Missing focuspoint for " + gameObject.name);
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (horizontalMoveDir == 0 && verticalMoveDir == 0)
        {
            Vector3 dir = -focusPoint.transform.localPosition.normalized;
            focusPoint.transform.Translate(dir * Time.fixedDeltaTime * lookArountSpeed * 0.5f);
        }
        else
        {
            focusPoint.transform.Translate(new Vector3(1 * horizontalMoveDir, 1 * verticalMoveDir, 0) * Time.fixedDeltaTime * lookArountSpeed);
        }
        if(focusPoint.transform.localPosition.magnitude > distanceFromPlayerAllowed)
        {
            focusPoint.transform.localPosition = focusPoint.transform.localPosition.normalized * distanceFromPlayerAllowed;
        }
    }
    public void ResetFocusPoint()
    { 
        focusPoint.transform.localPosition = Vector3.zero; 
        horizontalMoveDir = 0; 
        verticalMoveDir = 0; 
    }
    public void SetCameraHorizontalDir(float dir) { horizontalMoveDir = Mathf.Clamp(dir, -1, 1); }
    public void SetCameraVerticalDir(float dir) { verticalMoveDir = Mathf.Clamp(dir, -1, 1); }
    
}
