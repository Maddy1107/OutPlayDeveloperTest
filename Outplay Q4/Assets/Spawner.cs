using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnableObject;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(-100, 100);
            float y = Random.Range(-10, 10);
            float z = Random.Range(-100, 100);
            Instantiate(spawnableObject, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
