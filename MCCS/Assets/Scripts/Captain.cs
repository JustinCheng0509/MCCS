using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Player
{

    public float birdSpeed;
    public GameObject birdPrefab;
    public GameObject otherPlayer;
    public float maxDistance = 25.0f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        horizontalName = "P1_Horizontal";
        jumpName = "P1_Jump";
        attackName = "P1_Attack";
        specialName = "P1_Special";

        Physics2D.IgnoreCollision(_feetCollider, otherPlayer.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(_feetCollider, otherPlayer.GetComponent<BoxCollider2D>());
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
        GameObject bird = Instantiate(birdPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            Vector2 direction;
            if (transform.localScale.x > 0)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
                bird.transform.localScale = new Vector3(bird.transform.localScale.x * (-1), bird.transform.localScale.y, bird.transform.localScale.z);
            }
            rb.velocity = direction * birdSpeed;
        }


        Collider2D collider = bird.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void LateUpdate()
    {
        base.LateUpdate();
        foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Bird"))
        {

            Vector3 screenPoint = Camera.main.WorldToViewportPoint(bird.transform.position);

            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                Destroy(bird);
            }
        }
    }
}
