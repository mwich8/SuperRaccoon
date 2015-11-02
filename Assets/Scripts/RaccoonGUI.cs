using UnityEngine;
using System.Collections;

public class RaccoonGUI : MonoBehaviour {

    private Color color;

    private float ratio;

    private Vector3 startScale;

    private float buttonWidth;
    private float buttonHeight;

    public Font avengersFont;
    private Texture2D resizedButton;

    // Need a delayed start because otherwise "score" is set to null ==> division by zero = BAD!
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.001f);
        // Sets up the starting scale of the raccoonGUI
        startScale = new Vector3(0.25f, 1, 0.25f);
        // Sets up the button size w.r.t. the actual screen
        buttonWidth = Screen.width / 4;
        buttonHeight = Screen.height / 16;
        transform.localScale = new Vector3(MainSceneLayout.actualScreenWidth / 10, 1f, MainSceneLayout.screenHeight / 5);
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
        // Set the raccoonGUI to red/blue or a composition of the currently collected energySpheres depending on which power is active at the moment
        Renderer rend = gameObject.GetComponent<Renderer>();
        if (Player.isLight)
        {
            rend.material.SetColor("_Color", Color.blue);
        } else if (Player.isDark)
        {
            rend.material.SetColor("_Color", Color.red);
        } else
        {
            ratio = (float)Player.darkEnergy / (float)Player.score;
            color = new Color(ratio, 0, 1 - ratio);
            rend.material.SetColor("_Color", color);
        }
    }

    // Scales the GUI down to it's startScale
    void Update()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, startScale, 0.075f);
    }

    // Updates the GUI if a collectible hits the GUI
    void OnTriggerEnter(Collider coll)
    {
        // Color has only to be updated if the raccoon is neither light nor dark
        if (!Player.isDark && !Player.isLight)
        {
            updateColor();
        }
        Vector3 expandedScale = new Vector3(0.4f, 1, 0.4f);
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, expandedScale, 0.5f);
    }

    // Set the color of the GUI and scales the GUI up
    void updateColor()
    {
        ratio = (float)Player.darkEnergy / (float)Player.score;
        color = new Color(ratio, 0, 1 - ratio);
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
    }

    void OnGUI()
    {
        // Sets up the GUI style
        GUIStyle superGUIStyle = new GUIStyle();
        superGUIStyle.font = avengersFont;
        superGUIStyle.fontSize = 15;
        superGUIStyle.normal.textColor = new Color(168f / 255f, 29f / 255f, 29f / 255f);
        // Center the texts in the buttons (at least it should, it more an approximation to be honest)
        float offsetY = (buttonHeight - superGUIStyle.fontSize);
        // Main Menu Button
        if (GUI.Button(new Rect(buttonHeight / 2, buttonHeight / 2, buttonWidth, buttonHeight), resizedButton, superGUIStyle))
        {
            Player.saveProgress();
            Application.LoadLevel("MainMenu");
        }
        GUI.Button(new Rect(buttonHeight / 2, buttonHeight / 2 + offsetY, 0, 0), "Main Menu", superGUIStyle);
    }
}
