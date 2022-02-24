using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float distanceUp = 4f;
    [SerializeField] float distanceDown = 4f;

    bool MovingUp = true;

    void Update()
    {
        if (transform.position.y > distanceUp)
        {
            MovingUp = false;
        }
        else if (transform.position.y < distanceDown)
        {
            MovingUp = true;
        }

        if (MovingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}
