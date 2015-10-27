using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

    private float oneDirection = 1f;

    private float amountMovingY = 1.5f;
    public bool wasCollected = false;

    private Vector3 raccoonGUI = new Vector3(12.8f, 7.8f, -1.0f);
    
    private Shader shader;

    private bool isMovingUpwards = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!wasCollected)
        {
            transform.Translate(-0.08f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        } else
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, raccoonGUI, 0.075f);
        }
        if (isMovingUpwards)
        {
            transform.Translate(Vector3.up/200);
        } else
        {
            transform.Translate(Vector3.down/200);
        }
        if (oneDirection > 0)
        {
            oneDirection -= Time.deltaTime;
        } else
        {
            oneDirection = amountMovingY;
            isMovingUpwards = !isMovingUpwards;
        }
        if (transform.position.x <= -15.5f || transform.position.y >= 11f)
        {
            Destroy(gameObject);
        }
        if ((transform.position.x >= 12f || transform.position.y >= 7.2f) && wasCollected)
        {

            // Player.updateRaccoonGUI();
            Destroy(gameObject);
        }
    }

    public void SimpleMethod()
    {
        wasCollected = true;
    }
}
