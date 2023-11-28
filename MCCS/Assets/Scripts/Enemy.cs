using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health1;
    public int maxHealth1;
    public int health2;
    public int maxHealth2;

    public int damage;
    private PlayerHealth playerhealth;

    /*public float breakPointP1;
    public float breakPointP2;
    public float breakPointBoth;*/
    public int typeNum;

    public Healthbar[] healthbars;

    // Start is called before the first frame update
    public void Start()
    {
        //playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        health1 = maxHealth1;
        health2 = maxHealth2;
        SetUpHealthbars();
    }

    // Update is called once per frame
    public void Update()
    {
        if (typeNum == 2)//enemy with both healthbars
        {
            if (health1 <= 0 && health2 <= 0) Destroy(gameObject);
        }else  if (health1 <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetUpHealthbars()
    {
        healthbars = GetComponentsInChildren<Healthbar>();
        healthbars[0].UpdateHealthBar(health1, maxHealth1);
        if(typeNum==2)healthbars[1].UpdateHealthBar(health2, maxHealth2);
    }

    public void Damaged(int damage, int healthbarNum)
    {
        if (typeNum == 4) {
            health1 -= damage;
            healthbars[0].UpdateHealthBar(health1, maxHealth1);
        }
        if (typeNum == 2) //enemy with both healthbars
        {
            if (healthbarNum == 0)
            {
                health1 -= damage;
                if (health1 < 0) health1 = 0;
                healthbars[0].UpdateHealthBar(health1, maxHealth1);
            }
            else
            {
                health2 -= damage;
                if (health2 < 0) health2 = 0;
                healthbars[1].UpdateHealthBar(health2, maxHealth2);
            }
        }
        else if (healthbarNum == typeNum) {
            health1 -= damage;
            healthbars[0].UpdateHealthBar(health1, maxHealth1);
        }
/*
        if (typeNum != 2)
        {
            health1 -= damage;
            healthbars[0].UpdateHealthBar(health1, maxHealth1);
        }
        else {
            if (healthbarNum == 0)
            {
                health1 -= damage;
                healthbars[0].UpdateHealthBar(health1, maxHealth1);
                Debug.Log("Green decrease");

            }
            else
            {
                health2 -= damage;
                healthbars[1].UpdateHealthBar(health2, maxHealth2);
                Debug.Log("Orange decrease");
            }
        }*/
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        /*
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerhealth != null)
            {
                playerhealth.DamagePlayer(damage);
            }

        }*/
    }

    /*private void SetEnemyType() {
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
    }*/
}
