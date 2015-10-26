using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {

    private float speed = 0.08f;

    private TextMesh textObject;

    private int numberOfTurns;
    private int rotateAroundY;

    // Use this for initialization
    void Start()
    {
        textObject = GameObject.Find("Score").GetComponent<TextMesh>();
        // Every time a dice is generated it should be rotated and get a random color
        if (gameObject.name == "Dice(Clone)")
        {
            numberOfTurns = Random.Range(1, 4);
            rotateAroundY = Random.Range(0, 2);
            gameObject.transform.Rotate(90 * numberOfTurns, 90 * numberOfTurns * rotateAroundY, 0, Space.World);
            GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }

    // Update is called once per frame
    void Update () {
        // Set movement speed of the obstacles
        transform.Translate(-speed-(Mathf.Sqrt(Player.score))/100, 0, 0, Space.World);
        // Destruction of 
        if (transform.position.x < -15.5f && gameObject.name == "Dice(Clone)")
        {
            Player.score++;
            textObject.text = "Score: " + Player.score;
            Destroy(gameObject);
        } else if (transform.position.x < -17.72f && gameObject.name == "Table(Clone)")
        {
            Player.score++;
            textObject.text = "Score: " + Player.score;
            Destroy(gameObject);
        }
    }
}
