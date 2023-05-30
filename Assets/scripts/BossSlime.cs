using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
    public SpriteRenderer rend;

    public List<GameObject> stuff;

    public void TurnOn()
    {
        rend.enabled = true;
        GetComponent<Collider2D>().enabled = true;

        if(stuff.Count > 0)
        {
            foreach(var thing in stuff)
            {
                if(thing.GetComponent<health>())
                {
                    thing.GetComponent<health>().TakeDamage(999);
                }
                if(thing.GetComponent<PlayerHealth>())
                {
                    thing.GetComponent<PlayerHealth>().TakeAHit();
                }
            }
            stuff.Clear();
        }
    }

    private void Update()
    {
        foreach(var stu in stuff)
        {
            stu.GetComponent<Rigidbody2D>().AddForce(-(stu.GetComponent<Rigidbody2D>().velocity / 2));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>())
        {
            stuff.Add(collision.gameObject);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stuff.Contains(collision.gameObject))
            stuff.Remove(collision.gameObject);
    }
}
