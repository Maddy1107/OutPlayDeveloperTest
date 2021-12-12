using System;
using UnityEngine;
using static JewelProperties;

public abstract class Board : MonoBehaviour
{
    public int numOfRows;

    public int numOfCols;

    public JewelKind[,] MainBoard;

    //Essential for level Design that is if wanted a jewel not to be used in the certain level.
    //Can set the array to the number of jewels needed and set the colours and then set the Main Board to select random colour from the list.

    //[SerializeField]JewelKind[] JewelsneededForLevel; 

    public int GetWidth()
    {
        return numOfCols;
    }
    public int GetHeight()
    {
        return numOfRows;
    }

    public JewelKind GetJewel(int x, int y)
    {
        return MainBoard[x, y];
    }
    public void SetJewel(int x, int y, JewelKind kind)
    {
        MainBoard[x, y] = kind;
    }

    //Assign jewels to board
    public void assignJewelstoBoard()
    {
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            //For the demo purpose here we are just selecting a random enum value which is not an ideal way.
            MainBoard[x, y] = (JewelKind)UnityEngine.Random.Range(1, Enum.GetNames(typeof(JewelKind)).Length);
        }
    }
}
