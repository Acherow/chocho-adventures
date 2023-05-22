using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float MaxHP;
    public float HP;
    public GameObject gibs;

    private void Start()
    {
        HP = MaxHP;
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if(HP <= 0)        
            Die();
        if(amount > 0)
        StartCoroutine(DmgEffect());
    }

    public IEnumerator DmgEffect()
    {
        Shader shaderGUItext = Shader.Find("GUI/Text Shader");
        Shader shaderSpritesDefault = Shader.Find("Sprites/Default");

        SpriteRenderer[] parts = GetComponentsInChildren<SpriteRenderer>();

        foreach (var part in parts)
        {
            part.material.shader = shaderGUItext;
            part.color = Color.white;
        }
        yield return new WaitForSeconds(0.05f);
        foreach (var part in parts)
        {
            part.material.shader = shaderSpritesDefault;
            part.color = Color.white;
        }
    }

    void Die()
    {
        GetComponent<Animator>().SetTrigger("death");
    }

    public void DieEffect()
    {
        Instantiate(gibs, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
