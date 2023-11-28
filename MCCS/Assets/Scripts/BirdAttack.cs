using UnityEngine;

public class Bird : MonoBehaviour
{
    public int damage = 10; // ��Ե�����ɵ��˺�


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
        // ����������Ƿ��ǵ���
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // �Ե�������˺�
            collision.gameObject.GetComponent<Enemy>().Damaged(damage, 0);
        }
    }

}
