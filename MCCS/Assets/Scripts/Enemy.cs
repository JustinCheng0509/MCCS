using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int maxHealth;
    private PlayerHealth playerhealth;

    // Start is called before the first frame update
    public void Start()
    {

        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damaged(int damage)
    {

        health -= damage;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerhealth != null)
            {
                playerhealth.DamagePlayer(damage);
            }

        }
    }
}
