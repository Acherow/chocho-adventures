using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject slimeAttack;
    public GameObject EyeAttack;
    public float idletimer;

    private void Start()
    {
        idletimer = 10;
    }

    private void Update()
    {
        
    }

    void Slime()
    {
        Instantiate(slimeAttack, transform.position, Quaternion.identity);
    }

    void Eye()
    {

    }

}
