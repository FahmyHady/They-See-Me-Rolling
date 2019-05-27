using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePanel : MonoBehaviour
{
    static GameManager gameManager;
    [SerializeField] HObstacleOne horizontalObstacle;
    [SerializeField] GameObject verticalObstacles;
    [SerializeField] Transform H_startPoint;
    [SerializeField] Transform H_endPoint;
    [SerializeField] Transform verticalSpawnPoint;
    HObstacleOne temp;

    int random;
    static int spawnedObstaclesCount;
    static int myCountWithoutObstacle;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        myCountWithoutObstacle += 1;
        RandomizeAnObstacle();
    }
    private void OnTriggerEnter(Collider other)
    {
        gameManager.InstantiatePanel();
        Destroy(gameObject.transform.parent.gameObject, 4);
    }

    void RandomizeAnObstacle()
    {
        random = Random.Range(1, 11);
        if (random == 3)//any number has a 10% chance
        {
            SpawnObstacle();
        }
        else if (myCountWithoutObstacle > 10) //force one to spawn if more than 10 pass without spawning
        {
            SpawnObstacle();
        }
    }
    void SpawnObstacle() //Chooses a random obstacle to spawn
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                if (horizontalObstacle )
                {
                    temp = Instantiate(horizontalObstacle, RandomizeLocation());
                    temp.startPos = H_startPoint.position;
                    temp.endPos = H_endPoint.position;
                    temp.movingForward = true;
                    myCountWithoutObstacle = 0;
                }
                break;
            case 2:
                if (verticalObstacles)
                {
                Instantiate(verticalObstacles, verticalSpawnPoint);
                myCountWithoutObstacle = 0;

                }

                
                break;

        }

    }
    Transform RandomizeLocation() //Spawn randomly at one of the two locations
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                return H_startPoint;
            case 2:
                return H_endPoint;
        }
        return H_startPoint;
    }
}
