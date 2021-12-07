using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints Instance;

    public GameObject PointSphere;

    public Vector3 P1;
    public Vector3 P2;
    public Vector3 P3;

    private void Awake() => Instance = this;

    private void Start()
    {
        Instantiate(PointSphere, P1, Quaternion.identity);
        Instantiate(PointSphere, P2, Quaternion.identity);
        Instantiate(PointSphere, P3, Quaternion.identity);
    }

}
