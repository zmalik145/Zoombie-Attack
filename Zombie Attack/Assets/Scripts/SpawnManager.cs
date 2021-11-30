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
        playerController = GameObject.Find("Mutant").GetComponent<PlayerController>();
        InvokeRepeating("Spawning", startDelay, spawnRate);
    }

    void Spawning()
    {
        if (!playerController.isGameOver)
        {
            float posXNegative = Random.Range(-5f, -2.5f);
            float posXPositive = Random.Range(2.5f, 5f);
            float posZ = Random.Range(-50, 40);
            Vector3 spawnPos1 = new Vector3(posXNegative, 0, posZ);
            Vector3 spawnPos2 = new Vector3(posXPositive, 0, posZ);
            Instantiate(enemy, spawnPos1, enemy.transform.rotation);
            Instantiate(enemy, spawnPos2, enemy.transform.rotation);
        }
        
    }
}
