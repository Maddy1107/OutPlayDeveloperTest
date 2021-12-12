using UnityEngine;

public class PredictHeight : MonoBehaviour
{
    # region Variables

    public float Height;
    public float width;
    public float gravity;
    public Vector2 initialPos;
    public Vector2 velocity;
    public float groundHeight;

    public Texture ballInitial;
    public Texture ballPath;
    public Texture ballEnd;

    public Texture wall;

    GUIStyle style = new GUIStyle();

    const float ballRad = 20;

    #endregion

    #region GUIStyle
    void OnGUI()
    {
        #region Draw the room
        style.fontSize = 20;
        style.fontStyle = FontStyle.Bold;

        //Base
        GUI.DrawTexture(new Rect((Screen.width - width) / 2, Screen.height - groundHeight, width, 5), wall);

        //Specified Height
        GUI.Label(new Rect((Screen.width - width) / 2 + 5, Screen.height - groundHeight - Height - 20, width, 30), "Height", style);
        GUI.DrawTexture(new Rect((Screen.width - width) / 2, Screen.height - groundHeight - Height, width, 5), wall);

        //Wall
        GUI.DrawTexture(new Rect((Screen.width - width) / 2, 0, 5, Screen.height - groundHeight), wall);
        GUI.DrawTexture(new Rect((Screen.width + width) / 2, 0, 5, Screen.height - groundHeight), wall);

        #endregion

        //Point on init Pos
        drawBall(initialPos, ballInitial);

        //initialising required position to 0
        float reqX = 0;

        
        #region Change of Parameters

        GUI.Label(new Rect(10, Screen.height - groundHeight - 480, 200, 5), "Change Parameters", style);

        //Change parameters in runtime

        //Height
        GUI.Label(new Rect(5, Screen.height - groundHeight - 420, 200, 5), "Height - " + Height, style);
        Height = GUI.HorizontalSlider(new Rect(5, Screen.height - groundHeight - 390, 200, 5),
            Height, 100.0f, 480.0f);//Question mentions the the height can be infinite. But for the visualization the height has been limited.

        //Initial Position x
        GUI.Label(new Rect(5, Screen.height - groundHeight - 350, 200, 5), "Intial Pos X - " + initialPos.x, style);
        initialPos.x = GUI.HorizontalSlider(new Rect(5, Screen.height - groundHeight - 320, 200, 5),
            initialPos.x, 0.0f, 500.0f);

        //Initial Position y
        GUI.Label(new Rect(5, Screen.height - groundHeight - 280, 200, 5), "Initial Pos Y - " + initialPos.y, style);
        initialPos.y = GUI.HorizontalSlider(new Rect(5, Screen.height - groundHeight - 250, 200, 5),
            initialPos.y, 0.0f, Height);

        //Velocity x
        GUI.Label(new Rect(5, Screen.height - groundHeight - 210, 200, 5), "Initial Velocity X - " + velocity.x, style);
        velocity.x = GUI.HorizontalSlider(new Rect(5, Screen.height - groundHeight - 180, 200, 5),
            velocity.x, 0.0f, 100.0f);

        //Velocity y
        GUI.Label(new Rect(5, Screen.height - groundHeight - 140, 200, 5), "Initial Velocity Y - " + velocity.y, style);
        velocity.y = GUI.HorizontalSlider(new Rect(5, Screen.height - groundHeight - 100, 200, 5),
            velocity.y, 0.0f, 100.0f);

        //Gravity
        GUI.Label(new Rect(5, Screen.height - groundHeight - 70, 200, 5), "Gravity - " + gravity, style);
        gravity = GUI.HorizontalSlider(new Rect(5, Screen.height - groundHeight - 30, 200, 5),
            gravity, 0.0f, 10.0f);


        //Some values has been limited to a certain limit for simplicity of visualization

        #endregion

        //Call the function to predict the path and tehj final position at teh specified height.
        TryCalculateXPositionAtHeight(Height, initialPos, velocity, gravity, width, ref reqX);

        if (reqX == 0)
        {
            GUI.Label(new Rect((Screen.width - width) / 2 + 80, Screen.height - groundHeight + 10, width, 30), "Required Position - Not reached Height", style);
        }
        else
        {
            GUI.Label(new Rect((Screen.width - width) / 2 + 80, Screen.height - groundHeight + 10, width, 30), "Required Position-" + reqX, style);
        }

        Debug.Log(reqX);

    }

    void drawBall(Vector2 pos, Texture tex)
    {
        GUI.DrawTexture(new Rect((Screen.width - width) / 2 + pos.x - ballRad / 2, Screen.height - groundHeight - pos.y - ballRad / 2,
            ballRad, ballRad), tex);
    }

    #endregion

    #region TryCalculateXPositionAtHeight
    bool TryCalculateXPositionAtHeight(float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
    {

        //--------------------------------------------------------Initial Approach------------------------------------------------------------------------

        // Initial approach was to get the time required to reach the height and 
        //then find the x position by adding the distance that will be travelled in the calculated time.


        //Calculate the time to reach the given height

        //float time = (Mathf.Sqrt(v.y * v.y - 4.0f * (_G * 0.5f) * (p.y - currentpos.y)) - v.y) / (2.0f * (_G * 0.5f));

        //Get the new Xposition by adding the distance to the initial position.
        //Accelaration = 0

        //float XPos = p.x + (time * v.x);
        //--------------------------------------------------------Initial Approach------------------------------------------------------------------------

        //--------------------------------------------------------Final Approach------------------------------------------------------------------------


        //Final approach is to apply the forces to the balls current pos to find the next position
        //Check if it is colliding with the wall on either side.
        //Check if reached the height, if yes set that to teh xPosition.
        //Lastly if it has reached teh ground, it will not bounce and will return false
        //Else it will continue.

        Vector2 currentpos = p;
        Vector2 prevpos = currentpos;
        Vector2 vel = velocity;
        float g = G;

        while (currentpos.y <= h && currentpos.y >= 0)
        {
            //Apply forces.
            currentpos = currentpos + vel + (Vector2.down * g);

            // Ball bouncing off the walls
            if (currentpos.x > w)
            {
                //Simplified w -(Xpos - w)
                currentpos.x = (2.0f * w) - currentpos.x;
                vel.x = -vel.x;
            }
            else if (currentpos.x < 0)
            {
                currentpos.x = 0;
                currentpos.x = currentpos.x - prevpos.x;
                vel.x = -vel.x;
            }

            //Check if reached height
            if (currentpos.y >= h && prevpos.y <= h)
            {
                currentpos.y = h;
                xPosition = currentpos.x;
                drawBall(new Vector2(xPosition, currentpos.y), ballEnd);
                return true;
            }

            prevpos = currentpos;

            //Check if reached ground.
            if(currentpos.y <= 0)
            {
                currentpos.y = 0;
                drawBall(currentpos, ballEnd);
                return false;
            }
            else
            {
                g += G;
                drawBall(currentpos, ballPath);
            }
        }
        return true;
    }
    #endregion
}

