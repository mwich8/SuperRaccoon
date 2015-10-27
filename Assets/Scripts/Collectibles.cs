using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

    private float oneDirection = 1f;

    private float amountMovingY = 1.5f;
    public bool wasCollected = false;

    private Vector3 direction;

    // private Light light;
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
            transform.Translate(direction/7.5f, Space.World);
            // transform.Translate(-0.08f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
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
	}

    public void SimpleMethod(Vector3 dir)
    {
        print(dir);
        direction = dir;
        wasCollected = true;
    }
}
