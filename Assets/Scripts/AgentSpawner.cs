using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] AIAgent[] agents;
    [SerializeField] LayerMask layerMask;
    [SerializeField, Range(0,5)] float randomMax;

    int index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) index = ++index % agents.Length;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
            {
                Vector3 randomLocation = hitInfo.point + Random.onUnitSphere * randomMax * Random.value;

                Vector3 position = new Vector3(randomLocation.x, gameObject.transform.position.y, randomLocation.z);
                Instantiate(agents[index], position, Quaternion.identity);
            }
        }
    }
}
