using UnityEngine;
using System.Collections;

public class GenerateCollectibles : MonoBehaviour {

    public GameObject energy;

    public float interval = 0.25f;

    private float offsetTime = 0.25f;

    private int numberOfCollectibles;

    private float offsetY;

	// Update is called once per frame
	void Update () {
        // Determines the point in time when to generate the next collectible
        if (offsetTime > 0)
        {
            offsetTime -= Time.deltaTime;
        } else
        {
            StartCoroutine(GenerateCollectible());
            offsetTime += Random.Range(1f,5f);
        }
	}

    // Generates a collectible
    IEnumerator GenerateCollectible()
    {
        // Determines number of collectibles that have to be generated and their position
        numberOfCollectibles = Random.Range(3, 15);
        offsetY = energy.transform.position.y;
        energy.transform.position = new Vector3(MainSceneLayout.actualScreenWidth / 2, Random.Range(-7f, -4f));
        // Decides whether it's dark or light energy
        Renderer rend = energy.GetComponent<Renderer>();
        if (Random.Range(0f,1f) < 0.5f)
        {
            rend.sharedMaterial.SetColor("_Color", Color.red);
            rend.sharedMaterial.SetColor("_EmissionColor", Color.red);
            energy.GetComponent<Light>().color = Color.red;
        }
        else
        {
            rend.sharedMaterial.SetColor("_Color", Color.blue);
            rend.sharedMaterial.SetColor("_EmissionColor", Color.blue);
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
                energy.transform.position = new Vector3(energy.transform.position.x, offsetY + Random.Range(0f, 1f));
            } else
            {
                energy.transform.position = new Vector3(energy.transform.position.x, offsetY - Random.Range(0f, 1f));
            }
            Instantiate(energy);
            numberOfCollectibles--;
        }
    }
}
