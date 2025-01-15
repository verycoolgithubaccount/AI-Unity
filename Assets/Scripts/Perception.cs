using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    [SerializeField] protected string tagName;
    [SerializeField] protected float maxDistance;
    [SerializeField] protected float maxAngle;

    public abstract GameObject[] GetGameObjects();

    public float GetMaxDistance() { return maxDistance; }
    public float GetMaxAngle() { return maxAngle; }

}
