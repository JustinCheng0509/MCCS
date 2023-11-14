using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int Damage;
    private Animator AttackAni;
    private PolygonCollider2D Collider2D;
    public float StartTime;
    public float time;
    public string AttackInput = "Attack1";

    void Start()
    {
        AttackAni = GetComponentInParent<Animator>(); // 修改为直接获取当前对象的动画组件
        Collider2D = GetComponent<PolygonCollider2D>();
   
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown(AttackInput))
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
