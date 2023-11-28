using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyS : Enemy
{
    public float moveSpeed;
    private Rigidbody2D _rigidBody;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float movementSmoothing = 0.05f;
    private bool _isRight = true;

    public float Speed;
    public float Radius;
    private float Distance;
    private Animator NavyAttackAni;
    private PolygonCollider2D Collider2D;
    public float StartTime;
    public float time;

    private GameObject[] PlayerTransformPos;
 
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        PlayerTransformPos = GameObject.FindGameObjectsWithTag("Player");
        _rigidBody = GetComponent<Rigidbody2D>();

    }

    public void Move(float direction)
    {

        Vector3 targetVelocity = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, _rigidBody.velocity.y);
        //Debug.Log(targetVelocity);
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, movementSmoothing);

        if (direction > 0 && _isRight)
        {
            FlipDirection();
        }
        else if (direction < 0 && !_isRight)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        _isRight = !_isRight;

        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

       // base.Update();

        if (PlayerTransformPos != null)
        {
            Distance = (transform.position - PlayerTransformPos[0].transform.position).magnitude;

            if (Distance < Radius)
            {
                if (PlayerTransformPos[0].transform.position.x < transform.position.x)//means target is to the left, so move negative x
                {
                    Move(-1);
                }
                else {
                    Move(1);
                }
            }
            else {
                Distance = (transform.position - PlayerTransformPos[1].transform.position).magnitude;

                if (Distance < Radius)
                {
                    if (PlayerTransformPos[1].transform.position.x < transform.position.x)//means target is to the left, so move negative x
                    {
                        Move(-1);
                    }
                    else
                    {
                        Move(1);
                    }

                }
            }



        }
    }




}
