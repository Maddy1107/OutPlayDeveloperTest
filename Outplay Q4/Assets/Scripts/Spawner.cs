using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnableObject;

    public float minRange;

    public float maxRange;

    private void Start()
    {
        //Spawn 100 spheres
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(minRange, maxRange);
            float y = Random.Range(minRange, maxRange);
            float z = Random.Range(minRange, maxRange);
            Instantiate(spawnableObject, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
