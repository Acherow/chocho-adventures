using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerClone clone;
    public int hits;

    public float iframes;
    float ifram;

    private void Update()
    {
        if (ifram > 0)
            ifram -= Time.deltaTime;
    }

    public void TakeAHit()
    {
        if (GetComponent<PlayerMove>().animFrozen || ifram > 0)
            return;

        hits++;
        if(hits >= 1)
        {
            if (clone.freehit)
            {
                hits = 0;
                clone.Activate();
                ifram = iframes;
                StartCoroutine(Blink());
            }
            else
            {
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(var rend in GetComponentsInChildren<SpriteRenderer>())
        {
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0.5f);
        }
        yield return new WaitForSeconds(0.1f);
        foreach (var rend in GetComponentsInChildren<SpriteRenderer>())
        {
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1f);
        }

        if(ifram > 0)        
            StartCoroutine(Blink());        
    }
}
