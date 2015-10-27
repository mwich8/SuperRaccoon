using UnityEngine;
using System.Collections;

public class GenerateCollectibles : MonoBehaviour {

    public GameObject energy;

    public float interval = 0.25f;

    private float offsetTime = 0.25f;

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
        numberOfCollectibles = Random.Range(0, 15);
        energy.transform.position = new Vector3(16f, Random.Range(-7f, -2f));
        offsetY = energy.transform.position.y;
        // Decides whether it's dark or light energy
        if (Random.Range(0f,1f) < 0.5f)
        {
            Renderer rend = energy.GetComponent<Renderer>();
            rend.sharedMaterial.SetColor("_Color", Color.red);
            energy.GetComponent<Light>().color = Color.red;
        }
        else
        {
            Renderer rend = energy.GetComponent<Renderer>();
            rend.sharedMaterial.SetColor("_Color", Color.blue);
            energy.GetComponent<Light>().color = Color.blue;
        }
        while (numberOfCollectibles > 0)
        {
            offsetTime += interval + 0.1f;
            yield return new WaitForSeconds(interval);
            offsetY = energy.transform.position.y + Random.Range(0f, 1f);
            // Calculated y-offset of a single "energy" with respect to the antecedent "energy"
            if (Random.Range(0f,1f) >= 0.5f)
            {
                energy.transform.position = new Vector3(16f, offsetY + Random.Range(0f, 1f));
            } else
            {
                energy.transform.position = new Vector3(16f, offsetY - Random.Range(0f, 1f));
            }
            Instantiate(energy);
            numberOfCollectibles--;
        }
    }
}
