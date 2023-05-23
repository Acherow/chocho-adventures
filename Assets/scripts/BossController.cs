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
        Instantiate(slimeAttack, transform.position + (Random.Range(0,2) == 1?Vector3.right*5:-Vector3.right*5), Quaternion.identity);
    }

    void Eye()
    {
        Instantiate(EyeAttack, transform.position, Quaternion.Euler(new Vector3(0,0,Random.Range(200,160))));
    }

}
