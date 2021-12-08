using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private float startDelay = 1f;
    private float spawnRate = 0.5f;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //get the playercontroller script reference from plyer in hierarchy
        playerController = GameObject.Find("Mutant").GetComponent<PlayerController>();

        //Invoke the spawing method on start and then repeat after 0.5 second
        InvokeRepeating("Spawning", startDelay, spawnRate);
    }

    void Spawning()
    {
        //check if game is not over
        if (!playerController.isGameOver)
        {
            float posXNegative = Random.Range(-5f, -2.5f);          //spawn the enemy b/w -5 - -2.5 on -x-axis
            float posXPositive = Random.Range(2.5f, 5f);            //spawn the enemy b/w 2.5 - 5 on  x-axis
            float posZ = Random.Range(-50, 40);                     // spawn the enemy b/w -50 - 40 on z.axis
            Vector3 spawnPos1 = new Vector3(posXNegative, 0, posZ);
            Vector3 spawnPos2 = new Vector3(posXPositive, 0, posZ);
            Instantiate(enemy, spawnPos1, enemy.transform.rotation); //instantiate the enemy
            Instantiate(enemy, spawnPos2, enemy.transform.rotation);
        }
        
    }
}
