using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPerception : Perception
{
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        // get all the colliders inside the sphere
        var colliders = Physics.OverlapSphere(transform.position, maxDistance);
        foreach (var collider in colliders)
        {
            // don't include self
            if (collider.gameObject == gameObject) continue;
            if (tagName == "" || collider.tag == tagName)
            {
                // check if within max range
                Vector3 direction = collider.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                if (angle <= maxAngle)
                {
                    result.Add(collider.gameObject);
                }
            }
        }

        return result.ToArray();
    }
}
