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
        camerOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer(); // camera will follow the player at fix offSet
    }

    void FollowPlayer()
    {
        Vector3 newPos = player.transform.position + camerOffset;
        transform.position = newPos;

        if (lookAtTarget)
        {
            this.transform.LookAt(player);
        }

    }
}
