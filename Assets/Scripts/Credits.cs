using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    private float buttonWidth = Screen.width / 4;
    private float buttonHeight = Screen.height / 16;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        transform.localScale = new Vector3(Camera.main.orthographicSize / 2 * (Screen.width / Screen.height), 1f, Camera.main.orthographicSize / 4);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(buttonHeight / 2, Screen.height - (buttonHeight * 1.5f), buttonWidth / 2, buttonHeight), "Back"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
