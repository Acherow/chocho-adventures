using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    public GameObject enemy;
    public int loop;

    public void Spawn()
    {
        GameObject g = Instantiate(enemy, transform.position, Quaternion.identity);
        g.GetComponent<health>().MaxHP *= loop;
        FindObjectOfType<EnemyWaves>().AliveEnemies.Add(g);
        Destroy(gameObject);
    }
}
