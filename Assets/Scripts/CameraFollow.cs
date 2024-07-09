using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 rotation;
    [SerializeField] float speed;
    

    public void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, player.position + offset, speed);
        //transform.position = Vector3.MoveTowards(transform.position, player.position + offset, speed);

        transform.rotation = Quaternion.Euler(rotation);
    }
}
