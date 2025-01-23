using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgentData", menuName = "Data/AutonomousAgentData")]
public class AutonomousAgentData : ScriptableObject
{
    [SerializeField, Range(0, 180)] float displacement;
    [SerializeField, Range(0, 10)] float distance;
    [SerializeField, Range(0, 10)] float radius;
    [SerializeField, Range(0, 5)] float cohestionWeight;
    [SerializeField, Range(0, 5)] float separationWeight;
    [SerializeField, Range(0, 5)] float alignmentWeight;

    public float Displacement { get { return displacement; } }
    public float Distance { get { return distance; } }
    public float Radius { get { return radius; } }
    public float CohesionWeight { get { return cohestionWeight; } }
    public float SeparationWeight { get { return separationWeight; } }
    public float AlignmentWeight { get { return alignmentWeight; } }
}
