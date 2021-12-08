using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    private Vector3 camerOffset;
    private bool lookAtTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        //get the camera offset positin by substracting player postion from camera position
        camerOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer(); // camera will follow the player at fix offSet
    }

    void FollowPlayer()
    {
        //add the camera offset position to player position to get new position
        Vector3 newPos = player.transform.position + camerOffset;

        //set current transform position to new position
        transform.position = newPos;

        if (lookAtTarget)
        {
            this.transform.LookAt(player); // look at the player
        }

    }
}
