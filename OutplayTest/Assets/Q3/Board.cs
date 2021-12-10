using UnityEngine;
using System;

public class Board: MonoBehaviour
{
    public enum JewelKind
    {
        Empty,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    };

    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    };

    struct Move
    {
        int x;
        int y;
        MoveDirection direction;
    };

    public Texture RedJewel;
    public Texture OrangeJewel;
    public Texture YellowJewel;
    public Texture GreenJewel;
    public Texture BlueJewel;
    public Texture IndigoJewel;
    public Texture VioletJewel;

    JewelKind[,] MainBoard;

    JewelKind[,] OrigBoardbackup;

    //Essential for level Design that is if wanted a jewel not to be used in the certain level.
    //Can set the array to the number of jewels needed and set the colours and then set the Main Board to select random colour from the list.
 
    //[SerializeField]JewelKind[] JewelsneededForLevel; 

    public int numOfRows;

    public int numOfCols;

    const int jewelGridSize = 50;

    int GetWidth()
    {
        return numOfCols;
    }
    int GetHeight()
    {
        return numOfRows;
    }

    JewelKind GetJewel(int x, int y)
    {
        return MainBoard[x,y];
    }
    void SetJewel(int x, int y, JewelKind kind)
    {
        MainBoard[x,y] = kind;
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(80, Screen.height - 350, 100, 50), "Shuffle"))
        {
            shuffle();
        }
        if (GUI.Button(new Rect(80, Screen.height - 250, 100, 50), "Check Match"))
        {
            CheckforAnyMatchonBoard();
        }

        //Visualize the board
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            GUI.DrawTexture(new Rect(jewelGridSize * x + Screen.width / 4, jewelGridSize * (numOfCols - y), jewelGridSize, jewelGridSize), getTexture(MainBoard[x,y]));
        }
    }

    //Note: To make use of the enum JewelKind, a function is written to get the textures using enum.
    //Texture to visualize can be also taken randomly from a list of Textures.
    Texture getTexture(JewelKind kind)
    {
        switch (kind)
        {
            case JewelKind.Red:
                return RedJewel;
            case JewelKind.Orange:
                return OrangeJewel;
            case JewelKind.Yellow:
                return YellowJewel;
            case JewelKind.Green:
                return GreenJewel;
            case JewelKind.Blue:
                return BlueJewel;
            case JewelKind.Indigo:
                return IndigoJewel;
            case JewelKind.Violet:
                return VioletJewel;
            default:
                return Texture2D.whiteTexture;
        }
    }

    private void Start()
    {
        MainBoard = new JewelKind[numOfRows,numOfCols];
        shuffle();
    }

    //Shuffle the board
    public void shuffle()
    {
        assignJewelstoBoard();

        while (CheckforAnyMatchonBoard())
        {
            assignJewelstoBoard();
        }

        OrigBoardbackup = MainBoard;
    }

    //Asign the colours to the board
    public void assignJewelstoBoard()
    {
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            //For the demo purpose here we are just selecting a random enum value which is not an ideal way.
            MainBoard[x,y] = (JewelKind)UnityEngine.Random.Range(1, Enum.GetNames(typeof(JewelKind)).Length);
        }
    }

    //To check if the board is created without any matches
    bool CheckforAnyMatchonBoard()
    {
        //Checking only right and up to avoid repeat checking
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            Debug.Log(x.ToString() + y.ToString());
            //Debug.Log(CheckforMatch(x, y, MoveDirection.Right));

            if (CheckforMatch(x, y, MoveDirection.Right) >= 3)
            {
                return true;
            }

            /*if (CheckforMatch(x, y, MoveDirection.Up) >= 3)
            {
                return true;
            }*/

        }
        return false;
    }

    //Check for match in the given direction
    int CheckforMatch(int x, int y, MoveDirection direction)
    {
        bool gotMatch = true;
        JewelKind currentJewel = GetJewel(x, y);
        int currentIndex = 0;
        int MatchCount = 0;

        switch(direction)
        {
            case MoveDirection.Left:
                while(gotMatch)
                {
                    int currentJewelIndex = x - currentIndex;
                    currentIndex++;
                    if(currentIndex >= 0)
                    {
                        if(currentJewel == GetJewel(currentJewelIndex, y))
                        {
                            MatchCount++;
                        }
                        else
                        {
                            gotMatch = false;
                        }
                    }
                    else
                    {
                        gotMatch = false;
                    }
                }
                return MatchCount;

            case MoveDirection.Right:
                while (gotMatch)
                {
                    int currentJewelIndex = x + currentIndex;
                    currentIndex++;
                    if (currentIndex <= GetWidth() - 1)
                    {
                        if (currentJewel == GetJewel(currentJewelIndex, y))
                        {
                            MatchCount++;
                        }
                        else
                        {
                            gotMatch = false;
                        }
                        //Debug.Log(currentJewel);

                    }
                    else
                    {
                        gotMatch = false;
                    }
                }
                return MatchCount;
            case MoveDirection.Up:
                while (gotMatch)
                {
                    int currentJewelIndex = y + currentIndex;
                    currentIndex++;
                    if (currentIndex <= GetHeight() - 1)
                    {
                        if (currentJewel == GetJewel(x, currentJewelIndex))
                        {
                            MatchCount++;
                        }
                        else
                        {
                            gotMatch = false;
                        }
                    }
                    else
                    {
                        gotMatch = false;
                    }
                }
                return MatchCount;
            case MoveDirection.Down:
                while (gotMatch)
                {
                    int currentJewelIndex = x - currentIndex;
                    currentIndex++;
                    if (currentIndex >= 0)
                    {
                        if (currentJewel == GetJewel(x, currentJewelIndex))
                        {
                            MatchCount++;
                        }
                        else
                        {
                            gotMatch = false;
                        }
                    }
                    else
                    {
                        gotMatch = false;
                    }
                }
                return MatchCount;
            default:
                return MatchCount;
        }
    }


    //Implement this function
    //Public Move CalculateBestMoveForBoard();
};

