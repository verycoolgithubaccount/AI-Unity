using UnityEngine;

public abstract class AIAgent : MonoBehaviour
{
    [SerializeField] protected Movement movement;

    public Movement Movement { get { return movement; } }
}
