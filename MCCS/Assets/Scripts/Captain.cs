using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Player
{

    // Start is called before the first frame update
    void Start()
    {
        horizontalName = "P1_Horizontal";
        jumpName = "P1_Jump";
        attackName = "P1_Attack";
        specialName = "P1_Special";
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

}
