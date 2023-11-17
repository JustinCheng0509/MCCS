using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int health2;
    public int damage;
    public int maxHealth;
    public int maxHealth2;
    private PlayerHealth playerhealth;

    public float breakPointP1;
    public float breakPointP2;
    public float breakPointBoth;
    public int typeNum;

    public Healthbar[] healthbars;

    // Start is called before the first frame update
    public void Start()
    {
        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        maxHealth = health;
        health2 = health;
        maxHealth2 = maxHealth;
        SetUpHealthbars();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetUpHealthbars()
    {
        healthbars = GetComponentsInChildren<Healthbar>();
        SetEnemyType();
    }

    public void Damaged(int damage, int healthbarNum)
    {

        if (typeNum != 2)
        {
            health -= damage;
            healthbars[0].UpdateHealthBar(health, maxHealth);
        }
        else {
            if (healthbarNum == 0)
            {
                health -= damage;
                healthbars[0].UpdateHealthBar(health, maxHealth);
                Debug.Log("Green decrease");

            }
            else
            {
                health2 -= damage;
                healthbars[1].UpdateHealthBar(health2, maxHealth2);
                Debug.Log("Orange decrease");
            }
        }
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

    private void SetEnemyType() {
        float seed = Random.value;
        if (seed >= 0.0 && seed <= breakPointP1)
        {
            typeNum = 0;
        }
        else if (seed > breakPointP1 && seed <= breakPointP2)
        {
            typeNum = 1;
        }
        else {
            typeNum = 2;
        }


        switch (typeNum) {
            case 0:
                healthbars[1].gameObject.SetActive(false);
                healthbars[0].UpdateHealthBar(health, maxHealth);
                break;
            case 1:
                healthbars[1].gameObject.SetActive(false);
                healthbars[0].ChangeColor();
                healthbars[0].UpdateHealthBar(health, maxHealth);
                break;
            case 2:
                healthbars[0].UpdateHealthBar(health, maxHealth);
                healthbars[1].UpdateHealthBar(health2, maxHealth2);
                
                break;
        }
    }
}
