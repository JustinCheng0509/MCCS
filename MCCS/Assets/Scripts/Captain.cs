using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Player
{

    public float birdSpeed;
    public GameObject birdPrefab;
    public GameObject otherPlayer;


    // Start is called before the first frame update
    void Start()
    {
        horizontalName = "P1_Horizontal";
        jumpName = "P1_Jump";
        attackName = "P1_Attack";
        specialName = "P1_Special";

        Physics2D.IgnoreCollision(_feetCollider, otherPlayer.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(_feetCollider, otherPlayer.GetComponent<BoxCollider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
	{
        UpdateGrounded();
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
