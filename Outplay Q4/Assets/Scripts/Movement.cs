using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;//movement speed
    public List<Vector3> points;//All the points

    private int currentPointIndex = 2;//current point the player is moving towards

    bool reachedFinal;//check if reached the last point

    bool effectPlayed = false;

    private void Start()
    {
        //Adding all points to the list
        points = new List<Vector3>()
        {
            SpawnPoints.Instance.P1,
            SpawnPoints.Instance.P2,
            SpawnPoints.Instance.P3,
        };
    }

    void Update()
    {
        //check if reached teh last point or else keep moving
        if(currentPointIndex != points.Count && !reachedFinal)
        {
            MovePlayer();
        }
        else 
        {
            reachedFinal = true;
        }

        //if reached final point play effects
        if(reachedFinal && !effectPlayed)
        {
            EffectPlayer.instance.PlayEffects(transform.position);
            effectPlayed = true;
        }
    }

    public void MovePlayer()
    {
        //if current point is crossed go next point
        if (transform.position != points[currentPointIndex])
        {
            transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex], speed * Time.deltaTime);
        }
        else
        {
            currentPointIndex += 1;
        }
    }
}
