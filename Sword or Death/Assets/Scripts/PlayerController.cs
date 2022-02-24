using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource trampolineSound;
    [SerializeField] private AudioSource leverArmSound;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private float speedX = -1f;
    [SerializeField] private float _launchForce = 0;

    private float _horizontal = 0f;
    private bool _isFacingRight = true;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFinish = false;
    private bool _isLeverArm = false;

    private Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;
    private FixedJoystick _fixedJoystick;
    private PlayerHealth _playerHealth;


    const float speedXMultiplier = 50f;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _fixedJoystick = GameObject.FindGameObjectWithTag("Fixed Joystick").GetComponent<FixedJoystick> ();
        _leverArm = FindObjectOfType<LeverArm>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal"); // -1 : 1
        _horizontal = _fixedJoystick.Horizontal;
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
       if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
       */
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, 350f));
            _isGround = false;
            _isJump = false;
        }

        if (_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0f && _isFacingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    public void Jump()
    {
        if (!_isGround) return;
        _isJump = true;
        jumpSound.Play();
    }

    public void Interact()
    {
        if (_isFinish)
        {
            _finish.FinishLevel();
        }
        if (_isLeverArm)
        {

            _leverArm.ActivateLeverArm();
            leverArmSound.Play();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
        if (other.gameObject.CompareTag("Trampoline"))
        {
            _isGround = false;
            _rb.velocity = Vector2.up * _launchForce;
            trampolineSound.Play();
        }
        if (other.gameObject.name.Equals("VerticalPlatform"))
        {
            this.transform.parent = other.transform;
        }
        if (other.gameObject.name.Equals("HorizontalPlatform"))
        {
            this.transform.parent = other.transform;
        }
        if (other.gameObject.name.Equals("Kolishki"))
        {
            _playerHealth.Die();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Worked");
            _isFinish = true;
        }
        if (leverArmTemp != null)
        {
            _isLeverArm = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Not worked");
            _isFinish = false;
        }
        if (leverArmTemp != null)
        {
            _isLeverArm = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("VerticalPlatform"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.name.Equals("HorizontalPlatform"))
        {
            this.transform.parent = null;
        }
    }
}