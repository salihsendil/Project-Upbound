using UnityEngine;

public abstract class BaseBoost : MonoBehaviour
{
    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    virtual public void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
