using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/MovementData")]
public class MovementData : ScriptableObject
{
    [SerializeField, Range(1, 20)] protected float maxSpeed = 5;
    [SerializeField, Range(1, 20)] protected float minSpeed = 5;
    [SerializeField, Range(1, 20)] protected float maxForce = 5;

    public float MaxSpeed { get { return maxSpeed; } }
    public float MinSpeed { get { return minSpeed; } }
    public float MaxForce { get { return maxForce; } }
}
