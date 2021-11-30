using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float lowerBound = -2;


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowerBound)
        {
            // Just deactivate it
            gameObject.SetActive(false);

        }

    }
}
