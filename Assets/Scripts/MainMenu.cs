using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private float buttonWidth = Screen.width/4;
    private float buttonHeight = Screen.height/8;

    public GameObject superRaccoonLogo;

    private Vector3 superRaccoonLogoScale = new Vector3(3f, 1f, 1f);

    private Vector3 endScale = new Vector3(3.2f, 1f, 1.1f);

    private bool isLogoGrowing = true;

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = new Vector3(Camera.main.orthographicSize / 2 * (Screen.width / Screen.height), 1f, Camera.main.orthographicSize / 4);
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
        if (GUI.Button(new Rect((Screen.width/2) - (buttonWidth/2), Screen.height - (buttonHeight * 6), buttonWidth, buttonHeight), "Start"))
        {
            Application.LoadLevel("MainScene");
        }
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height - (buttonHeight * 4), buttonWidth, buttonHeight), "Shop"))
        {
            Application.LoadLevel("Shop");
        }
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height - (buttonHeight*2), buttonWidth, buttonHeight), "Credits"))
        {
            Application.LoadLevel("Credits");
        }
    }
}
