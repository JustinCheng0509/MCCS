using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int Damage;
    private PolygonCollider2D Collider2D;
    public float StartTime;
    public float time;
    public string AttackInput1 = "P1_Attack";
    public int playerNum;

    public bool didAttack = false;

    void Start()
    {
        Collider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (!didAttack && Input.GetButtonDown(AttackInput1))
        {
            StartCoroutine(StartAttack());
        }

    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(StartTime);
        Collider2D.enabled = true;
        didAttack = true;
        StartCoroutine(DisableHitBox());
    }

    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(time);
        Collider2D.enabled = false;
        didAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy temp = other.GetComponent<Enemy>();
            if (temp.typeNum==2 || playerNum == temp.typeNum) {
                other.GetComponent<Enemy>().Damaged(Damage, playerNum);
            }
        }
    }
}
