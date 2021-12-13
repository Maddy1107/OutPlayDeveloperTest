using System.Collections.Generic;
using UnityEngine;
using static JewelProperties;

//Note: This is done in a very bruteforce method because of the specification given in the question.
//Here we could make a different struct for Jewels x and y.
//Also make another struct to store the result like the move, score.
//But it is not understandable here if I am allowed to do that or not.
//Therefore used whatever is given in the specifications.

public class BoardOperations : Board
{
    #region Variables
    public static BoardOperations instance;

    public JewelKind[,] OrigBoardbackup;

    public List<Move> AllMatches = new List<Move>();
    public List<int> MatchCount = new List<int>();

    public List<JewelCoords> bestMoveJewels = new List<JewelCoords>();

    //As said in the above note, here except of making so many lists we can just make one list of type jewel Data and one list of
    //the move result that we get.

    #endregion

    private void Awake() => instance = this;

    private void Start()
    {
        MainBoard = new JewelKind[numOfRows, numOfCols];
    }

    #region Shuffle

    //Shuffle the board
    public void shuffle()
    {
        assignJewelstoBoard();

        while (CheckforAnyMatchonBoard())
        {
            assignJewelstoBoard();
        }

        AllMatches.Clear();
        MatchCount.Clear();

        OrigBoardbackup = (JewelKind[,])MainBoard.Clone();
    }
    #endregion

    #region Check Whole Board for match

    //To check if the board is created without any matches
    bool CheckforAnyMatchonBoard()
    {
        //Checking only right and up to avoid repeat checking
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            List<JewelCoords> RightDir = CheckforMatch(x, y, MoveDirection.Right);
            if (RightDir.Count >= 3)
            {
                return true;
            }

            List<JewelCoords> UpDir = CheckforMatch(x, y, MoveDirection.Up);
            if (UpDir.Count >= 3)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Check whole board for match after a certain swap
    void checkallMatch(Move m)
    {
        //Checking only right and up to avoid repeat checking
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            List<JewelCoords> RightDir = CheckforMatch(x, y, MoveDirection.Right);
            if (RightDir.Count >= 3)
            {
                if (!MatchCount.Contains(RightDir.Count))
                {
                    MatchCount.Add(RightDir.Count);
                    AllMatches.Add(m);
                }
            }

            List<JewelCoords> UpDir = CheckforMatch(x, y, MoveDirection.Up);
            if (UpDir.Count >= 3)
            {
                if (!MatchCount.Contains(UpDir.Count))
                {
                    MatchCount.Add(UpDir.Count);
                    AllMatches.Add(m);
                }
            }
        }
    }
    #endregion

    #region Check for match in a particular Direction

    //Check for match in the given direction and return the count
    List<JewelCoords> CheckforMatch(int x, int y, MoveDirection direction)
    {
        bool gotMatch = true;
        JewelKind currentJewel = GetJewel(x, y);
        int currentIndex = 0;

        List<JewelCoords> JewelData = new List<JewelCoords>();

        switch (direction)
        {
            case MoveDirection.Left:
                while (gotMatch)
                {
                    int targetJewelIndex = x - currentIndex;
                    currentIndex++;
                    
                    if (targetJewelIndex >= 0)
                    {
                        if (currentJewel == GetJewel(targetJewelIndex, y))
                        {
                            JewelData.Add(new JewelCoords(targetJewelIndex, y));
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
                return JewelData;

            case MoveDirection.Right:
                while (gotMatch)
                {
                    int targetJewelIndex = x + currentIndex;
                    currentIndex++;
                    if (targetJewelIndex <= GetWidth() - 1)
                    {
                        if (currentJewel == GetJewel(targetJewelIndex, y))
                        {
                            JewelData.Add(new JewelCoords(targetJewelIndex, y));
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
                return JewelData;

            case MoveDirection.Up:
                while (gotMatch)
                {
                    int targetJewelIndex = y + currentIndex;
                    currentIndex++;
                    if (targetJewelIndex <= GetHeight() - 1)
                    {
                        if (currentJewel == GetJewel(x, targetJewelIndex))
                        {
                            JewelData.Add(new JewelCoords(y, targetJewelIndex));
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
                return JewelData;

            case MoveDirection.Down:
                while (gotMatch)
                {
                    int targetJewelIndex = x - currentIndex;
                    currentIndex++;
                    if (targetJewelIndex >= 0)
                    {
                        if (currentJewel == GetJewel(x, targetJewelIndex))
                        {
                            JewelData.Add(new JewelCoords(y, targetJewelIndex));
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
                return JewelData;

            default:
                return JewelData;
        }
    }
    #endregion

    #region Swap Jewels

    //Swap
    public bool JewelSwap(Move m)
    {
        MainBoard = (JewelKind[,])OrigBoardbackup.Clone(); ;

        JewelKind jewel1 = GetJewel(m.x, m.y);
        JewelKind jewel2 = JewelKind.Empty;

        switch (m.direction)
        {
            case MoveDirection.Right:
                if (m.x < GetWidth() - 1)
                {
                    jewel2 = GetJewel(m.x + 1, m.y);
                    SetJewel(m.x + 1, m.y, jewel1);
                }
                else
                {
                    return false;
                }
                break;
            case MoveDirection.Left:
                if (m.x > 0)
                {
                    jewel2 = GetJewel(m.x - 1, m.y);
                    SetJewel(m.x - 1, m.y, jewel1);
                }
                else
                {
                    return false;
                }
                break;
            case MoveDirection.Up:
                if (m.y < GetHeight() - 1)
                {
                    jewel2 = GetJewel(m.x, m.y + 1);
                    SetJewel(m.x, m.y + 1, jewel1);
                }
                else
                {
                    return false;
                }
                break;
            case MoveDirection.Down:
                if (m.y > 0)
                {
                    jewel2 = GetJewel(m.x, m.y - 1);
                    SetJewel(m.x, m.y - 1, jewel1);
                }
                else
                {
                    return false;
                }
                break;
        }
        SetJewel(m.x, m.y, jewel2);
        return true;
    }
    #endregion

    #region Calculate Best Move

    //Implement this function
    public Move CalculateBestMoveForBoard()
    {
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int x = i % GetWidth();
            int y = i / GetWidth();

            if (x > 0)
            {
                StoreResult(x, y, MoveDirection.Left);
            }

            if (x < GetWidth() - 1)
            {
                StoreResult(x, y, MoveDirection.Right);
            }

            if (y < GetHeight() - 1)
            {
                StoreResult(x, y, MoveDirection.Up);
            }

            if (y > 0)
            {
                StoreResult(x, y, MoveDirection.Down);
            }
        }

        MainBoard = (JewelKind[,])OrigBoardbackup.Clone();

        if (AllMatches.Count == 0 || MatchCount.Count == 0)
            return new Move(0, 0, MoveDirection.None);

        Move bestMove = AllMatches[MatchCount.IndexOf(Mathf.Max(MatchCount.ToArray()))];

        bestMoveJewels = CheckforMatch(bestMove.x, bestMove.y, bestMove.direction);

        return bestMove;
    }

    void StoreResult(int x, int y, MoveDirection direction)
    {
        Move currMove = new Move(x, y, direction);

        if (JewelSwap(currMove))
        {
            checkallMatch(currMove);
        }
    }
    #endregion
}
