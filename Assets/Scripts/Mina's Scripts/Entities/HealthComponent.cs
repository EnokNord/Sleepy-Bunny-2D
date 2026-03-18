using Events;
using UnityEngine;

public abstract class HealthComponent : MonoBehaviour, IDeathEvent
{
    MovementAnimationController animationController;

    protected virtual void Initialize()
    {
        animationController = GetComponent<MovementAnimationController>();
    }
    public virtual void TriggerDeathEvent()
    {
       animationController.TriggerDeathAnimation();
    }
}
