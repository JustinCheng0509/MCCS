using UnityEngine;

public class BirdSummoner : MonoBehaviour
{
    public GameObject birdPrefab; 
    public GameObject character; 
    public float speed = 5f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SummonBirdAtCharacter();
        }
    }

    void SummonBirdAtCharacter()
    {
     
        Vector2 characterPosition = character.transform.position;

        
        GameObject bird = Instantiate(birdPrefab, characterPosition, Quaternion.identity);
       

      
        Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; 
            Vector2 direction = character.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.velocity = direction * speed; 
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
