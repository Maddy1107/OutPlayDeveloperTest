using UnityEngine;

public class Match_3Manager : MonoBehaviour
{
    public Texture RedJewel;
    public Texture OrangeJewel;
    public Texture YellowJewel;
    public Texture GreenJewel;
    public Texture BlueJewel;
    public Texture IndigoJewel;
    public Texture VioletJewel;

    const int jewelGridSize = 60;

    void OnGUI()
    {
        if (GUI.Button(new Rect(80, Screen.height - 350, 100, 50), "Shuffle"))
        {
            BoardOperations.instance.shuffle();
        }
        if (GUI.Button(new Rect(80, Screen.height - 250, 100, 50), "Get Best Move"))
        {
            //Move m = CalculateBestMoveForBoard();
            //GUI.Label(new Rect(80, Screen.height - 250, 100, 50), "Move (" + m.x.ToString() + "," + m.y.ToString() + ") in the " + m.direction + " direction.");
            //Debug.Log("Move (" + m.x.ToString() + "," + m.y.ToString() + ") in the " + m.direction + " direction.");
        }

        //Visualize the board
        for (int i = 0; i < BoardOperations.instance.MainBoard.Length; i++)
        {
            int x = i % BoardOperations.instance.GetWidth();
            int y = i / BoardOperations.instance.GetWidth();

            GUI.DrawTexture(new Rect(jewelGridSize * x + Screen.width / 4, jewelGridSize * (BoardOperations.instance.GetHeight() - y) - 70, jewelGridSize, jewelGridSize), getTexture(BoardOperations.instance.MainBoard[x, y]));
            GUI.Label(new Rect(jewelGridSize * x + Screen.width / 4 - 10, jewelGridSize * (BoardOperations.instance.GetHeight() - y) - 50, jewelGridSize, jewelGridSize), "(" + x.ToString() + "," + y.ToString() + ")");

        }
    }

    Texture getTexture(JewelProperties.JewelKind kind)
    {
        switch (kind)
        {
            case JewelProperties.JewelKind.Red:
                return RedJewel;
            case JewelProperties.JewelKind.Orange:
                return OrangeJewel;
            case JewelProperties.JewelKind.Yellow:
                return YellowJewel;
            case JewelProperties.JewelKind.Green:
                return GreenJewel;
            case JewelProperties.JewelKind.Blue:
                return BlueJewel;
            case JewelProperties.JewelKind.Indigo:
                return IndigoJewel;
            case JewelProperties.JewelKind.Violet:
                return VioletJewel;
            default:
                return Texture2D.whiteTexture;
        }
    }

    public void Start()
    {
        BoardOperations.instance.shuffle();
    }

}
