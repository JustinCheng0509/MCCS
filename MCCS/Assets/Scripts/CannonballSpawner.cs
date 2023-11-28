using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CannonballSpawner : MonoBehaviour
{
    public GameObject cannonballPrefab; // 炮弹预设
    public float spawnDistance = 5f; // 生成距离

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, Camera.main.nearClipPlane));
            spawnPosition.z = 0; // 确保Z坐标为0，因为是2D游戏
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

            spawnPosition += Vector3.forward * spawnDistance; // 根据玩家视角前方一定距离生成

            Instantiate(cannonballPrefab, spawnPosition, Quaternion.identity);
        }
    }

 }

