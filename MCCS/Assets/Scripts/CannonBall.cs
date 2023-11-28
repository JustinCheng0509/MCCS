using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) { 
            Destroy(gameObject); // 销毁炮弹
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰到的是否是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damaged(damage, 1);
        }
    }
}
