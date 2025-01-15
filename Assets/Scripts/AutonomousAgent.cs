using UnityEngine;

public class AutonomousAgent : AIAgent
{
    [SerializeField] Perception perception;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * perception.GetMaxDistance(), Color.yellow);

        var gameObjects = perception.GetGameObjects();
        foreach (var gObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gObject.transform.position, Color.cyan);
        }
    }
}
