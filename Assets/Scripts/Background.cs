using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-0.05f - (Mathf.Sqrt(Player.score)) / 100, 0, 0, Space.World);
        if (transform.position.x <= -30)
        {
            transform.position = new Vector3(30, 0, 100);
        }
    }
}
