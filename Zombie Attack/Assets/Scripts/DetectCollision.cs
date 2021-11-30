using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Destroy(other.gameObject, 3);
        }
        else if (other.gameObject.CompareTag("Rocks"))
        {
            gameObject.SetActive(false);
        }
    }
      
}
