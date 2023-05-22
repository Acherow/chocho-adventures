using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float length;
    public float damage;
    GameObject fireff;

    private void Start()
    {
        StartCoroutine(FireTick());
        fireff = Instantiate(Resources.Load<GameObject>("fire effect"), transform.position, Quaternion.identity, transform);
    }

    private void FixedUpdate()
    {
        length -= Time.deltaTime;
        if (length <= 0)
        {
            Destroy(fireff);
            Destroy(this);
        }
    }

    IEnumerator FireTick()
    {
        yield return new WaitForSeconds(0.25f);
        GetComponent<health>().TakeDamage(damage);
        StartCoroutine(FireTick());
    }
}
