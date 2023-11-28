using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMate : Player
{

    public GameObject cannonballPrefab;
    public float spawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        horizontalName = "P2_Horizontal";
        jumpName = "P2_Jump";
        attackName = "P2_Attack";
        specialName = "P2_Special";
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
