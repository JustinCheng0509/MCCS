using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) { 
            Destroy(gameObject); // Ïú»ÙÅÚµ¯
        }
    }
}
