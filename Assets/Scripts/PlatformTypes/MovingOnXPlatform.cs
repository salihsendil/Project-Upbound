using UnityEngine;

public class MovingOnXPlatform : BasePlatform
{
    [SerializeField] private float xLimit = 2.75f;
    [SerializeField] private float speed = 2f;
    private Vector3 moveDirection = Vector3.right;

    private void FixedUpdate()
    {
        if (ShouldChangeDirection(transform.position.x))
        {
            ChangeDirection();
        }
        Move();
    }

    private bool ShouldChangeDirection(float currentX)
    {
        return currentX >= xLimit || currentX <= -xLimit;
    }

    private void ChangeDirection()
    {
        moveDirection = -moveDirection;
    }

    private void Move()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
