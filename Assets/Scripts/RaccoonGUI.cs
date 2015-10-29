using UnityEngine;
using System.Collections;

public class RaccoonGUI : MonoBehaviour {

    private Color color;

    private float ratio;

    private Vector3 startScale = new Vector3(0.25f, 1, 0.25f);

    private float buttonWidth = Screen.width / 4;
    private float buttonHeight = Screen.height / 16;

    // Need a delayed start because otherwise "score" is set to null ==> division by zero = BAD!
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.001f);
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
    }

    // Set the color of the GUI and scales the GUI up
    void updateColor()
    {
        ratio = (float)Player.darkEnergy / (float)Player.score;
        color = new Color(ratio, 0, 1 - ratio);
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
        Vector3 a = new Vector3(0.3f, 1, 0.3f);
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, a, 0.5f);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(buttonHeight / 2, buttonHeight / 2, buttonWidth, buttonHeight), "Main Menu"))
        {
            Player.saveProgress();
            Application.LoadLevel("MainMenu");
        }
    }
}
