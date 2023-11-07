using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    public float Speed;
    public float StartWaitTime;
    private float WaitTimme;

    public Transform MovPos;
    public Transform LeftDownPos;
    public Transform RightUpPos;



    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        WaitTimme = StartWaitTime;
        MovPos.position = GetRandomPos();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        transform.position = Vector2.MoveTowards(transform.position, MovPos.position, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, MovPos.position) < 0.1f)
        {
            if (WaitTimme <= 0)
            {
                MovPos.position = GetRandomPos();
                WaitTimme = StartWaitTime;
            }
            else
            {
                WaitTimme -= Time.deltaTime;
            }
        }
    }


    Vector2 GetRandomPos()
    {
        Vector2 rndmPos = new Vector2(Random.Range(LeftDownPos.position.x, RightUpPos.position.x), Random.Range(LeftDownPos.position.y, RightUpPos.position.y));
        return rndmPos;
    }


}
