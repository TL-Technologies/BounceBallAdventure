using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x -3.0f, player.position.y + 10.0f, -10); // Camera follows the player but 6 to the right
    }
}
