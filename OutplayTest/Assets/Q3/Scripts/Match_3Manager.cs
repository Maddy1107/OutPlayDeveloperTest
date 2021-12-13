using UnityEngine;

public class Match_3Manager : MonoBehaviour
{
    public void Start()
    {
        BoardOperations.instance.shuffle();
    }

}
