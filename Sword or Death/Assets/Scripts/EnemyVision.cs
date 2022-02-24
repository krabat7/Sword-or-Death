using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private GameObject currentHitObject;

    [SerializeField] private float circleRadius;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private EnemyController _enemyController;
    private Vector2 _origin;
    private Vector2 direction;

    private float currentHitDistance;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        _origin = transform.position;

        if (_enemyController.IsFacingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.CircleCast(_origin, circleRadius, direction, maxDistance, layerMask);

        if (hit)
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            if (currentHitObject.CompareTag("Player"))
            {
                _enemyController.StartChasingPlayer();
            }
            else
            {
                currentHitObject = null;
                currentHitDistance = maxDistance;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_origin, _origin + direction * currentHitDistance);
        Gizmos.DrawSphere(_origin + direction * currentHitDistance, circleRadius);
    }
}
