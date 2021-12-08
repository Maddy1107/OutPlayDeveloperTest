using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    private Vector3 offset;

    public Button SpawnPlayerButton;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        //Get the distance between the camera and player
        offset = transform.position - target.transform.position;

    }

    private void Update()
    {
        if (target)
        {
            SpawnPlayerButton.interactable = false;
        }
        else
        {
            SpawnPlayerButton.interactable = true;
        }
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