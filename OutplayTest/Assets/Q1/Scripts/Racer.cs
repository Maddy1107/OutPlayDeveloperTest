using UnityEngine;

//Racer class to avoid errors and create demo having Random Calculation.
internal sealed class Racer
{
    public void update(float v)
    {
        int i = 0;
        while (i < 50)
        {
            i++;
        }
    }

    public bool IsAlive()
    {
        return Random.Range(0, 5) > 2;
    }

    public bool IsCollidable()
    {
        return Random.Range(0, 5) > 2;
    }

    public bool CollidesWith(Racer racer2)
    {
        return Random.Range(0, 5) > 2;
    }

    public void Destroy()
    {
        int i = 0;
        while(i < 50)
        {
            i++;
        }
    }
}
