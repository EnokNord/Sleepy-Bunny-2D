using UnityEngine;

public class WaterController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthComponent healthComponent = collision.GetComponent<PlayerHealthComponent>();
        if (healthComponent && collision == healthComponent.DrownCollider) healthComponent.EnterWater();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealthComponent healthComponent = collision.GetComponent<PlayerHealthComponent>();
        if (healthComponent && collision == healthComponent.DrownCollider) healthComponent.LeaveWater();
    }
}
