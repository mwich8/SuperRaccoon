using UnityEngine;
using System.Collections;

public class GenerateObstacles : MonoBehaviour {

    // Use this for initialization
    public GameObject dice;
    public GameObject table;

    private float nextObstacleInSecs;

    private float fastSpawn = 1.0f;
    private float slowSpawn = 2.0f;

    // Use this for initialization
    IEnumerator Start()
    {
        // Has to wait to make sure the MainSceneLayout-script is loaded properly
        yield return new WaitForSeconds(0.001f);
        // Determines the spawnPoint of the obstacles
        float platformHeight = GetComponent<MainSceneLayout>().platform1.transform.localScale.y;
        float dicePosY = -MainSceneLayout.screenHeight + platformHeight + dice.transform.localScale.y;
        float tablePosY = -MainSceneLayout.screenHeight + platformHeight;
        // Set the spawnPoint of the obstacles
        dice.transform.position = new Vector3((MainSceneLayout.actualScreenWidth / 2) + dice.transform.localScale.x, dicePosY);
        table.transform.position = new Vector3((MainSceneLayout.actualScreenWidth / 2) + table.transform.localScale.x, tablePosY);
    }

    // Update is called once per frame
    void Update () {
        // Determines the point in time when to generate the next obstacle
        if (nextObstacleInSecs > 0)
        {
            nextObstacleInSecs -= Time.deltaTime;
        } else
        {
            StartCoroutine(GenerateObstacle());
            nextObstacleInSecs = Random.Range(fastSpawn, slowSpawn);
        }

    }

    // Generates an obstacle
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
