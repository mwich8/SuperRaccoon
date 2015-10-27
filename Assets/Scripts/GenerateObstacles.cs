using UnityEngine;
using System.Collections;

public class GenerateObstacles : MonoBehaviour {

    // Use this for initialization
    public GameObject dice;
    public GameObject table;

    private float randomNumber;

    private float fastSpawn = 1.0f;
    private float slowSpawn = 2.0f;

    // Use this for initialization
    void Start()
    {
        dice.transform.position = new Vector3(15.76f, -7.15f);
        table.transform.position = new Vector3(17.72f, -7.9f);
    }

    // Update is called once per frame
    void Update () {
        if (randomNumber > 0)
        {
            randomNumber -= Time.deltaTime;
        } else
        {
            // fastSpawn += (Mathf.Sqrt(Player.score)) / 100;
            // slowSpawn += (Mathf.Sqrt(Player.score)) / 100;
            StartCoroutine(GenerateObstacle());
            // Invoke("GenerateObstacle", 0);
            randomNumber = Random.Range(fastSpawn, slowSpawn);
        }

    }

    IEnumerator GenerateObstacle()
    {
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            yield return new WaitForSeconds(1);
            Instantiate(dice);
        }
        else
        {
            yield return new WaitForSeconds(2);
            Instantiate(table);
        }
    }
}
