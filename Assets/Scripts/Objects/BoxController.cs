using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{
    // [SerializeField] float allowedRotationDegrees = 45f;
    [SerializeField] LayerMask boxLayer;
    BoxController stackedBox;
    public bool BottomBox { get;  set; }
    public void OnGrab()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 2, 1<<6);
        if (hit)
        {
            stackedBox = hit.rigidbody.gameObject.GetComponent<BoxController>();
            if (stackedBox == null) return;
            stackedBox.transform.parent = transform.parent;
            stackedBox.GetComponent<BoxController>().OnGrab();
        }
    }
    public void OnRelease()
    {
        BottomBox = false;
        transform.parent = null;
        if(stackedBox) stackedBox.OnRelease();  
    }
    private void FixedUpdate()
    {
       if(transform.parent != null && !BottomBox)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2, 1 << 6);
            if (hit) {}
            else
            {
                OnRelease();
            }
        }
    }
}
