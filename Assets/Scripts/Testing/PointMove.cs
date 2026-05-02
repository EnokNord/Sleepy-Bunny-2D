using UnityEngine;
using UnityEngine.Splines;

public class PointMove : MonoBehaviour
{
    private const int PlayerLayer = 7;

    public GameObject Carrot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != PlayerLayer) return;
        Carrot.GetComponent<SplineAnimate>().enabled = true;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != PlayerLayer) return;

        Carrot.GetComponent<SplineAnimate>().enabled = false;
    }
}
