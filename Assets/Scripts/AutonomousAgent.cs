using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class AutonomousAgent : AIAgent
{
    [SerializeField] AutonomousAgentData data;

    [Header("Perception")]
    [SerializeField] Perception seekPerception;
    [SerializeField] Perception fleePerception;
    [SerializeField] Perception flockPerception;
    [SerializeField] Perception obstaclePerception;
    [SerializeField] float height = 0.5f;

    float angle;

    private void Update()
    {
        int distance = 50;
        transform.position = Utilities.Wrap(transform.position, new Vector3(-distance, height, -distance), new Vector3(distance, height, distance));

        // SEEK
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                // Apply force to move towards the target (first game object)
                movement.ApplyForce(Seek(gameObjects[0]));
            }
        }

        // FLEE
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                // Apply force to move towards the target (first game object)
                movement.ApplyForce(Flee(gameObjects[0]));
            }
        }

        // FLOCK
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects) * data.CohesionWeight);
                movement.ApplyForce(Separation(gameObjects, data.Radius) * data.SeparationWeight);
                movement.ApplyForce(Alignment(gameObjects) * data.AlignmentWeight);
            }
        }

        // OBSTACLE
        if (obstaclePerception != null)
        {
            if (obstaclePerception.CheckDirection(Vector3.forward))
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 3, Color.red, 0.5f);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 3, Color.green, 0.5f);
            }
        }

        // WANDER
        if (movement.Acceleration.sqrMagnitude == 0)
        {
            movement.ApplyForce(Wander());
        }

        if (movement.Direction.sqrMagnitude != 0) 
        {
            transform.rotation = Quaternion.LookRotation(movement.Direction);
        }

        
    }

    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }

        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;

        return GetSteeringForce(direction);
    }

    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            Vector3 direction = transform.position - neighbor.transform.position;
            float distance = direction.magnitude;
            if (distance < radius)
            {
                separation += direction / (distance * distance);
            }
        }

        return GetSteeringForce(separation);
    }

    private Vector3 Alignment(GameObject[] neighbors)
    {
        Vector3 velocities = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            velocities += neighbor.GetComponent<AIAgent>().Movement.Velocity;
        }

        Vector3 averageVelocity = velocities / neighbors.Length;
        return GetSteeringForce(averageVelocity);
    }

    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        return GetSteeringForce(direction);
    }

    private Vector3 Flee(GameObject target)
    {
        Vector3 direction = transform.position - target.transform.position;
        return GetSteeringForce(direction);
    }

    private Vector3 Wander()
    {
        angle += Random.Range(-data.Displacement, data.Displacement);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * data.Radius);
        Vector3 forward = movement.Direction * data.Distance;
        return GetSteeringForce(forward + point);
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.data.MaxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.data.MaxForce);

        return force;
    }
}
