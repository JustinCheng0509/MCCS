using UnityEngine;

public class BirdSummoner : MonoBehaviour
{
    public GameObject birdPrefab; // 鸟的Prefab
    public GameObject character; // 角色的GameObject，在Inspector中设置
    public float speed = 5f; // 鸟的速度

    void Update()
    {
        // 检测空格键是否被按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SummonBirdAtCharacter();
        }
    }

    void SummonBirdAtCharacter()
    {
        // 获取角色的当前位置
        Vector2 characterPosition = character.transform.position;

        // 创建鸟的实例
        GameObject bird = Instantiate(birdPrefab, characterPosition, Quaternion.identity);
       

        // 设置鸟的速度和方向
        Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // 使鸟不受物理力的影响
            Vector2 direction = character.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.velocity = direction * speed; // 设置鸟的速度和方向
        }

        // 设置Collider为触发器
        Collider2D collider = bird.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void LateUpdate()
    {
        foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Bird"))
        {
            // 将鸟的位置转换为视口坐标
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(bird.transform.position);

            // 如果鸟不在视口范围内，销毁它
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                Destroy(bird);
            }
        }
    }
}
