using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyS : Enemy
{

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

    }

    // Update is called once per frame
    public void Update()
    {

        base.Update();

        if (PlayerTransformPos != null)
        {
            Distance = (transform.position - PlayerTransformPos[0].transform.position).magnitude;

            if (Distance < Radius)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlayerTransformPos[0].transform.position, Speed * Time.deltaTime);

            }
            else {
                Distance = (transform.position - PlayerTransformPos[1].transform.position).magnitude;

                if (Distance < Radius)
                {
                    transform.position = Vector2.MoveTowards(transform.position, PlayerTransformPos[1].transform.position, Speed * Time.deltaTime);

                }
            }



        }
    }




}
