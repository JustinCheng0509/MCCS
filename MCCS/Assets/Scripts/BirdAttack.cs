using UnityEngine;

public class Bird : MonoBehaviour
{
    public int damage = 10; // 鸟对敌人造成的伤害


    public void SetVelocity(Vector2 velocity)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰到的是否是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 对敌人造成伤害
            collision.gameObject.GetComponent<Enemy>().Damaged(damage, 0);
        }
    }

}
