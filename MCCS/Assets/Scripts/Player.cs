using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour
{
    public Healthbar healthbar;
    private Rigidbody2D _rigidBody;
    protected Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private bool didTakeDamage = false;
    public float iFrames;

    [SerializeField] protected BoxCollider2D _boxCollider;
    [SerializeField] protected CircleCollider2D _feetCollider;
    [SerializeField] private float movementSmoothing = 0.05f;
    


    [SerializeField] protected bool _isGrounded = false;
    [SerializeField] protected string horizontalName;
    [SerializeField] protected string jumpName;
    [SerializeField] protected string attackName;
    [SerializeField] protected string specialName;



    private bool _isFootDisabled = false;
    private Vector3 _velocity = Vector3.zero;
    private bool _isRight = false;
    protected bool _didJump = false;


    public float jumpForce;
    public float moveSpeed;
    public float xMovement = 0f;

    protected bool canAttack = true;
    public float attackCooldown = 0f;

    private Vector2 screenBounds;
    private float _width;
    private float _height;
    

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _feetCollider = GetComponent<CircleCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _width = _spriteRenderer.bounds.size.x/2;
        _height = _spriteRenderer.bounds.size.y/2;
    }

	protected abstract void SpecialAttack();


    protected void UpdateGrounded() {
        if (!_isFootDisabled) {
            //Debug.Log("STILL DISABLED");
            //Debug.Log(_rigidBody.velocity.y);
            if (_rigidBody.velocity.y==0 || _feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) //means player is touching platform or floor
            {
                // Debug.Log("WHAT");
                //_feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))
                _isGrounded = true;
                _didJump = false;
                _animator.SetBool("DidJump", false); // first frame character landed, stop jumping animation
            }
            else
            {
                _isGrounded = false;
            }
        }
        
    }

    public void Move(float move, bool didJump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, _rigidBody.velocity.y);
        //Debug.Log(targetVelocity);
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, movementSmoothing);

        if (move > 0 && _isRight)
        {
            FlipDirection();
        } else if (move < 0 && !_isRight)
        {
            FlipDirection();
        }

        //only jump if the button is pressed and character is grounded
        if (didJump && _isGrounded) {
            _isGrounded = false; //set grounded to false (player about to jump)
            _rigidBody.AddForce(new Vector2(0f, jumpForce)); //makes player jump
        }
	}

    private void FlipDirection() {
        _isRight = !_isRight;

        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    public void HandleInput()
    {
        xMovement = Input.GetAxis(horizontalName) * moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(xMovement));

        if (Input.GetButtonDown(jumpName))
        {
            //Debug.Log("JUMPED");
            _didJump = true;
            _feetCollider.enabled = false;
            _isFootDisabled = true;
            _animator.SetBool("DidJump", true);
            StartCoroutine(EnableFootCollision());
        }

        if (Input.GetButtonDown(attackName)) {
            if (!_animator.GetBool("DidAttack"))
            Attack();
        }

        if (Input.GetButtonDown(specialName)) {
            SpecialAttack();
        }
    }

    private IEnumerator EnableFootCollision()
    {
        yield return new WaitForSeconds(0.2f);
        _feetCollider.enabled = true;
        _isFootDisabled = false;
    }

    private void Attack() {
        if (canAttack)
        {
            canAttack = false;
            _animator.SetBool("DidAttack", true);
            StartCoroutine(AttackCD());
        }
    }

    private IEnumerator AttackCD() {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    public void OnAttackAnimationEnd()
    {
        _animator.SetBool("DidAttack", false);
    }

    private IEnumerator TookDamage() {
        didTakeDamage = true;
        health -= 10;
        if (health < 0) GameManager.Instance.EndGame();

        healthbar.UpdateHealthBar(health, maxHealth);
        _spriteRenderer.color = Color.red;
        moveSpeed = 60f;
        yield return new WaitForSeconds(iFrames);
        moveSpeed = 40f;
        _spriteRenderer.color = Color.white;
        didTakeDamage = false;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            if (!didTakeDamage)
            {
                Physics2D.IgnoreCollision(_feetCollider, other);
                Physics2D.IgnoreCollision(_feetCollider, other.gameObject.GetComponent<CircleCollider2D>());
                StartCoroutine(TookDamage());
                Physics2D.IgnoreCollision(_feetCollider, other.gameObject.GetComponent<CircleCollider2D>(), false);

            }
        }
    }

	public void LateUpdate()
	{
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + _width, screenBounds.x * -1 - _width);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + _height, screenBounds.y * -1 - _height);
        //Debug.Log(viewPos);
        //transform.position = viewPos;
    }
}
