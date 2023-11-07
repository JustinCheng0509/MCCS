using UnityEngine;
using System.Collections;

public class Player2Movement : MonoBehaviour
{

    [SerializeField] private LayerMask platformMask;

    public float MovementSpeed = 5f;
    public float Jumpforce = 10f;
    public bool didJump = false;
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //left & right movement
        var movement2 = Input.GetAxis("P2_Horizontal");
        transform.position += new Vector3(movement2, 0, 0) * Time.deltaTime * MovementSpeed;

        //Jump
        if (!didJump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            didJump = true;
            _rigidbody.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
        }

        //fall through platform
        Collider2D platformCollider = getPlatform();
        if (platformCollider != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Debug.Log("pressing s");
            Physics2D.IgnoreCollision(_collider, platformCollider);  //ignore platform collision to fall through
            StartCoroutine(EnablePlatformCollision(platformCollider)); //start co-routine to enable collider again
        }
    }

    //return the collider object of platfom the player is standing on, null if not
    private Collider2D getPlatform()
    {
        float buffer = 3f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, buffer, platformMask);
        return raycastHit.collider;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ignore other player
        if (collision.gameObject.CompareTag("Player")) Physics2D.IgnoreCollision(_collider, collision.collider);
        //can only jump if you landed (colliding with floor or platform)
        if (didJump && (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Platform")))
        {
            didJump = false;
        }
    }

    //re-enables collider between player and colliding platform
    private IEnumerator EnablePlatformCollision(Collider2D platformCollider)
    {
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(_collider, platformCollider, false);

    }
}
