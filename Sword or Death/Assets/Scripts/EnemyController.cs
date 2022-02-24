using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyModelTransfrorm;
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chasingSpeed = 3f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float timeToChase = 3f;


    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;
    private Vector2 _nextPoint;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer;
    private bool _collidedWithPlayer;

    private float _walkSpeed;
    private float _waitTime;
    private float _chaseTime;

    public bool IsFacingRight
    {
        get => _isFacingRight;
    }
    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance; //  Vector2.right = new Vector(1, 0)
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = patrolSpeed;
    }
    private void Update()
    {
        if (_isChasingPlayer)
        {
            StartChasingTimer();
        }
        if (_isWait && !_isChasingPlayer)
        {
            StartWaitTimer();
        }
        
        if (ShouldWait())
        {
            //wait
            _isWait = true;
        }
    }
    private void FixedUpdate()
    {
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (_isChasingPlayer && _collidedWithPlayer)
        {
            return;
        }
        if (_isChasingPlayer)
        {
            ChasePlayer();
        }

        if (!_isWait && !_isChasingPlayer)
        {
            Patrol();
        }
    }
    private void Patrol()
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();
        if (distance <0)
        {
            _nextPoint *= -1;
        }
        if (distance > 0.2f && !_isFacingRight)
        {
            Flip();
        }
        else if (distance < 0.2f && _isFacingRight)
        {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }

    private void StartWaitTimer()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;
        if(_chaseTime < 0f)
        {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = patrolSpeed;
        }
    }
    private bool ShouldWait()
    {
        bool isOutOfRightBoundray = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundray = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;
        return isOutOfLeftBoundray || isOutOfRightBoundray;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // color linii
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransfrorm.localScale;
        playerScale.x *= -1;
        enemyModelTransfrorm.localScale = playerScale;
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _collidedWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _collidedWithPlayer = false;
    }
}

