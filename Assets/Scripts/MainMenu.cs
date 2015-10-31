using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private float buttonWidth;
    private float buttonHeight;

    public GameObject superRaccoonLogo;

    private Vector3 superRaccoonLogoScale = new Vector3(3f, 1f, 1f);

    private Vector3 endScale = new Vector3(3.2f, 1f, 1.1f);

    private bool isLogoGrowing = true;

    public Font avengersFont;
    private Texture2D resizedButton;

    private string[] buttonMessages = new string[3];
    private int[] buttonLength = new int[3];

    // Use this for initialization
    void Start () {
        buttonWidth = Screen.width / 3;
        buttonHeight = Screen.height / 8;
        float screenHeight = Camera.main.orthographicSize;
        float screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        float actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = new Vector3(actualScreenWidth / 10, 1f, screenHeight / 5);
        // Resizes button and colors it
        int borderSize = 5;
        Color32 borderColor = new Color32(168, 29, 29, 255);
        resizedButton = new Texture2D(Screen.width/3, Screen.height/8);
        for (int x = 0; x <= resizedButton.width; x++)
        {
            for (int y = 0; y <= resizedButton.height; y++)
            {
                if (x < borderSize || y < borderSize || x > resizedButton.width - borderSize || y > resizedButton.height - borderSize)
                {
                    resizedButton.SetPixel(x, y, borderColor);
                } else
                {
                    resizedButton.SetPixel(x, y, Color.yellow);
                }
            }
        }
        resizedButton.Apply();
        buttonMessages[0] = "Start";
        buttonMessages[1] = "Shop";
        buttonMessages[2] = "Credits";
        for (int i = 0; i <= 2; i++)
        {
            buttonLength[i] = buttonMessages[i].Length;
        }
    }
	
	// Makes the logo wiggling
	void Update () {
        if (isLogoGrowing) {
            if (superRaccoonLogo.transform.localScale.x <= endScale.x)
            {
                superRaccoonLogo.transform.localScale = new Vector3(superRaccoonLogo.transform.localScale.x + 0.0025f, superRaccoonLogo.transform.localScale.y, superRaccoonLogo.transform.localScale.z + 0.001f);
            }
            else
            {
                isLogoGrowing = !isLogoGrowing;
            }
        }
        else
        {
            if (superRaccoonLogo.transform.localScale.x >= superRaccoonLogoScale.x)
            {
                superRaccoonLogo.transform.localScale = new Vector3(superRaccoonLogo.transform.localScale.x - 0.0025f, superRaccoonLogo.transform.localScale.y, superRaccoonLogo.transform.localScale.z - 0.001f);
            }
            else
            {
                isLogoGrowing = !isLogoGrowing;
            }
        }
    }

    // Creates the buttons and is responsible for the major scene handling
    void OnGUI()
    {
        GUIStyle superGUIStyle = new GUIStyle();
        superGUIStyle.font = avengersFont;
        superGUIStyle.fontSize = 30;
        superGUIStyle.normal.textColor = new Color(168f / 255f, 29f / 255f, 29f / 255f);
        float fontScaleX = superGUIStyle.fontSize * 0.3f;
        float offsetY = (buttonHeight - superGUIStyle.fontSize * 0.8f) / 2;
        if (GUI.Button(new Rect((Screen.width/2) - (buttonWidth/2), Screen.height - (buttonHeight * 6), resizedButton.width, resizedButton.height), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("MainScene");
        }
        GUI.Button(new Rect((Screen.width / 2) - (buttonLength[0] * fontScaleX) , Screen.height - (buttonHeight * 6) + offsetY, 0, 0), buttonMessages[0], superGUIStyle);
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height - (buttonHeight * 4), buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("Shop");
        }
        GUI.Button(new Rect((Screen.width / 2) - (buttonLength[1] * fontScaleX), Screen.height - (buttonHeight * 4) + offsetY, 0, 0), buttonMessages[1], superGUIStyle);
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height - (buttonHeight * 2), buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("Credits");
        }
        GUI.Button(new Rect((Screen.width / 2) - (buttonLength[2] * fontScaleX), Screen.height - (buttonHeight * 2) + offsetY, 0, 0), buttonMessages[2], superGUIStyle);
    }
}
