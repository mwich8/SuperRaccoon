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
    }
}
