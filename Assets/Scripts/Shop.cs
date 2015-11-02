using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject darkEnergy;
    public GameObject lightEnergy;

    public TextMesh darkCounterText;
    public TextMesh lightCounterText;
    public TextMesh darkUpgrade1Text;
    public TextMesh lightUpgrade1Text;
    public TextMesh currentPowerText;

    private float buttonWidth;
    private float buttonHeight;

    public static float screenHeight;
    public static float screenRatioXtoY;
    public static float actualScreenWidth;

    public Font avengersFont;
    private Texture2D resizedButton;

    private string[] buttonMessages = new string[2];
    private int[] buttonLength = new int[2];

    // Use this for initialization
    void Start () {
        // Calculates the size of the current screen
        screenHeight = Camera.main.orthographicSize;
        screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        // Set Screen orientation and scales background to fit screen size
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = new Vector3((screenHeight * screenRatioXtoY) / 5, 1, screenHeight / 5);
        // Load saveGame 
        Player.loadProgress();
        updateStatusOfCoon();
        // Sets up the text which display the amount of energy which was collected
        darkCounterText = GameObject.Find("DarkEnergyCount").GetComponent<TextMesh>();
        darkCounterText.text = Player.darkEnergy.ToString();
        lightCounterText = GameObject.Find("LightEnergyCount").GetComponent<TextMesh>();
        lightCounterText.text = Player.lightEnergy.ToString();
        // Pre(Calculations)
        float energyAndTextZ = -7f;
        darkUpgrade1Text.text = "Stamp";
        lightUpgrade1Text.text = "D-Jump";
        // Transforms position of energySpheres
        darkEnergy.transform.position = new Vector3((-actualScreenWidth / 2) * 0.9f, screenHeight / 2, energyAndTextZ);
        lightEnergy.transform.position = new Vector3((actualScreenWidth / 2) * 0.9f, screenHeight / 2, energyAndTextZ);
        // Transforms position of counters
        darkCounterText.transform.position = new Vector3(darkEnergy.transform.position.x + darkEnergy.transform.localScale.x, darkEnergy.transform.position.y + darkEnergy.transform.localScale.y / 2, energyAndTextZ);
        lightCounterText.transform.position = new Vector3(lightEnergy.transform.position.x - lightCounterText.text.Length*1.5f, lightEnergy.transform.position.y + lightEnergy.transform.localScale.y / 1.5f, energyAndTextZ);
        // Transforms position of upgrade texts
        darkUpgrade1Text.transform.position = new Vector3(darkEnergy.transform.position.x, 0, energyAndTextZ);
        lightUpgrade1Text.transform.position = new Vector3(actualScreenWidth / 4, 0, energyAndTextZ);
        // Transforms position of currentPowerText
        currentPowerText.transform.position = new Vector3(-currentPowerText.text.Length/2, darkCounterText.transform.position.y, energyAndTextZ);
        buttonWidth = Screen.width / 5;
        buttonHeight = Screen.height / 7;
        // Resizes button and colors it
        int borderSize = 5;
        Color32 borderColor = new Color32(168, 29, 29, 255);
        resizedButton = new Texture2D((int)buttonWidth, (int)buttonHeight);
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
        // Sets up the buttonNames
        buttonMessages[0] = "Back";
        buttonMessages[1] = "Buy";
        // Gets the length of each button-name
        for (int i = 0; i <= buttonMessages.Length; i++)
        {
            buttonLength[i] = buttonMessages[i].Length;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Rotate the energy spheres
        darkEnergy.transform.Rotate(Vector3.up, Space.Self);
        lightEnergy.transform.Rotate(Vector3.up, Space.Self);
    }

    void OnGUI()
    {
        // Sets up the GUI style
        GUIStyle superGUIStyle = new GUIStyle();
        superGUIStyle.font = avengersFont;
        superGUIStyle.fontSize = 30;
        superGUIStyle.normal.textColor = new Color(168f / 255f, 29f / 255f, 29f / 255f);
        // Center the texts in the buttons (at least it should, it more an approximation to be honest)
        float fontScaleX = superGUIStyle.fontSize * 0.2f;
        float offsetY = (buttonHeight - superGUIStyle.fontSize * 0.8f) / 2;
        // Back Button
        if (GUI.Button(new Rect(buttonHeight / 4, Screen.height - (buttonHeight * 1.25f), buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Application.LoadLevel("MainMenu");
        }
        GUI.Button(new Rect((buttonHeight / 4) + (buttonLength[0] * fontScaleX)/2, Screen.height - (buttonHeight * 1.25f) + offsetY, 0, 0), buttonMessages[0], superGUIStyle);
        // Buy buttons
        // Resizing font for smaller buttons
        superGUIStyle.fontSize = (int)((float)superGUIStyle.fontSize / 1.25f);
        fontScaleX = superGUIStyle.fontSize * 0.3f;
        offsetY = (buttonHeight - superGUIStyle.fontSize * 0.8f) / 3;
        // Buy Button for dark power
        if (GUI.Button(new Rect(buttonWidth*1.5f, Screen.height/2 - buttonHeight/3, buttonWidth/1.25f, buttonHeight/1.25f), resizedButton, superGUIStyle))
        {
            if (Player.darkEnergy >= 100)
            {
                Player.darkEnergy -= 100;
                Player.score -= 100;
                Player.isDark = true;
                Player.isLight = false;
                Player.saveProgress();
                darkCounterText = GameObject.Find("DarkEnergyCount").GetComponent<TextMesh>();
                darkCounterText.text = Player.darkEnergy.ToString();
                updateStatusOfCoon();
            } else
            {
                // Doesn't work on Android
                /*
                EditorUtility.DisplayDialog("Not enough energy!",
                "You haven't collected enough dark energy yet! Collect more!", "OK");
                */
            }
        }
        GUI.Button(new Rect(buttonWidth* 1.5f + (buttonLength[1] * fontScaleX) / 2, Screen.height/2 - buttonHeight/3 + offsetY, 0, 0), buttonMessages[1], superGUIStyle);
        // Buy Button for dark power
        if (GUI.Button(new Rect(Screen.width/2, Screen.height / 2 - buttonHeight / 3, buttonWidth / 1.25f, buttonHeight / 1.25f), resizedButton, superGUIStyle))
        {
            if (Player.lightEnergy >= 100)
            {
                Player.lightEnergy -= 100;
                Player.score -= 100;
                Player.isDark = false;
                Player.isLight = true;
                Player.saveProgress();
                lightCounterText = GameObject.Find("LightEnergyCount").GetComponent<TextMesh>();
                lightCounterText.text = Player.lightEnergy.ToString();
                updateStatusOfCoon();
            }
            else
            {
                // Doesn't work on Android
                /*
                EditorUtility.DisplayDialog("Not enough energy!",
                "You haven't collected enough light energy yet! Collect more!", "OK");
                */
            }
        }
        GUI.Button(new Rect(Screen.width / 2 + (buttonLength[1] * fontScaleX) / 2, Screen.height / 2 - buttonHeight / 3 + offsetY, 0, 0), buttonMessages[1], superGUIStyle);
    }

    // Set up the current power of the player
    void updateStatusOfCoon()
    {
        if (Player.isDark)
        {
            currentPowerText.text = "Dark Coon";
            currentPowerText.color = new Color32(168, 29, 29, 255);
        }
        else if (Player.isLight)
        {
            currentPowerText.text = "Light Coon";
            currentPowerText.color = new Color32(35, 0, 219, 255);
        }
        else
        {
            currentPowerText.text = "No Power";
            currentPowerText.color = Color.white;
        }
    }
}
