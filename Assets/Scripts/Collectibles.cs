using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

    private float oneDirection = 1f;

    private float amountMovingY = 1.5f;
    // private float amountMovingX = 0.2f;

    // private Light light;
    private Shader shader;

    private bool isMovingUpwards = false;

	// Use this for initialization
	void Start () {
        // light = GetComponent<Light>();
        // light.color = new Color(0.3f, 1, 0);
        // Renderer rend = GetComponent<Renderer>();
        // rend.material.shader = Shader.Find("Emission");
        // rend.material.SetColor("_Emission", Color.red);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-0.08f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (isMovingUpwards)
        {
            transform.Translate(Vector3.up/200);
            // GetComponent<Rigidbody>().velocity = new Vector3(0, amountMovingX, 0);
        } else
        {
            transform.Translate(Vector3.down/200);
            // GetComponent<Rigidbody>().velocity = new Vector3(0, -amountMovingX, 0);
        }
        if (oneDirection > 0)
        {
            oneDirection -= Time.deltaTime;
        } else
        {
            oneDirection = amountMovingY;
            isMovingUpwards = !isMovingUpwards;
        }
        if (transform.position.x <= -15.5f)
        {
            Destroy(gameObject);
        }
	}
}
