using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = (player.position + offset);
    }
}
