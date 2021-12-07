using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;

    private void Start()
    {
        //Get the distance between the camera and player
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        if(target)
        {
            Vector3 newpos = target.transform.position + offset;
            transform.position = newpos;
        }
    }
}