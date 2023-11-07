using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMidPoint : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private Vector3 pos;
    private Vector3 player1Pos;
    private Vector3 player2Pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 0, -3);
    }

    // Update is called once per frame
    void Update()
    {
        player1Pos = player1.transform.position;
        player2Pos = player2.transform.position;
        pos.x = player1Pos.x + (player2Pos.x - player1Pos.x)/2;
        pos.y = player1Pos.y + (player2Pos.y - player1Pos.y)/2;
        transform.position = pos;
    }
}
