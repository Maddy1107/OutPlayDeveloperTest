using UnityEngine;

public class RenderUI : MonoBehaviour
{
    public Texture RedJewel;
    public Texture OrangeJewel;
    public Texture YellowJewel;
    public Texture GreenJewel;
    public Texture BlueJewel;
    public Texture IndigoJewel;
    public Texture VioletJewel;

    const int jewelGridSize = 60;

    bool ShowCoordiantes = false;

    bool showBoard = true;

    bool showOriginalBoard = true;

    void OnGUI()
    {
        if (showBoard)
        {
            if (GUI.Button(new Rect(70, Screen.height - 450, 120, 50), "Show Coordinates"))
            {
                ShowCoordiantes = !ShowCoordiantes;
            }

            if (GUI.Button(new Rect(80, Screen.height - 350, 100, 50), "Shuffle"))
            {
                BoardOperations.instance.shuffle();
                showOriginalBoard = true;
            }

            if (GUI.Button(new Rect(80, Screen.height - 250, 100, 50), "Get Best Move") && showOriginalBoard)
            {
                showOriginalBoard = false;

                JewelProperties.Move m = BoardOperations.instance.CalculateBestMoveForBoard();
                BoardOperations.instance.JewelSwap(m);
                string text = "Best Move is:\n Move (" + m.x.ToString() + ", " + m.y.ToString() + ") to the " + m.direction + "direction."; 
                GUI.Label(new Rect(500 , 100, 1000, 1000), "Hello");

            }
            else if (GUI.Button(new Rect(60, Screen.height - 150, 130, 50), "Show Original Board") && !showOriginalBoard)
            {
                showOriginalBoard = true;
            }

            //Visualize the board
            for (int i = 0; i < BoardOperations.instance.MainBoard.Length; i++)
            {
                int x = i % BoardOperations.instance.GetWidth();
                int y = i / BoardOperations.instance.GetWidth();

                if (showOriginalBoard)
                {
                    GUI.DrawTexture(new Rect(jewelGridSize * x + Screen.width / 4, jewelGridSize * (BoardOperations.instance.GetHeight() - y) - 70, jewelGridSize, jewelGridSize), getTexture(BoardOperations.instance.OrigBoardbackup[x, y]));
                }
                else
                {
                    GUI.DrawTexture(new Rect(jewelGridSize * x + Screen.width / 4, jewelGridSize * (BoardOperations.instance.GetHeight() - y) - 70, jewelGridSize, jewelGridSize), getTexture(BoardOperations.instance.MainBoard[x, y]));
                    foreach (JewelProperties.JewelCoords j in BoardOperations.instance.bestMoveJewels)
                    {
                        GUI.DrawTexture(new Rect(jewelGridSize * j.x + Screen.width / 4, jewelGridSize * (BoardOperations.instance.GetHeight() - j.y) - 70, jewelGridSize, jewelGridSize), getTexture(JewelProperties.JewelKind.Empty));
                    }
                }

                if (ShowCoordiantes)
                {
                    GUI.Label(new Rect(jewelGridSize * x + Screen.width / 4 - 10, jewelGridSize * (BoardOperations.instance.GetHeight() - y) - 50, jewelGridSize, jewelGridSize), "(" + x.ToString() + "," + y.ToString() + ")");
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 40, 120, 50), "Generate Board"))
            {
                showBoard = true;
            }
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
}
