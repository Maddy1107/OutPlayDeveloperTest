using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public GameObject PointSphere;

    public GameObject spawnableObject;

    public GameObject Player;

    public List<Vector3> points = new List<Vector3>(3);

    private void Awake() => Instance = this;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 100, 100, 50), "Shuffle Objects"))
        {
            SpawnObjects();
        }

        if (GUI.Button(new Rect(50, 200, 100, 50), "Spawn Player") && !GameObject.FindGameObjectWithTag("Player"))
        {
            SpawnPlayer();
        }
    }

    private void Start()
    {
        SpawnObjects();
        SpawnPoints();
    }

    public void SpawnPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Instantiate(PointSphere, points[i], Quaternion.identity);
        }
    }

    public void SpawnObjects()
    {
        GameObject[] previousSpheres = GameObject.FindGameObjectsWithTag("Sphere");

        foreach (var go in previousSpheres)
        {
            Destroy(go);
        }

        //Spawn 100 spheres
        for (int i = 0; i < 100; i++)
        {
            Instantiate(spawnableObject, Random.insideUnitSphere * 50, Quaternion.identity);
        }
    }

    public void SpawnPlayer()
    {
        Instantiate(Player, new Vector3(0,0,0), Quaternion.identity);
    }
}
