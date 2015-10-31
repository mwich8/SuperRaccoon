using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private float buttonWidth = Screen.width/3;
    private float buttonHeight = Screen.height/8;

    public GameObject superRaccoonLogo;

    private Vector3 superRaccoonLogoScale = new Vector3(3f, 1f, 1f);

    private Vector3 endScale = new Vector3(3.2f, 1f, 1.1f);

    private bool isLogoGrowing = true;

    public Font avengersFont;
    public Texture2D customButton;
    private Texture2D resizedButton;


	// Use this for initialization
	void Start () {
        float screenHeight = Camera.main.orthographicSize;
        float screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        float actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = new Vector3(actualScreenWidth / 10, 1f, screenHeight / 5);
        // Resizes button and colors it
        int borderSize = 20;
        Color32 borderColor = new Color32(168, 29, 29, 255);
        resizedButton = new Texture2D(customButton.width * 3, (int)(customButton.height*1.8f), TextureFormat.RGBA32,false);
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
        float offsetX = (buttonWidth - superGUIStyle.fontSize) / 8;
        float offsetY = (buttonHeight - superGUIStyle.fontSize) / 2;
        if (GUI.Button(new Rect((Screen.width/2) - (buttonWidth/2), Screen.height - (buttonHeight * 6), buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("MainScene");
        }
        GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2) + offsetX, Screen.height - (buttonHeight * 6) + offsetY, buttonWidth, buttonHeight), "Start", superGUIStyle);
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height - (buttonHeight * 4), buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("Shop");
        }
        GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2) + offsetX, Screen.height - (buttonHeight * 4) + offsetY, buttonWidth, buttonHeight), "Shop", superGUIStyle);
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height - (buttonHeight * 2), buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("Credits");
        }
        GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2) + offsetX, Screen.height - (buttonHeight * 2) + offsetY, buttonWidth, buttonHeight), "Credits", superGUIStyle);
    }
}
