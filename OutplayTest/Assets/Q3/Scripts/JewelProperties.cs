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
}
