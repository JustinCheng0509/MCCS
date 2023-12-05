using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Player : MonoBehaviour
{

    public Healthbar healthbar;
    public Healthbar abilityBar;
    private Rigidbody2D _rigidBody;
    protected Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public bool isAbilityCD = false;

    public float abilityCD = 5.0f;

    [SerializeField] private float maxMana = 50;
    [SerializeField] private float mana;
    [SerializeField] private float manaPS;
    [SerializeField] private float manaPerCoin;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private bool didTakeDamage = false;
    public float iFrames;

    [SerializeField] protected BoxCollider2D _boxCollider;
    [SerializeField] protected CircleCollider2D _feetCollider;
    [SerializeField] private float movementSmoothing = 0.05f;


    [SerializeField] private LayerMask groundMask;
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
        mana = maxMana;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _width = _spriteRenderer.bounds.size.x/2;
        _height = _spriteRenderer.bounds.size.y/2;
    }

	protected abstract void SpecialAttack();


    protected void UpdateGrounded() {
        bool wasGrounded = _isGrounded;
        _isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_feetCollider.transform.position, _feetCollider.radius+1f, groundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && _rigidBody.velocity.y <= 0.1f)
            { //make sure not colliding with self
                //if we get in here it means we hit groundMask
                _isGrounded = true;
                if (!wasGrounded)
                {
                    //means first frame of land
                    _animator.SetBool("DidJump", false);
                }
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
            _animator.SetBool("DidJump", true);
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
        }

        if (Input.GetButtonDown(attackName)) {
            if (!_animator.GetBool("DidAttack"))
            Attack();
        }

        if (!isAbilityCD && Input.GetButtonDown(specialName)) {
            isAbilityCD = true;
            mana = 0;
            abilityBar.UpdateHealthBar(mana, maxMana);
            SpecialAttack();
            

            //StartCoroutine(SetAbilityCD());
        }

        if (isAbilityCD) {
            mana += manaPS * Time.deltaTime;
            UpdateAbilityBar();
        }
    }

    private void UpdateAbilityBar() {
        if (mana >= maxMana)
        {
            mana = maxMana;
            isAbilityCD = false;
        }
        abilityBar.UpdateHealthBar(mana, maxMana);
    }

    private IEnumerator SetAbilityCD() {
        yield return new WaitForSeconds(abilityCD);
        isAbilityCD = false;
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

	public void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Coin")
        {
           // Debug.Log("Coin Collected");
            mana += manaPerCoin;
            UpdateAbilityBar();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Treasure")
        {

            GameManager.Instance.UpdateGameState(GameManager.GameState.EndScreen);
        }
    }


	public void LateUpdate()
	{
        Vector3 viewPos = transform.position;
/*        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + _width, screenBounds.x * -1 - _width);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + _height, screenBounds.y * -1 - _height);
*/        if(viewPos.x< -10)
        {
            viewPos.x = -10;
        }
        //Debug.Log(viewPos);
        transform.position = viewPos;
    }
}
