using Unity.VisualScripting;
using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    [Multiline] public string description;
    [SerializeField] protected string tagName;
    [SerializeField] protected float maxDistance;
    [SerializeField] protected float maxAngle;
    [SerializeField] protected LayerMask layerMask = Physics.AllLayers;

    public abstract GameObject[] GetGameObjects();

    public float GetMaxDistance() { return maxDistance; }
    public float GetMaxAngle() { return maxAngle; }

    public bool CheckDirection(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, transform.rotation * direction);
        return Physics.Raycast(ray, maxDistance, layerMask);
    }

}
