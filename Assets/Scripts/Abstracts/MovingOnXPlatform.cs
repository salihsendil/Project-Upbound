using UnityEngine;

public class MovingOnXPlatform : BasePlatform
{
    [SerializeField] private float limitPos = 2.75f;
    [SerializeField] private float speed = 3f;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        //start pos güncellenecek
        float newX = Mathf.PingPong(Time.time * speed, limitPos * 2) - limitPos;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
