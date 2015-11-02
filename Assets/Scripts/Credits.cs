using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    private float buttonWidth;
    private float buttonHeight;

    public Font avengersFont;
    private Texture2D resizedButton;

    void Start()
    {
        // Calculates the size of the current screen
        float screenHeight = Camera.main.orthographicSize;
        float screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        float actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        // Scales the background
        transform.localScale = new Vector3(actualScreenWidth/10, 1f, screenHeight/5);
        // Sets up the button size w.r.t. the actual screen
        buttonWidth = Screen.width / 4;
        buttonHeight = Screen.height / 16;
        // Resizes button and colors it
        int borderSize = 5;
        Color32 borderColor = new Color32(168, 29, 29, 255);
        resizedButton = new Texture2D(Screen.width / 3, Screen.height / 8);
        for (int x = 0; x <= resizedButton.width; x++)
        {
            for (int y = 0; y <= resizedButton.height; y++)
            {
                if (x < borderSize || y < borderSize || x > resizedButton.width - borderSize || y > resizedButton.height - borderSize)
                {
                    resizedButton.SetPixel(x, y, borderColor);
                }
                else
                {
                    resizedButton.SetPixel(x, y, Color.yellow);
                }
            }
        }
        resizedButton.Apply();
    }

    void OnGUI()
    {
        // Sets up the GUI style
        GUIStyle superGUIStyle = new GUIStyle();
        superGUIStyle.font = avengersFont;
        superGUIStyle.fontSize = 15;
        superGUIStyle.normal.textColor = new Color(168f / 255f, 29f / 255f, 29f / 255f);
        // Center the texts in the buttons (at least it should, it more an approximation to be honest)
        float fontScaleX = "Back".Length * (superGUIStyle.fontSize * 0.3f);
        float offsetY = (buttonHeight - superGUIStyle.fontSize) / 2;
        // Back Button
        if (GUI.Button(new Rect(buttonHeight / 2, Screen.height - (buttonHeight * 1.5f), buttonWidth/2, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("MainMenu");
        }
        GUI.Button(new Rect(buttonHeight / 2 + (fontScaleX / 2), Screen.height - (buttonHeight * 1.5f) + offsetY, 0, 0), "Back", superGUIStyle);
    }
}
