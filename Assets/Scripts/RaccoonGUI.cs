using UnityEngine;
using System.Collections;

public class RaccoonGUI : MonoBehaviour {

    private Color color;

    private float ratio;

    private Vector3 startScale;

    // Need a delayed start because otherwise "score" is set to null ==> division by zero = BAD!
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.001f);
        ratio = (float)Player.darkEnergy / (float)Player.score;
        color = new Color(ratio, 0, 1 - ratio);
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
        startScale = new Vector3(0.25f,1,0.25f);
    }

    // Scales the GUI down to it's startScale
    void Update()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, startScale, 0.075f);
    }

    // Updates the GUI if a collectible hits the GUI
    void OnTriggerEnter(Collider coll)
    {
        updateColor();
    }

    // Set the color of the GUI and scales the GUI up
    void updateColor()
    {
        ratio = (float)Player.darkEnergy / (float)Player.score;
        color = new Color(ratio, 0, 1 - ratio);
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
        Vector3 a = new Vector3(0.3f, 1, 0.3f);
        // Vector3.Lerp(gameObject.transform.localScale, a, 0.075f);
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, a, 0.5f);
    }
}
