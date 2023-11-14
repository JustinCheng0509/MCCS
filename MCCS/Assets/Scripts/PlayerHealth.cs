using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    private Renderer myRender;
    public int blinks;
    public float time;
    private Animator HitAni;
    // Start is called before the first frame update
    void Start()
    {
        HitAni = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        myRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        HitAni.SetTrigger("Hit");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        BlinkPlayer(blinks,time);
    }

    public void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        { 
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
