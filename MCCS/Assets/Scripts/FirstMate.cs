using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMate : Player
{
    // Start is called before the first frame update
    void Start()
    {
        horizontalName = "P2_Horizontal";
        jumpName = "P2_Jump";
        attackName = "P2_Attack";
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
