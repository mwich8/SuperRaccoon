using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {

    private int numberOfTurns;
    private int rotateAroundY;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.001f);
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
        transform.Translate(MainSceneLayout.actualSpeed, 0, 0, Space.World);
        // Destruction of obstacles if they are no longer visible
        if (transform.position.x < -(MainSceneLayout.actualScreenWidth/2) - gameObject.transform.localScale.y && gameObject.name == "Dice(Clone)")
        {
            Destroy(gameObject);
        } else if (transform.position.x < -(MainSceneLayout.actualScreenWidth / 2) - gameObject.transform.localScale.y * 2 && gameObject.name == "Table(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
