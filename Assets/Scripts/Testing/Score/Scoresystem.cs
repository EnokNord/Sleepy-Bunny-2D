using UnityEngine;
using UnityEngine.Events;

public class Scoresystem : MonoBehaviour
{

    // this code is placed on the player, and is used to tigger other code
    // "this" Seems not to be working
    // Current bug, w3 l1 (only) the text isn't being updated correctly

    public int SmallPickups {  get; private set; }

    public int BigPickups { get; private set; }

    public UnityEvent<Scoresystem> OnBigCollection;

    public UnityEvent<Scoresystem> OnSmallCollection;

    public void SmallCollected()
    {
        SmallPickups++;
        OnSmallCollection.Invoke(this);
    }

    public void BigCollected()
    {
        BigPickups++;
        OnBigCollection.Invoke(this);
    }

}
