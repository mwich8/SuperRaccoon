using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject darkEnergy;
    public GameObject lightEnergy;

    public TextMesh darkText;
    public TextMesh lightText;

	// Use this for initialization
	void Start () {
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
        if (GUI.Button(new Rect(10, Screen.height-40, 50, 30), "Back"))
        {
            Application.LoadLevel("MainMenu");
        }
        if (GUI.Button(new Rect(Screen.width/1.35f, Screen.height/3.8f, 120, 30), "100 Dark energy"))
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
        if (GUI.Button(new Rect(Screen.width/1.35f, Screen.height/1.9f, 120, 30), "100 Light energy"))
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
