using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //check if fireball collides with enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);                //deactive the fireball
            Destroy(other.gameObject, 3);               //destroy the enemy
        }
        else if (other.gameObject.CompareTag("Rocks"))  //if collides with rocks
        {
            gameObject.SetActive(false);                //deactivate the fireball
        }
    }
      
}
