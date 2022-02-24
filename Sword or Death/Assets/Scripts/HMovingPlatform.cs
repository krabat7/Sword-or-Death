using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float distanceLeft = 4f;
    [SerializeField] float distanceRight = 4f;

    bool MovingRight = true;

    void Update()
    {
        if (transform.position.x > distanceRight)
        {
            MovingRight = false;
        }
        else if (transform.position.x < distanceLeft)
        {
            MovingRight = true;
        }

        if (MovingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
}
