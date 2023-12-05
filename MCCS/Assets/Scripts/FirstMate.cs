using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMate : Player
{

    public GameObject cannonballPrefab;
    public float spawnDistance;
    public GameObject otherPlayer;
    public float maxDistance = 25.0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        horizontalName = "P2_Horizontal";
        jumpName = "P2_Jump";
        attackName = "P2_Attack";
        specialName = "P2_Special";
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    private void FixedUpdate()
    {
        UpdateGrounded();

        // 先进行距离检查和移动限制
        float distance = Vector3.Distance(transform.position, otherPlayer.transform.position);
        if (distance > maxDistance)
        {
            bool isLeft = transform.position.x < otherPlayer.transform.position.x;
            float moveDirection = Input.GetAxis(horizontalName); // 使用角色特定的输入名称

            if ((isLeft && moveDirection < 0) || (!isLeft && moveDirection > 0))
            {
                // 这里暂时停止移动
                xMovement = 0; // 假设xMovement是控制水平移动的变量
            }
        }

        // 然后执行移动
        Move(xMovement * Time.fixedDeltaTime, _didJump);
        _didJump = false;
    }

    protected override void SpecialAttack()
	{
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, Camera.main.nearClipPlane));
        spawnPosition.z = 0;
        float direction;
        if (transform.localScale.x < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        spawnPosition.x = transform.position.x + spawnDistance * direction;

        spawnPosition += Vector3.forward * spawnDistance;

        Instantiate(cannonballPrefab, spawnPosition, Quaternion.identity);
    }
}
