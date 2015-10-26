using UnityEngine;
using System.Collections;

public class GenerateCollectibles : MonoBehaviour {

    public GameObject energy;

    public float spawnTime = 0.2f;

	// Use this for initialization
	void Start () {
        energy.transform.position = new Vector3(15.5f, Random.Range(-7.25f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
	    if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        } else
        {
            GenerateCollectible();
            spawnTime = 0.2f;
        }
	}

    void GenerateCollectible()
    {
        Instantiate(energy);
    }
}
