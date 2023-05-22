using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed;
    public Transform toFollow;

    void FixedUpdate()
    {
        Vector3 pos = toFollow.position;
        pos.x = 0;
        pos.z = -10;
        pos.y = Mathf.Clamp(pos.y, -10, 10);

       transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
