using UnityEngine;
using System.Collections;

public class Feet : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        // float a = gameObject.transform.parent.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(gameObject.transform.parent.transform.position.x + 0.5f, gameObject.transform.parent.transform.position.y - 0.5f);
        print(gameObject.transform.parent.transform.position.y);
        transform.Translate(-0.05f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (transform.position.x <= -30)
        {
            transform.position = new Vector3(30, 0, 100);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            Player.state = Player.State.normal;
        }
        if (col.gameObject.tag == "Obstacle")
        {
            Player.state = Player.State.normal;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Obstacle")
        {
            Player.state = Player.State.jumping;
        }
    }
}
