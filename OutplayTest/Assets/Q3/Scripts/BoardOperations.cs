using System.Collections.Generic;
using System.Linq;
using static JewelProperties;

public class BoardOperations : Board
{
    public static BoardOperations instance;

    JewelKind[,] OrigBoardbackup;

    Dictionary<Move, int> AllMovesWithMatches = new Dictionary<Move, int>();

    private void Awake() => instance = this;

    private void Start()
    {
        MainBoard = new JewelKind[numOfRows, numOfCols];
    }

    //Shuffle the board
    public void shuffle()
    {
        assignJewelstoBoard();

        while (CheckforAnyMatchonBoard())
        {
            assignJewelstoBoard();
        }
    }

    //To check if the board is created without any matches
    bool CheckforAnyMatchonBoard()
    {
        //Checking only right and up to avoid repeat checking
        for (int i = 0; i < MainBoard.Length; i++)
        {
            int currentCount;
            Move m;
            int x = i % GetWidth();
            int y = i / GetWidth();

            currentCount = CheckforMatch(x, y, MoveDirection.Right);
            m = new Move(x, y, MoveDirection.Right);
            if (currentCount >= 3)
            {
                if (!CheckIFMoveAlreadyExist(m))
                    AllMovesWithMatches.Add(m, currentCount);
                return true;
            }

            currentCount = CheckforMatch(x, y, MoveDirection.Up);
            m = new Move(x, y, MoveDirection.Up);

            if (currentCount >= 3)
            {
                if (!CheckIFMoveAlreadyExist(m))
                    AllMovesWithMatches.Add(m, currentCount);
                return true;
            }

        }
        return false;
    }

    //Check if already exists
    bool CheckIFMoveAlreadyExist(Move m)
    {
        if (AllMovesWithMatches.ContainsKey(m))
        {
            return true;
        }
        return false;
    }

    //Check for match in the given direction and return the count
    int CheckforMatch(int x, int y, MoveDirection direction)
    {
        bool gotMatch = true;
        JewelKind currentJewel = GetJewel(x, y);
        int currentIndex = 0;
        int MatchCount = 0;

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
                    int targetJewelIndex = x + currentIndex;
                    currentIndex++;
                    if (targetJewelIndex <= GetWidth() - 1)
                    {
                        if (currentJewel == GetJewel(targetJewelIndex, y))
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
            case MoveDirection.Up:
                while (gotMatch)
                {
                    int targetJewelIndex = y + currentIndex;
                    currentIndex++;
                    if (targetJewelIndex <= GetHeight() - 1)
                    {
                        if (currentJewel == GetJewel(x, targetJewelIndex))
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
                    int targetJewelIndex = x - currentIndex;
                    currentIndex++;
                    if (targetJewelIndex >= 0)
                    {
                        if (currentJewel == GetJewel(x, targetJewelIndex))
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

    //Swap
    bool JewelSwap(Move m)
    {
        MainBoard = (JewelKind[,])OrigBoardbackup.Clone(); ;

        JewelKind jewel1 = GetJewel(m.x, m.y);
        JewelKind jewel2 = JewelKind.Empty;
        switch (m.direction)
        {
            case MoveDirection.Right:
                if (m.x < GetWidth() - 1)
                {
                    swapPieces(jewel1, jewel2, m.x + 1, m.y);
                }
                else
                {
                    return false;
                }
                break;
            case MoveDirection.Left:
                if (m.x > 0)
                {
                    swapPieces(jewel1, jewel2, m.x - 1, m.y);
                }
                else
                {
                    return false;
                }
                break;
            case MoveDirection.Up:
                if (m.y < GetHeight() - 1)
                {
                    swapPieces(jewel1, jewel2, m.x, m.y + 1);
                }
                else
                {
                    return false;
                }
                break;
            case MoveDirection.Down:
                if (m.y > 0)
                {
                    swapPieces(jewel1, jewel2, m.x, m.y - 1);
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

    public void swapPieces(JewelKind jewel1, JewelKind jewel2, int x, int y)
    {
        jewel2 = GetJewel(x, y);
        SetJewel(x, y, jewel1);
    }

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

        int bestValue = AllMovesWithMatches.Values.Max();
        Move bestMove = AllMovesWithMatches.Single(s => s.Value == bestValue).Key;

        MainBoard = (JewelKind[,])OrigBoardbackup.Clone();

        return bestMove;
    }

    bool StoreResult(int x, int y, MoveDirection direction)
    {
        Move currMove = new Move(x, y, direction);

        if (JewelSwap(currMove))
        {
            if (CheckforAnyMatchonBoard())
            {
                return true;
            }
        }
        return false;
    }
}
