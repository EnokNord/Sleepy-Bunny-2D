using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.XR;

public class AdoptSPline : MonoBehaviour
{
    GameObject Spline1;
    private void Awake()
    {

        Spline1 = GameObject.Find("Spline1");
        GetComponent<SplineAnimate>().Container = Spline1.GetComponent<SplineContainer>();

        GetComponent<SplineAnimate>().Play();
    }

}
