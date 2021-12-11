using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class RevisedSolution : BaseClass
{
    public int numOfRacers;

    public override void UpdateRacers(float deltaTimeS, List<Racer> racers)
    {
        numOfRacers = racers.Count;

        for (int racerIndex = 0; racerIndex < numOfRacers; racerIndex++)
        {
            Racer racer1 = racers[racerIndex];
            if (racer1.IsAlive())
            {
                //Racer update takes milliseconds
                racer1.update(deltaTimeS * 1000.0f);

                for (int racerIndex2 = 0; racerIndex2 < numOfRacers; racerIndex2++)
                {
                    Racer racer2 = racers[racerIndex2];
                    if (racerIndex != racerIndex2)
                    {
                        if (racer1.IsCollidable() && racer2.IsCollidable() && racer1.CollidesWith(racer2))
                        {
                            //Explode
                            OnRacerExplodes(racer1);

                            //According to the given solution, it is not clear if I should remove both cars that collided or the only car that exploded.
                            //Because OnRacerExplode is applied only for Racer1, but Racer and Racer2 both has been sent to racersneedingremoved list.
                            //On the other hand in the comment section it is written getting rid of the exploded cars.
                            //So it is quite unclear to me.
                            //Therefore, I am removing here both the cars from the list.
                            //if only exploded cars were to be removed we just remove the statement racers.Remove(racer2);


                            //Remove the cars from the list
                            racers.Remove(racer1);
                            racers.Remove(racer2);

                            //Decrease the numbers
                            numOfRacers -= 2;
                            break;
                        }
                    }
                }
            }
        }
    }
}
