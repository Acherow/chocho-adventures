using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject lightprefab;

    public float range;
    public float damage;
    public float dmgreduction;
    public float count;
    public bool ignoredmg;

    void Start()
    {
        lightprefab = Resources.Load<GameObject>("LightningVisual");
        if (count > 0)
            CheckNearby();
        StartCoroutine(Act());
    }

    void CheckNearby()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(var col in cols)
        {
            if(col.GetComponent<health>() && !col.GetComponent<Lightning>())
            {
                Lightning l = col.gameObject.AddComponent<Lightning>();
                l.range = range;
                l.damage = damage * dmgreduction;
                l.dmgreduction = dmgreduction;
                l.count = count - 1;
                GameObject g = Instantiate(lightprefab, transform.position + (col.transform.position - transform.position)/2, Quaternion.LookRotation(Vector3.forward, (col.transform.position - transform.position)));
                g.GetComponent<SpriteRenderer>().size = new Vector2(1, Vector2.Distance(transform.position, col.transform.position));
            }
        }
    }

    IEnumerator Act()
    {
        yield return new WaitForSeconds(0.05f);
        if (!ignoredmg)
            GetComponent<health>().TakeDamage(damage);
        yield return new WaitForSeconds(count * 0.05f);
        Destroy(this);
    }
}
