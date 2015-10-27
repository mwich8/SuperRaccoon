using UnityEngine;
using System.Collections;

public class GenerateCollectibles : MonoBehaviour {

    public GameObject energy;

    public float interval = 0.25f;

    private float offsetTime = 0.2f;

    private int numberOfCollectibles;

    private float offsetY;

	// Use this for initialization
	void Start () {
        // energy.transform.position = new Vector3(15.5f, Random.Range(-7.25f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
	    if (offsetTime > 0)
        {
            offsetTime -= Time.deltaTime;
        } else
        {
            StartCoroutine(GenerateCollectible());
            offsetTime += Random.Range(1f,5f);
        }
	}

    IEnumerator GenerateCollectible()
    {
        // Determines number of collectibles that have to be genrated and their position
        numberOfCollectibles = Random.Range(0, 10);
        energy.transform.position = new Vector3(15.5f, Random.Range(-7f, -2f));
        offsetY = energy.transform.position.y;
        while (numberOfCollectibles > 0)
        {
            offsetTime += interval;
            yield return new WaitForSeconds(interval);
            offsetY = energy.transform.position.y + Random.Range(0f, 1f);
            if (Random.Range(0f,1f) >= 0.5f)
            {
                energy.transform.position = new Vector3(transform.position.x, offsetY + Random.Range(0f, 1f));
            } else
            {
                energy.transform.position = new Vector3(transform.position.x, offsetY - Random.Range(0f, 1f));
            }
            Instantiate(energy);
            numberOfCollectibles--;
        }
    }
}
