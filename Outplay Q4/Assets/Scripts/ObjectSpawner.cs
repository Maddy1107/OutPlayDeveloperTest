using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnableObject;

    [Header("Set X,Y,Z ranges")]
    public float minXZRange;

    public float maxXZRange;

    public float minYRange;

    public float maxYRange;

    private void Start()
    {
        //Spawn 100 spheres
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(minXZRange, maxXZRange);
            float y = Random.Range(minYRange, maxYRange);
            float z = Random.Range(minXZRange, maxXZRange);
            Instantiate(spawnableObject, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
