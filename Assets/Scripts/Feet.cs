using UnityEngine;
using System.Collections;

public class Feet : MonoBehaviour {

    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        // Transforms the position of the "feet" according to the player
        transform.position = new Vector3(gameObject.transform.parent.transform.position.x + 0.5f, gameObject.transform.parent.transform.position.y - 0.5f);
    }

    void OnCollisionEnter(Collision col)
    {
        // Sets the Player-State to normal if the feet, hit a platform or an obstacle ==> Used for determining correct animations etc.
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Obstacle")
        {
            Player.state = Player.State.normal;
        }
    }
    
    void OnCollisionExit(Collision col)
    {
        // Sets the Player-State to jumping if the feet leave the obstacle
        // As I'm using two platforms I didn't make an case for the feet leaving the platform, because otherwise everytime the player leaves the first platform and the feet hit the second one, he is trapped in the "Jumping"-state
        if (col.gameObject.tag == "Obstacle")
        {
            Player.state = Player.State.jumping;
        }
    }
}
