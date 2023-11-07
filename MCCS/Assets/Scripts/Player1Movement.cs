    using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public float Jumpforce = 10f;
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //left & right movement
        var movement = Input.GetAxis("P1_Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        //Jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rigidbody.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
        }
    }
}
