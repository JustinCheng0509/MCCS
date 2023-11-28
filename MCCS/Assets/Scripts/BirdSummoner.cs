using UnityEngine;

public class BirdSummoner : MonoBehaviour
{
    public GameObject birdPrefab; // ���Prefab
    public GameObject character; // ��ɫ��GameObject����Inspector������
    public float speed = 5f; // ����ٶ�

    void Update()
    {
        // ���ո���Ƿ񱻰���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SummonBirdAtCharacter();
        }
    }

    void SummonBirdAtCharacter()
    {
        // ��ȡ��ɫ�ĵ�ǰλ��
        Vector2 characterPosition = character.transform.position;

        // �������ʵ��
        GameObject bird = Instantiate(birdPrefab, characterPosition, Quaternion.identity);
       

        // ��������ٶȺͷ���
        Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // ʹ������������Ӱ��
            Vector2 direction = character.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.velocity = direction * speed; // ��������ٶȺͷ���
        }

        // ����ColliderΪ������
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
            // �����λ��ת��Ϊ�ӿ�����
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(bird.transform.position);

            // ��������ӿڷ�Χ�ڣ�������
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                Destroy(bird);
            }
        }
    }
}
