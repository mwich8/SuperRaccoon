using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject darkEnergy;
    public GameObject lightEnergy;

    public TextMesh darkText;
    public TextMesh lightText;

    private float buttonWidth = Screen.width / 4;
    private float buttonHeight = Screen.height / 8;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = new Vector3(Camera.main.orthographicSize / 2 * (Screen.width / Screen.height), 1f, Camera.main.orthographicSize / 4);
        // darkEnergy.transform.position = new Vector3(-Camera.main.orthographicSize, darkEnergy.transform.position.y, darkEnergy.transform.position.z);
        Player.loadProgress();
        darkText = GameObject.Find("DarkEnergyCount").GetComponent<TextMesh>();
        darkText.text = "x  " + Player.darkEnergy;
        lightText = GameObject.Find("LightEnergyCount").GetComponent<TextMesh>();
        lightText.text = "x  " + Player.lightEnergy;
    }
	
	// Update is called once per frame
	void Update () {
        darkEnergy.transform.Rotate(Vector3.up, Space.Self);
        lightEnergy.transform.Rotate(Vector3.up, Space.Self);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(buttonHeight/2, Screen.height-(buttonHeight * 1.5f), buttonWidth/2, buttonHeight), "Back"))
        {
            Application.LoadLevel("MainMenu");
        }
        if (GUI.Button(new Rect(Screen.width - (buttonWidth * 1.25f), buttonHeight * 2, buttonWidth, buttonHeight), "100 Dark energy"))
        {
            if (Player.darkEnergy >= 100)
            {
                Player.darkEnergy -= 100;
                Player.score -= 100;
                Player.isDark = true;
                Player.isLight = false;
                Player.saveProgress();
                darkText = GameObject.Find("DarkEnergyCount").GetComponent<TextMesh>();
                darkText.text = "x  " + Player.darkEnergy;
            } else
            {
                // Doesn't work on Android
                /*
                EditorUtility.DisplayDialog("Not enough energy!",
                "You haven't collected enough dark energy yet! Collect more!", "OK");
                */
            }
        }
        if (GUI.Button(new Rect(Screen.width - (buttonWidth * 1.25f), buttonHeight * 4, buttonWidth, buttonHeight), "100 Light energy"))
        {
            if (Player.lightEnergy >= 100)
            {
                Player.lightEnergy -= 100;
                Player.score -= 100;
                Player.isDark = false;
                Player.isLight = true;
                Player.saveProgress();
                lightText = GameObject.Find("LightEnergyCount").GetComponent<TextMesh>();
                lightText.text = "x  " + Player.lightEnergy;
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
    }
}
