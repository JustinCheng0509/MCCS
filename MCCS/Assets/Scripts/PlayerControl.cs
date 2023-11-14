using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float RunSpeed;
    public float JumpSpeed;
    public string HorizontalInput = "Horizontal"; // 新增 - 用于区分输入轴
    public string JumpInput = "Jump"; // 新增 - 用于区分跳跃按钮s

    private Rigidbody2D Myrigidbody2D;
    private Animator MyAnimator;
    private BoxCollider2D Feet;

    private bool IsGround;
    public bool IsJump;
    public bool JumpPress;
    public int JumpNum;

    void Start()
    {
        Myrigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        Feet = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown(JumpInput) && CountJumps >= 0)
        {
            MyAnimator.SetBool("Jump", true);
            JumpPress = true;
        }
    }

    private void FixedUpdate()
    {
        Jump();
        Flip();
        CheckGround();
        JumpSwitchAni();
        Run();
        //Attack();
    }

    private void Flip()
    {
        bool PlayerHasXSpeed = Mathf.Abs(Myrigidbody2D.velocity.x) > Mathf.Epsilon;
        if (PlayerHasXSpeed)
        {
            if (Myrigidbody2D.velocity.x < 0f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (Myrigidbody2D.velocity.x >= 0f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void Run()
    {
        float MovDirect = Input.GetAxis(HorizontalInput);
        Vector2 PlayerVector = new Vector2(MovDirect * RunSpeed, Myrigidbody2D.velocity.y);
        Myrigidbody2D.velocity = PlayerVector;
        bool PlayerHasXSpeed = Mathf.Abs(Myrigidbody2D.velocity.x) > Mathf.Epsilon;
        MyAnimator.SetBool("Run", PlayerHasXSpeed);
    }

    int CountJumps = 0;
    private void Jump()
    {
        if (IsGround)
        {
            CountJumps = JumpNum;
            IsJump = false;
        }
        if (JumpPress && IsGround)
        {
            IsJump = true;
            Myrigidbody2D.velocity = new Vector2(Myrigidbody2D.velocity.x, JumpSpeed);
            CountJumps--;
            JumpPress = false;
        }
        else if (JumpPress && CountJumps > 0 && !IsGround)
        {
            Myrigidbody2D.velocity = new Vector2(Myrigidbody2D.velocity.x, JumpSpeed);
            CountJumps--;
            JumpPress = false;
        }
    }

    private void JumpSwitchAni()
    {
        MyAnimator.SetBool("Idle", false);
        if (MyAnimator.GetBool("Jump"))
        {
            if (Myrigidbody2D.velocity.y < 0.0f)
            {
                MyAnimator.SetBool("Jump", false);
            }
        }
        else if (IsGround)
        {
            MyAnimator.SetBool("Idle", true);
        }
    }

    /* void Attack()
    {
       if(Input.GetKeyDown(AttackInput))
        {
            MyAnimator.SetTrigger("Attack");
        }
    }*/

    private void CheckGround()
    {
        IsGround = Feet.IsTouchingLayers(LayerMask.GetMask("Platform"));
        //Debug.Log(IsGround);
    }
}
