using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpan : MonoBehaviour
{
    void Start()
    {
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
