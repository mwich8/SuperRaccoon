using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, Screen.height - 40, 50, 30), "Back"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
