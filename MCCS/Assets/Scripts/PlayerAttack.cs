using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int Damage;
    private Animator AttackAni;
    private PolygonCollider2D Collider2D;
    public float StartTime;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        AttackAni = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }



    private void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {

            AttackAni.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }


    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(StartTime);
        Collider2D.enabled = true;
        StartCoroutine(DisableHitBox());
    }

    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(time);
        Collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Damaged(Damage);
        }
    }

}
