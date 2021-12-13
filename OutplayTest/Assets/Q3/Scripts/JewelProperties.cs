using UnityEngine;

public class JewelProperties : MonoBehaviour
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

    public enum MoveDirection
    {
        None,//Adding this to state that there are no matches
        Up,
        Down,
        Left,
        Right
    };

    public struct Move
    {
        public int x;
        public int y;
        public MoveDirection direction;

        public Move(int x, int y, MoveDirection direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
    };

    //Adding this struct to store the bestMove data
    public struct JewelCoords
    {
        public int x;
        public int y;

        public JewelCoords(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
