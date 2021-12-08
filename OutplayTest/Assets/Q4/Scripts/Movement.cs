using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;//movement speed

    private int currentPointIndex = 0;//current point the player is moving towards

    bool reachedFinal = false;//check if reached the last point

    bool effectPlayed = false;

    void Update()
    {
        //check if reached the last point or else keep moving
        if(currentPointIndex != Spawner.Instance.points.Count)
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
        if (transform.position != Spawner.Instance.points[currentPointIndex])
        {
            transform.position = Vector3.MoveTowards(transform.position, Spawner.Instance.points[currentPointIndex], speed * Time.deltaTime);
        }
        else
        {
            currentPointIndex += 1;
        }
    }
}
