using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CannonballSpawner : MonoBehaviour
{
    public GameObject cannonballPrefab; // �ڵ�Ԥ��
    public float spawnDistance = 5f; // ���ɾ���

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, Camera.main.nearClipPlane));
            spawnPosition.z = 0; // ȷ��Z����Ϊ0����Ϊ��2D��Ϸ
            float direction;
            if (transform.localScale.x <0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            spawnPosition.x = transform.position.x + spawnDistance * direction;

            spawnPosition += Vector3.forward * spawnDistance; // ��������ӽ�ǰ��һ����������

            Instantiate(cannonballPrefab, spawnPosition, Quaternion.identity);
        }
    }

 }

