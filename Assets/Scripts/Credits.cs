using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    private float buttonWidth = Screen.width / 4;
    private float buttonHeight = Screen.height / 16;

    void Start()
    {
        float screenHeight = Camera.main.orthographicSize;
        float screenRatioXtoY = (float)Screen.width / (float)Screen.height;
        float actualScreenWidth = (screenRatioXtoY * 2f * screenHeight);
        transform.localScale = new Vector3(actualScreenWidth/10, 1f, screenHeight/5);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(buttonHeight / 2, Screen.height - (buttonHeight * 1.5f), buttonWidth / 2, buttonHeight), "Back"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
