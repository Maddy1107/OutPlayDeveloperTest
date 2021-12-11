using System.Collections.Generic;
using UnityEngine;

//Class used for initializing racecars and testing the difference
public class Manager : MonoBehaviour
{
    int numofRacer = 1000;

    [SerializeField]BaseClass revised;

    [SerializeField]BaseClass given;

    BaseClass current;

    float difference;

    GUIStyle style = new GUIStyle();

    List<Racer> racers = new List<Racer>();

    void OnGUI()
    {
        style.fontSize = 50;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(new Rect(130, 200, 2000, 40), "Execution Speed: " + difference, style);

        if (GUI.Button(new Rect(330, 300, 350, 80), "Current Solution:" + current.ToString()))
        {
            if (current == given)
            {
                current = revised;
            }
            else
            {
                current = given;
            }
        }
    }

    void Start()
    {
        current = given;
        generateRacers();
    }

    void generateRacers()
    {
        while (racers.Count != numofRacer)
        {
            racers.Add(new Racer());
        }
    }

    void Update()
    {
        float before = Time.realtimeSinceStartup;
        current.UpdateRacers(Time.deltaTime, racers);
        float after = Time.realtimeSinceStartup;
        difference = after - before;
        generateRacers();
    }
}
