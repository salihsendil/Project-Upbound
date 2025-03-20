using UnityEngine;

public abstract class BaseBoost : MonoBehaviour
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
